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
    internal static class ItemBalanceDefinitionLoader
    {
        public static InfoDictionary<ItemBalanceDefinition> Load(InfoDictionary<ItemTypeDefinition> itemTypes)
        {
            try
            {
                var raws = LoaderHelper.DeserializeJson<Dictionary<string, Raw.ItemBalanceDefinition>>("Item Balance");
                var defs =
                    new InfoDictionary<ItemBalanceDefinition>(raws.ToDictionary(kv => kv.Key,
                                                                                kv =>
                                                                                GetItemBalanceDefinition(itemTypes, kv)));
                foreach (var kv in raws.Where(kv => string.IsNullOrEmpty(kv.Value.Base) == false))
                {
                    if (defs.ContainsKey(kv.Value.Base) == false)
                    {
                        throw new KeyNotFoundException("could not find item balance '" + kv.Value.Base + "'");
                    }

                    defs[kv.Key].Base = defs[kv.Value.Base];
                }
                return defs;
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load item balance", e);
            }
        }

        private static ItemBalanceDefinition GetItemBalanceDefinition(InfoDictionary<ItemTypeDefinition> itemTypes,
                                                                      KeyValuePair<string, Raw.ItemBalanceDefinition> kv)
        {
            return new ItemBalanceDefinition()
            {
                ResourcePath = kv.Key,
                Type = GetItemType(itemTypes, kv.Value.Type),
                Types = GetTypes(itemTypes, kv.Value.Types),
                Manufacturers = GetManufacturers(kv.Value.Manufacturers),
                Parts = GetItemBalancePartCollection(itemTypes, kv.Value.Parts),
            };
        }

        private static ItemTypeDefinition GetItemType(InfoDictionary<ItemTypeDefinition> itemTypes, string type)
        {
            if (string.IsNullOrEmpty(type) == true)
            {
                return null;
            }

            if (itemTypes.ContainsKey(type) == false)
            {
                throw new KeyNotFoundException("could not find item type '" + type + "'");
            }

            return itemTypes[type];
        }

        private static List<string> GetManufacturers(List<string> manufacturers)
        {
            if (manufacturers == null)
            {
                return null;
            }

            return manufacturers.ToList();
        }

        private static List<ItemTypeDefinition> GetTypes(InfoDictionary<ItemTypeDefinition> itemTypes,
                                                         List<string> types)
        {
            if (types == null)
            {
                return null;
            }

            return types.Select(t =>
            {
                if (itemTypes.ContainsKey(t) == false)
                {
                    throw new KeyNotFoundException("could not find item type '" + t + "'");
                }

                return itemTypes[t];
            }).ToList();
        }

        private static ItemBalancePartCollection GetItemBalancePartCollection(
            InfoDictionary<ItemTypeDefinition> itemTypes, Raw.ItemBalancePartCollection raw)
        {
            if (raw == null)
            {
                return null;
            }

            ItemTypeDefinition type = null;
            if (string.IsNullOrEmpty(raw.Type) == false)
            {
                if (itemTypes.ContainsKey(raw.Type) == false)
                {
                    throw new KeyNotFoundException("could not find item type " + raw.Type);
                }

                type = itemTypes[raw.Type];
            }

            return new ItemBalancePartCollection()
            {
                Type = type,
                Mode = raw.Mode,
                AlphaParts = raw.AlphaParts,
                BetaParts = raw.BetaParts,
                GammaParts = raw.GammaParts,
                DeltaParts = raw.DeltaParts,
                EpsilonParts = raw.EpsilonParts,
                ZetaParts = raw.ZetaParts,
                EtaParts = raw.EtaParts,
                ThetaParts = raw.ThetaParts,
                MaterialParts = raw.MaterialParts,
            };
        }
    }
}
