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
using System.Linq;

namespace Gibbed.Borderlands2.GameInfo.Loaders
{
    internal static class AssetLibraryManagerLoader
    {
        public static AssetLibraryManager Load()
        {
            try
            {
                var raw = LoaderHelper.DeserializeJson<Raw.AssetLibraryManager>("Asset Library Manager");
                return GetAssetLibraryManager(raw);
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load asset library manager", e);
            }
        }

        private static AssetLibraryManager GetAssetLibraryManager(Raw.AssetLibraryManager raw)
        {
            return new AssetLibraryManager()
            {
                Version = raw.Version,
                Configurations = raw.Configurations.ToDictionary(kv => kv.Key, kv => GetAssetLibraryConfiguration(kv)),
                Sets = raw.Sets.Select(s => GetAssetLibrarySet(s)).ToList(),
            };
        }

        private static AssetLibraryConfiguration GetAssetLibraryConfiguration(
            KeyValuePair<AssetGroup, Raw.AssetLibraryConfiguration> kv)
        {
            return new AssetLibraryConfiguration()
            {
                Group = kv.Key,
                Type = kv.Value.Type,
                SublibraryBits = kv.Value.SublibraryBits,
                AssetBits = kv.Value.AssetBits,
            };
        }

        private static AssetLibrarySet GetAssetLibrarySet(Raw.AssetLibrarySet raw)
        {
            return new AssetLibrarySet()
            {
                Id = raw.Id,
                Libraries = raw.Libraries.ToDictionary(kv => kv.Key, kv => GetAssetLibraryDefinition(kv)),
            };
        }

        private static AssetLibraryDefinition GetAssetLibraryDefinition(
            KeyValuePair<AssetGroup, Raw.AssetLibraryDefinition> kv)
        {
            return new AssetLibraryDefinition()
            {
                Group = kv.Key,
                Type = kv.Value.Type,
                Sublibraries = kv.Value.Sublibraries.Select(sl => GetAssetSublibraryDefinition(sl)).ToList(),
            };
        }

        private static AssetSublibraryDefinition GetAssetSublibraryDefinition(Raw.AssetSublibraryDefinition raw)
        {
            return new AssetSublibraryDefinition()
            {
                Description = raw.Description,
                Package = raw.Package,
                Assets = raw.Assets.ToList(),
            };
        }
    }
}
