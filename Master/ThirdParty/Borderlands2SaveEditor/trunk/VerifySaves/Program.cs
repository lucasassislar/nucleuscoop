/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.IO;
using System.Linq;
using Gibbed.Borderlands2.FileFormats;
using Gibbed.IO;
using ProtoBuf;
using Huffman = Gibbed.Borderlands2.FileFormats.Huffman;
using WillowTwoSave = Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace VerifySaves
{
    internal class Program
    {
        
        private static void Main(string[] args)
        {
            var paths = Directory.GetFiles("saves", "*.sav");

            var successes = 0;
            var failures = 0;

            foreach (var path in paths)
            {
                var name = Path.GetFileNameWithoutExtension(path);

                using (var input = File.OpenRead(path))
                {
                    var readHash = input.ReadBytes(20);
                    using (var data = input.ReadToMemoryStream(input.Length - 20))
                    {
                        byte[] computedHash;
                        using (var sha1 = new System.Security.Cryptography.SHA1Managed())
                        {
                            computedHash = sha1.ComputeHash(data);
                        }

                        if (readHash.SequenceEqual(computedHash) == false)
                        {
                            Console.WriteLine("{0}: failed (SHA1 mismatch)", name);
                            failures++;
                            continue;
                        }

                        data.Position = 0;
                        var uncompressedSize = data.ReadValueU32(Endian.Big);
                        var actualUncompressedSize = (int)uncompressedSize;
                        var uncompressedBytes = new byte[uncompressedSize];
                        var compressedSize = (int)(data.Length - 4);
                        var compressedBytes = data.ReadBytes(compressedSize);
                        var result = LZO.Decompress(compressedBytes,
                                                    0,
                                                    compressedSize,
                                                    uncompressedBytes,
                                                    0,
                                                    ref actualUncompressedSize);
                        if (result != LZO.ErrorCode.Success)
                        {
                            Console.WriteLine("{0}: failed (LZO error {1})", name, result);
                            failures++;
                            continue;
                        }

                        using (var outerData = new MemoryStream(uncompressedBytes))
                        {
                            var innerSize = outerData.ReadValueU32(Endian.Big);
                            var magic = outerData.ReadString(3);
                            if (magic != "WSG")
                            {
                                Console.WriteLine("{0}: failed (bad magic)", name);
                                failures++;
                                continue;
                            }

                            var version = outerData.ReadValueU32(Endian.Little);
                            if (version != 2 &&
                                version.Swap() != 2)
                            {
                                Console.WriteLine("{0}: failed (bad version)", name);
                                failures++;
                                continue;
                            }

                            var endian = version == 2 ? Endian.Little : Endian.Big;

                            var hash = outerData.ReadValueU32(endian);
                            var innerUncompressedSize = outerData.ReadValueS32(endian);

                            var innerCompressedBytes = outerData.ReadBytes(innerSize - 3 - 4 - 4 - 4);
                            var innerUncompressedBytes = Huffman.Decoder.Decode(innerCompressedBytes,
                                                                                innerUncompressedSize);
                            using (var innerUncompressedData = new MemoryStream(innerUncompressedBytes))
                            {
                                using (var output = File.Create("temp.bin"))
                                {
                                    output.WriteBytes(innerUncompressedBytes);
                                }

                                var save =
                                    Serializer.Deserialize<WillowTwoSave.WillowTwoPlayerSaveGame>(innerUncompressedData);

                                using (var testData = new MemoryStream())
                                {
                                    Serializer.Serialize(testData, save);

                                    testData.Position = 0;
                                    var testBytes = testData.ReadBytes((uint)testData.Length);
                                    if (innerUncompressedBytes.SequenceEqual(testBytes) == false)
                                    {
                                        Console.WriteLine("{0}: failed (reencode mismatch)", name);

                                        using (var output = File.Create(Path.Combine("failures", name + "_before.bin")))
                                        {
                                            output.WriteBytes(innerUncompressedBytes);
                                        }

                                        using (var output = File.Create(Path.Combine("failures", name + "_after.bin")))
                                        {
                                            output.WriteBytes(testBytes);
                                        }

                                        failures++;
                                        continue;
                                    }

                                    successes++;
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("{0} processed ({1} failed, {2} succeeded).",
                              paths.Length,
                              failures,
                              successes);
        }
    }
}
