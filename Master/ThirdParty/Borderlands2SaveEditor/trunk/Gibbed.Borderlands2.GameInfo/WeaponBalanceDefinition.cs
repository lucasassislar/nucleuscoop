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

namespace Gibbed.Borderlands2.GameInfo
{
    public sealed class WeaponBalanceDefinition
    {
        internal WeaponBalanceDefinition()
        {
        }

        public string ResourcePath { get; internal set; }
        public WeaponBalanceDefinition Base { get; internal set; }
        public WeaponTypeDefinition Type { get; internal set; }
        public List<string> Manufacturers { get; internal set; }
        public WeaponBalancePartCollection Parts { get; internal set; }

        public bool IsSuitableFor(WeaponTypeDefinition type)
        {
            var current = this;
            do
            {
                if (current.Type != null)
                {
                    if (current.Type == type)
                    {
                        return true;
                    }

                    return false;
                }

                current = current.Base;
            }
            while (current != null);

            return false;
        }

        public WeaponBalanceDefinition Merge(WeaponTypeDefinition type)
        {
            var balances = new List<WeaponBalanceDefinition>();

            var current = this;
            do
            {
                balances.Insert(0, current);
                current = current.Base;
            }
            while (current != null);

            var merged = new WeaponBalanceDefinition()
            {
                ResourcePath = this.ResourcePath,
                Parts = new WeaponBalancePartCollection()
                {
                    Mode = PartReplacementMode.Complete,
                    BodyParts = new List<string>(),
                    GripParts = new List<string>(),
                    BarrelParts = new List<string>(),
                    SightParts = new List<string>(),
                    StockParts = new List<string>(),
                    ElementalParts = new List<string>(),
                    Accessory1Parts = new List<string>(),
                    Accessory2Parts = new List<string>(),
                    MaterialParts = new List<string>(),
                },
            };

            foreach (var balance in balances)
            {
                if (balance.Type != null)
                {
                    merged.Type = balance.Type;
                }

                if (balance.Manufacturers != null)
                {
                    merged.Manufacturers = balance.Manufacturers.ToList();
                }

                if (balance.Parts == null)
                {
                    continue;
                }

                if (balance.Parts.Type != null)
                {
                    merged.Parts.Type = balance.Parts.Type;
                }

                MergePartList(merged.Parts.BodyParts, balance.Parts.Mode, balance.Parts.BodyParts);
                MergePartList(merged.Parts.GripParts, balance.Parts.Mode, balance.Parts.GripParts);
                MergePartList(merged.Parts.BarrelParts, balance.Parts.Mode, balance.Parts.BarrelParts);
                MergePartList(merged.Parts.SightParts, balance.Parts.Mode, balance.Parts.SightParts);
                MergePartList(merged.Parts.StockParts, balance.Parts.Mode, balance.Parts.StockParts);
                MergePartList(merged.Parts.ElementalParts, balance.Parts.Mode, balance.Parts.ElementalParts);
                MergePartList(merged.Parts.Accessory1Parts, balance.Parts.Mode, balance.Parts.Accessory1Parts);
                MergePartList(merged.Parts.Accessory2Parts, balance.Parts.Mode, balance.Parts.Accessory2Parts);
                MergePartList(merged.Parts.MaterialParts, balance.Parts.Mode, balance.Parts.MaterialParts);
            }

            if (merged.Type != type)
            {
                throw new KeyNotFoundException("weapon type '" + type.ResourcePath + "' not valid for '" + this.ResourcePath + "'");
            }

            return merged;
        }

        private static void MergePartList(List<string> destination, PartReplacementMode mode, List<string> source)
        {
            if (mode == PartReplacementMode.Additive)
            {
                if (source != null)
                {
                    destination.AddRange(source);
                }
            }
            else if (mode == PartReplacementMode.Selective)
            {
                if (source != null)
                {
                    destination.Clear();
                    destination.AddRange(source);
                }
            }
            else if (mode == PartReplacementMode.Complete)
            {
                destination.Clear();
                if (source != null)
                {
                    destination.AddRange(source);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
