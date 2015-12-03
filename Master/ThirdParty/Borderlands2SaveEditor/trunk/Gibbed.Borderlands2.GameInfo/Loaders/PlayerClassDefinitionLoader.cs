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
    internal static class PlayerClassDefinitionLoader
    {
        public static InfoDictionary<PlayerClassDefinition> Load(
            InfoDictionary<DownloadableContentDefinition> downloadableContents)
        {
            try
            {
                var raws = LoaderHelper.DeserializeJson<Dictionary<string, Raw.PlayerClassDefinition>>("Player Classes");
                return
                    new InfoDictionary<PlayerClassDefinition>(raws.ToDictionary(kv => kv.Key,
                                                                                kv =>
                                                                                GetPlayerClassDefinition(
                                                                                    downloadableContents, kv)));
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load player classes", e);
            }
        }

        private static PlayerClassDefinition GetPlayerClassDefinition(
            InfoDictionary<DownloadableContentDefinition> downloadableContents,
            KeyValuePair<string, Raw.PlayerClassDefinition> kv)
        {
            DownloadableContentDefinition content = null;
            if (string.IsNullOrEmpty(kv.Value.DLC) == false)
            {
                if (downloadableContents.ContainsKey(kv.Value.DLC) == false)
                {
                    throw new KeyNotFoundException("could not find downloadable content " + kv.Value.DLC);
                }
                content = downloadableContents[kv.Value.DLC];
            }

            return new PlayerClassDefinition()
            {
                ResourcePath = kv.Key,
                Name = kv.Value.Name,
                Class = kv.Value.Class,
                SortOrder = kv.Value.SortOrder,
                DLC = content,
            };
        }
    }
}
