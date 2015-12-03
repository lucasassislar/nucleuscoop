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
    internal static class WeaponBalanceDefinitionLoader
    {
        public static InfoDictionary<WeaponBalanceDefinition> Load(InfoDictionary<WeaponTypeDefinition> weaponTypes)
        {
            try
            {
                var raws =
                    LoaderHelper.DeserializeJson<Dictionary<string, Raw.WeaponBalanceDefinition>>("Weapon Balance");
                var defs =
                    new InfoDictionary<WeaponBalanceDefinition>(raws.ToDictionary(kv => kv.Key,
                                                                                  kv =>
                                                                                  GetWeaponBalanceDefinition(
                                                                                      weaponTypes, kv)));
                foreach (var kv in raws.Where(kv => string.IsNullOrEmpty(kv.Value.Base) == false))
                {
                    if (defs.ContainsKey(kv.Value.Base) == false)
                    {
                        throw new KeyNotFoundException("could not find weapon balance '" + kv.Value.Base + "'");
                    }

                    defs[kv.Key].Base = defs[kv.Value.Base];
                }
                return defs;
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load weapon balance", e);
            }
        }

        private static WeaponBalanceDefinition GetWeaponBalanceDefinition(
            InfoDictionary<WeaponTypeDefinition> weaponTypes, KeyValuePair<string, Raw.WeaponBalanceDefinition> kv)
        {
            return new WeaponBalanceDefinition()
            {
                ResourcePath = kv.Key,
                Type = GetWeaponType(weaponTypes, kv.Value.Type),
                Manufacturers = GetManufacturers(kv.Value.Manufacturers),
                Parts = GetWeaponBalancePartCollection(weaponTypes, kv.Value.Parts),
            };
        }

        private static WeaponTypeDefinition GetWeaponType(InfoDictionary<WeaponTypeDefinition> weaponTypes, string type)
        {
            if (string.IsNullOrEmpty(type) == true)
            {
                return null;
            }

            if (weaponTypes.ContainsKey(type) == false)
            {
                throw new KeyNotFoundException("could not find weapon type '" + type + "'");
            }

            return weaponTypes[type];
        }

        private static List<string> GetManufacturers(List<string> manufacturers)
        {
            if (manufacturers == null)
            {
                return null;
            }

            return manufacturers.ToList();
        }

        private static WeaponBalancePartCollection GetWeaponBalancePartCollection(
            InfoDictionary<WeaponTypeDefinition> weaponTypes, Raw.WeaponBalancePartCollection raw)
        {
            if (raw == null)
            {
                return null;
            }

            WeaponTypeDefinition type = null;
            if (string.IsNullOrEmpty(raw.Type) == false)
            {
                if (weaponTypes.ContainsKey(raw.Type) == false)
                {
                    throw new KeyNotFoundException("could not find weapon type '" + raw.Type + "'");
                }

                type = weaponTypes[raw.Type];
            }

            return new WeaponBalancePartCollection()
            {
                Type = type,
                Mode = raw.Mode,
                BodyParts = raw.BodyParts,
                GripParts = raw.GripParts,
                BarrelParts = raw.BarrelParts,
                SightParts = raw.SightParts,
                StockParts = raw.StockParts,
                ElementalParts = raw.ElementalParts,
                Accessory1Parts = raw.Accessory1Parts,
                Accessory2Parts = raw.Accessory2Parts,
                MaterialParts = raw.MaterialParts,
            };
        }
    }
}
