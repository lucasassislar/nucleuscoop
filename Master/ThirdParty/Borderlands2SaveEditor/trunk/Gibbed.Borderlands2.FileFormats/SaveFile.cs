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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Gibbed.IO;
using WillowTwoSave = Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.FileFormats
{
    public class SaveFile : INotifyPropertyChanged
    {
        #region Fields
        private Endian _Endian;
        private WillowTwoSave.WillowTwoPlayerSaveGame _SaveGame;
        #endregion

        #region Properties
        public Endian Endian
        {
            get { return this._Endian; }
            set
            {
                if (value != this._Endian)
                {
                    this._Endian = value;
                    this.NotifyPropertyChanged("Endian");
                }
            }
        }

        public WillowTwoSave.WillowTwoPlayerSaveGame SaveGame
        {
            get { return this._SaveGame; }
            set
            {
                if (value != this._SaveGame)
                {
                    this._SaveGame = value;
                    this.NotifyPropertyChanged("SaveGame");
                }
            }
        }
        #endregion

        public const int BlockSize = 0x40000;

        public void Serialize(Stream output)
        {
            var saveGame = this.SaveGame;

            byte[] innerUncompressedBytes;
            using (var innerUncompressedData = new MemoryStream())
            {
                saveGame.Compose();
                try
                {
                    ProtoBuf.Serializer.Serialize(innerUncompressedData, saveGame);
                }
                finally
                {
                    saveGame.Decompose();
                }
                innerUncompressedData.Position = 0;
                innerUncompressedBytes = innerUncompressedData.ReadBytes((uint)innerUncompressedData.Length);
            }

            byte[] innerCompressedBytes;
            using (var innerCompressedData = new MemoryStream())
            {
                var endian = this.Endian;

                innerCompressedData.WriteValueS32(0, Endian.Big);
                innerCompressedData.WriteString("WSG");
                innerCompressedData.WriteValueU32(2, endian);
                innerCompressedData.WriteValueU32(CRC32.Hash(innerUncompressedBytes, 0, innerUncompressedBytes.Length),
                                                  endian); // crc32
                innerCompressedData.WriteValueS32(innerUncompressedBytes.Length, endian);

                var encoder = new Huffman.Encoder();
                encoder.Build(innerUncompressedBytes);
                innerCompressedData.WriteBytes(encoder.Encode(innerUncompressedBytes));

                innerCompressedData.Position = 0;
                innerCompressedData.WriteValueU32((uint)(innerCompressedData.Length - 4), Endian.Big);

                innerCompressedData.Position = 0;
                innerCompressedBytes = innerCompressedData.ReadBytes((uint)innerCompressedData.Length);
            }

            byte[] compressedBytes;

            if (innerCompressedBytes.Length <= BlockSize)
            {
                compressedBytes = new byte[innerCompressedBytes.Length +
                                           (innerCompressedBytes.Length / 16) + 64 + 3];
                var actualCompressedSize = compressedBytes.Length;

                var result = LZO.Compress(innerCompressedBytes,
                                          0,
                                          innerCompressedBytes.Length,
                                          compressedBytes,
                                          0,
                                          ref actualCompressedSize);
                if (result != LZO.ErrorCode.Success)
                {
                    throw new SaveCorruptionException(string.Format("LZO compression failure ({0})", result));
                }

                Array.Resize(ref compressedBytes, actualCompressedSize);
            }
            else
            {
                int innerCompressedOffset = 0;
                int innerCompressedSizeLeft = innerCompressedBytes.Length;

                using (var blockData = new MemoryStream())
                {
                    var blockCount = (innerCompressedSizeLeft + BlockSize) / BlockSize;
                    blockData.WriteValueS32(blockCount, Endian.Big);

                    blockData.Position = 4 + (blockCount * 8);

                    var blockInfos = new List<Tuple<uint, uint>>();
                    while (innerCompressedSizeLeft > 0)
                    {
                        var blockUncompressedSize = Math.Min(BlockSize, innerCompressedSizeLeft);

                        compressedBytes = new byte[blockUncompressedSize +
                                                   (blockUncompressedSize / 16) + 64 + 3];
                        var actualCompressedSize = compressedBytes.Length;

                        var result = LZO.Compress(innerCompressedBytes,
                                                  innerCompressedOffset,
                                                  blockUncompressedSize,
                                                  compressedBytes,
                                                  0,
                                                  ref actualCompressedSize);
                        if (result != LZO.ErrorCode.Success)
                        {
                            throw new SaveCorruptionException(string.Format("LZO compression failure ({0})", result));
                        }

                        blockData.Write(compressedBytes, 0, actualCompressedSize);
                        blockInfos.Add(new Tuple<uint, uint>((uint)actualCompressedSize, BlockSize));

                        innerCompressedOffset += blockUncompressedSize;
                        innerCompressedSizeLeft -= blockUncompressedSize;
                    }

                    blockData.Position = 4;
                    foreach (var blockInfo in blockInfos)
                    {
                        blockData.WriteValueU32(blockInfo.Item1, Endian.Big);
                        blockData.WriteValueU32(blockInfo.Item2, Endian.Big);
                    }

                    blockData.Position = 0;
                    compressedBytes = blockData.ReadBytes((uint)blockData.Length);
                }
            }

            byte[] uncompressedBytes;
            using (var uncompressedData = new MemoryStream())
            {
                uncompressedData.WriteValueS32(innerCompressedBytes.Length, Endian.Big);
                uncompressedData.WriteBytes(compressedBytes);
                uncompressedData.Position = 0;
                uncompressedBytes = uncompressedData.ReadBytes((uint)uncompressedData.Length);
            }

            byte[] computedHash;
            using (var sha1 = new System.Security.Cryptography.SHA1Managed())
            {
                computedHash = sha1.ComputeHash(uncompressedBytes);
            }

            output.WriteBytes(computedHash);
            output.WriteBytes(uncompressedBytes);
        }

        [Flags]
        public enum DeserializeSettings
        {
            None = 0,
            IgnoreSha1Mismatch = 1 << 0,
            IgnoreCrc32Mismatch = 1 << 1,
            IgnoreReencodeMismatch = 1 << 2,
        }

        public static SaveFile Deserialize(Stream input, DeserializeSettings settings)
        {
            if (input.Position + 20 > input.Length)
            {
                throw new SaveCorruptionException("not enough data for save header");
            }

            var check = input.ReadValueU32(Endian.Big);
            if (check == 0x434F4E20)
            {
                throw new SaveFormatException("Xbox 360 save game loading is in the works");

            }
            input.Seek(-4, SeekOrigin.Current);

            var readSha1Hash = input.ReadBytes(20);
            using (var data = input.ReadToMemoryStream(input.Length - 20))
            {
                byte[] computedSha1Hash;
                using (var sha1 = new System.Security.Cryptography.SHA1Managed())
                {
                    computedSha1Hash = sha1.ComputeHash(data);
                }

                if ((settings & DeserializeSettings.IgnoreSha1Mismatch) == 0 &&
                    readSha1Hash.SequenceEqual(computedSha1Hash) == false)
                {
                    throw new SaveCorruptionException("invalid SHA1 hash");
                }

                data.Position = 0;
                var uncompressedSize = data.ReadValueU32(Endian.Big);

                var uncompressedBytes = new byte[uncompressedSize];
                if (uncompressedSize <= BlockSize)
                {
                    var actualUncompressedSize = (int)uncompressedSize;
                    var compressedSize = (uint)(data.Length - 4);
                    var compressedBytes = data.ReadBytes(compressedSize);
                    var result = LZO.Decompress(compressedBytes,
                                                0,
                                                (int)compressedSize,
                                                uncompressedBytes,
                                                0,
                                                ref actualUncompressedSize);
                    if (result != LZO.ErrorCode.Success)
                    {
                        throw new SaveCorruptionException(string.Format("LZO decompression failure ({0})", result));
                    }

                    if (actualUncompressedSize != (int)uncompressedSize)
                    {
                        throw new SaveCorruptionException("LZO decompression failure (uncompressed size mismatch)");
                    }
                }
                else
                {
                    var blockCount = data.ReadValueU32(Endian.Big);
                    var blockInfos = new List<Tuple<uint, uint>>();
                    for (uint i = 0; i < blockCount; i++)
                    {
                        var blockCompressedSize = data.ReadValueU32(Endian.Big);
                        var blockUncompressedSize = data.ReadValueU32(Endian.Big);
                        blockInfos.Add(new Tuple<uint, uint>(blockCompressedSize, blockUncompressedSize));
                    }

                    int uncompressedOffset = 0;
                    int uncompressedSizeLeft = (int)uncompressedSize;
                    foreach (var blockInfo in blockInfos)
                    {
                        var blockUncompressedSize = Math.Min((int)blockInfo.Item2, uncompressedSizeLeft);
                        var actualUncompressedSize = blockUncompressedSize;
                        var compressedSize = (int)blockInfo.Item1;
                        var compressedBytes = data.ReadBytes(compressedSize);
                        var result = LZO.Decompress(compressedBytes,
                                                    0,
                                                    compressedSize,
                                                    uncompressedBytes,
                                                    uncompressedOffset,
                                                    ref actualUncompressedSize);
                        if (result != LZO.ErrorCode.Success)
                        {
                            throw new SaveCorruptionException(string.Format("LZO decompression failure ({0})", result));
                        }

                        if (actualUncompressedSize != blockUncompressedSize)
                        {
                            throw new SaveCorruptionException("LZO decompression failure (uncompressed size mismatch)");
                        }

                        uncompressedOffset += blockUncompressedSize;
                        uncompressedSizeLeft -= blockUncompressedSize;
                    }

                    if (uncompressedSizeLeft != 0)
                    {
                        throw new SaveCorruptionException("LZO decompression failure (uncompressed size left != 0)");
                    }
                }

                using (var outerData = new MemoryStream(uncompressedBytes))
                {
                    var innerSize = outerData.ReadValueU32(Endian.Big);
                    var magic = outerData.ReadString(3);
                    if (magic != "WSG")
                    {
                        throw new SaveCorruptionException("invalid magic");
                    }

                    var version = outerData.ReadValueU32(Endian.Little);
                    if (version != 2 &&
                        version.Swap() != 2)
                    {
                        throw new SaveCorruptionException("invalid or unsupported version");
                    }
                    var endian = version == 2 ? Endian.Little : Endian.Big;

                    var readCRC32Hash = outerData.ReadValueU32(endian);
                    var innerUncompressedSize = outerData.ReadValueS32(endian);

                    var innerCompressedBytes = outerData.ReadBytes(innerSize - 3 - 4 - 4 - 4);
                    var innerUncompressedBytes = Huffman.Decoder.Decode(innerCompressedBytes,
                                                                        innerUncompressedSize);
                    if (innerUncompressedBytes.Length != innerUncompressedSize)
                    {
                        throw new SaveCorruptionException("huffman decompression failure");
                    }

                    var computedCRC32Hash = CRC32.Hash(innerUncompressedBytes, 0, innerUncompressedBytes.Length);
                    if ((settings & DeserializeSettings.IgnoreCrc32Mismatch) == 0 &&
                        computedCRC32Hash != readCRC32Hash)
                    {
                        throw new SaveCorruptionException("invalid CRC32 hash");
                    }

                    using (var innerUncompressedData = new MemoryStream(innerUncompressedBytes))
                    {
                        var saveGame =
                            ProtoBuf.Serializer.Deserialize<WillowTwoSave.WillowTwoPlayerSaveGame>(innerUncompressedData);

                        if ((settings & DeserializeSettings.IgnoreReencodeMismatch) == 0)
                        {
                            using (var testData = new MemoryStream())
                            {
                                ProtoBuf.Serializer.Serialize(testData, saveGame);

                                testData.Position = 0;
                                var testBytes = testData.ReadBytes((uint)testData.Length);
                                if (innerUncompressedBytes.SequenceEqual(testBytes) == false)
                                {
                                    throw new SaveCorruptionException("reencode mismatch");
                                }
                            }
                        }

                        saveGame.Decompose();
                        return new SaveFile()
                        {
                            Endian = endian,
                            SaveGame = saveGame,
                        };
                    }
                }
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
