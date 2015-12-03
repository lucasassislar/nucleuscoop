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
    public sealed class ItemBalanceDefinition
    {
        internal ItemBalanceDefinition()
        {
        }

        public string ResourcePath { get; internal set; }
        public ItemBalanceDefinition Base { get; internal set; }
        public ItemTypeDefinition Type { get; internal set; }
        public List<ItemTypeDefinition> Types { get; internal set; }
        public List<string> Manufacturers { get; internal set; }
        public ItemBalancePartCollection Parts { get; internal set; }

        public bool IsSuitableFor(ItemTypeDefinition type)
        {
            var current = this;
            do
            {
                if (current.Type != null ||
                    current.Types != null)
                {
                    if (current.Type == type ||
                        (current.Types != null && current.Types.Contains(type) == true))
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

        public ItemBalanceDefinition Merge(ItemTypeDefinition itemType)
        {
            var balances = new List<ItemBalanceDefinition>();
            var current = this;
            do
            {
                balances.Insert(0, current);
                current = current.Base;
            }
            while (current != null);

            var merged = new ItemBalanceDefinition()
            {
                ResourcePath = this.ResourcePath,
                Parts = new ItemBalancePartCollection()
                {
                    Mode = PartReplacementMode.Complete,
                    AlphaParts = itemType.AlphaParts.ToList(),
                    BetaParts = itemType.BetaParts.ToList(),
                    GammaParts = itemType.GammaParts.ToList(),
                    DeltaParts = itemType.DeltaParts.ToList(),
                    EpsilonParts = itemType.EpsilonParts.ToList(),
                    ZetaParts = itemType.ZetaParts.ToList(),
                    EtaParts = itemType.EtaParts.ToList(),
                    ThetaParts = itemType.ThetaParts.ToList(),
                    MaterialParts = itemType.MaterialParts.ToList(),
                },
            };

            foreach (var balance in balances)
            {
                if (balance.Type != null)
                {
                    merged.Type = balance.Type;
                }

                if (balance.Types != null)
                {
                    merged.Types = balance.Types.ToList();
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

                MergePartList(merged.Parts.AlphaParts, balance.Parts.Mode, balance.Parts.AlphaParts);
                MergePartList(merged.Parts.BetaParts, balance.Parts.Mode, balance.Parts.BetaParts);
                MergePartList(merged.Parts.GammaParts, balance.Parts.Mode, balance.Parts.GammaParts);
                MergePartList(merged.Parts.DeltaParts, balance.Parts.Mode, balance.Parts.DeltaParts);
                MergePartList(merged.Parts.EpsilonParts, balance.Parts.Mode, balance.Parts.EpsilonParts);
                MergePartList(merged.Parts.ZetaParts, balance.Parts.Mode, balance.Parts.ZetaParts);
                MergePartList(merged.Parts.EtaParts, balance.Parts.Mode, balance.Parts.EtaParts);
                MergePartList(merged.Parts.ThetaParts, balance.Parts.Mode, balance.Parts.ThetaParts);
                MergePartList(merged.Parts.MaterialParts, balance.Parts.Mode, balance.Parts.MaterialParts);
            }

            if (merged.Type != itemType &&
                merged.Types.Contains(itemType) == false)
            {
                throw new KeyNotFoundException("item type '" + itemType.ResourcePath + "' not valid for '" +
                                               this.ResourcePath + "'");
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
