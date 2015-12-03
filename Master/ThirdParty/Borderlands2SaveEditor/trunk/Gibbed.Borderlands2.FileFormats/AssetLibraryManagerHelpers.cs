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
using System.Linq;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.FileFormats
{
    public static class AssetLibraryManagerHelpers
    {
        private static bool GetIndex(this AssetLibraryManager alm,
                                     int setId,
                                     AssetGroup group,
                                     string package,
                                     string asset,
                                     out uint index)
        {
            var config = alm.Configurations[group];

            var set = alm.GetSet(setId);
            var library = set.Libraries[group];

            var sublibrary =
                library.Sublibraries.FirstOrDefault(sl => sl.Package == package && sl.Assets.Contains(asset));
            if (sublibrary == null)
            {
                index = 0;
                return false;
            }

            var sublibraryIndex = library.Sublibraries.IndexOf(sublibrary);
            var assetIndex = sublibrary.Assets.IndexOf(asset);

            index = 0;
            index |= (((uint)assetIndex) & config.AssetMask) << 0;
            index |= (((uint)sublibraryIndex) & config.SublibraryMask) << config.AssetBits;

            if (setId != 0)
            {
                index |= 1u << config.AssetBits + config.SublibraryBits - 1;
            }

            return true;
        }

        public static void Encode(this AssetLibraryManager alm,
                                  BitWriter writer,
                                  int setId,
                                  AssetGroup group,
                                  string value)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            if (string.IsNullOrEmpty(value) == true)
            {
                throw new ArgumentNullException("value");
            }

            var config = alm.Configurations[group];

            uint index;
            if (value == "None")
            {
                index = config.NoneIndex;
            }
            else
            {
                var parts = value.Split(new[]
                {
                    '.'
                },
                                        2);
                if (parts.Length != 2)
                {
                    throw new ArgumentException();
                }

                var package = parts[0];
                var asset = parts[1];

                if (alm.GetIndex(setId, group, package, asset, out index) == false)
                {
                    if (alm.GetIndex(0, group, package, asset, out index) == false)
                    {
                        throw new ArgumentException("unsupported asset");
                    }
                }
            }

            writer.WriteUInt32(index, config.SublibraryBits + config.AssetBits);
        }

        public static string Decode(this AssetLibraryManager alm, BitReader reader, int setId, AssetGroup group)
        {
            var config = alm.Configurations[group];

            var index = reader.ReadUInt32(config.SublibraryBits + config.AssetBits);
            if (index == config.NoneIndex)
            {
                return "None";
            }

            var assetIndex = (int)((index >> 0) & config.AssetMask);
            var sublibraryIndex = (int)((index >> config.AssetBits) & config.SublibraryMask);
            var useSetId = ((index >> config.AssetBits) & config.UseSetIdMask) != 0;

            var set = alm.GetSet(useSetId == false ? 0 : setId);
            var library = set.Libraries[group];

            if (sublibraryIndex < 0 || sublibraryIndex >= library.Sublibraries.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return library.Sublibraries[sublibraryIndex].GetAsset(assetIndex);
        }
    }
}
