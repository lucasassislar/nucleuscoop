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
    internal static class WeaponTypeDefinitionLoader
    {
        public static InfoDictionary<WeaponTypeDefinition> Load()
        {
            try
            {
                var raws = LoaderHelper.DeserializeJson<Dictionary<string, Raw.WeaponTypeDefinition>>("Weapon Types");
                return new InfoDictionary<WeaponTypeDefinition>(raws.ToDictionary(kv => kv.Key, GetWeaponTypeDefinition));
            }
            catch (Exception e)
            {
                throw new InfoLoadException("failed to load weapon types", e);
            }
        }

        private static WeaponTypeDefinition GetWeaponTypeDefinition(KeyValuePair<string, Raw.WeaponTypeDefinition> kv)
        {
            return new WeaponTypeDefinition()
            {
                ResourcePath = kv.Key,
                Type = kv.Value.Type,
                Name = kv.Value.Name,
                Titles = kv.Value.Titles,
                Prefixes = kv.Value.Prefixes,
                BodyParts = kv.Value.BodyParts,
                GripParts = kv.Value.GripParts,
                BarrelParts = kv.Value.BarrelParts,
                SightParts = kv.Value.SightParts,
                StockParts = kv.Value.StockParts,
                ElementalParts = kv.Value.ElementalParts,
                Accessory1Parts = kv.Value.Accessory1Parts,
                Accessory2Parts = kv.Value.Accessory2Parts,
                MaterialParts = kv.Value.MaterialParts,
            };
        }
    }
}
