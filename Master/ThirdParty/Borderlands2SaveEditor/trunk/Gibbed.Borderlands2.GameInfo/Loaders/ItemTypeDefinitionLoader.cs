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
    internal static class ItemTypeDefinitionLoader
    {
        public static InfoDictionary<ItemTypeDefinition> Load()
        {
            try
            {
                var raws = LoaderHelper.DeserializeJson<Dictionary<string, Raw.ItemTypeDefinition>>("Item Types");
                return new InfoDictionary<ItemTypeDefinition>(raws.ToDictionary(kv => kv.Key, GetItemTypeDefinition));
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load item types", e);
            }
        }

        private static ItemTypeDefinition GetItemTypeDefinition(KeyValuePair<string, Raw.ItemTypeDefinition> kv)
        {
            return new ItemTypeDefinition()
            {
                ResourcePath = kv.Key,
                Type = kv.Value.Type,
                Name = kv.Value.Name,
                Titles = kv.Value.Titles,
                Prefixes = kv.Value.Prefixes,
                AlphaParts = kv.Value.AlphaParts,
                BetaParts = kv.Value.BetaParts,
                GammaParts = kv.Value.GammaParts,
                DeltaParts = kv.Value.DeltaParts,
                EpsilonParts = kv.Value.EpsilonParts,
                ZetaParts = kv.Value.ZetaParts,
                EtaParts = kv.Value.EtaParts,
                ThetaParts = kv.Value.ThetaParts,
                MaterialParts = kv.Value.MaterialParts,
            };
        }
    }
}
