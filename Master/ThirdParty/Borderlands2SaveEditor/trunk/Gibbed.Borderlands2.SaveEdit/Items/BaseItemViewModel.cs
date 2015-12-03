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
using Caliburn.Micro;
using Gibbed.Borderlands2.FileFormats.Items;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class BaseItemViewModel : PropertyChangedBase, IBaseSlotViewModel
    {
        private readonly BaseItem _Item;

        public IBaseSlot BaseSlot
        {
            get { return this._Item; }
        }

        public BaseItemViewModel(BaseItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._Item = item;

            IEnumerable<ItemTypeDefinition> resources;
            resources = InfoManager.ItemBalance.Items.Where(kv => kv.Value.Type != null).Select(kv => kv.Value.Type);
            resources =
                resources.Concat(
                    InfoManager.ItemBalance.Items.Where(kv => kv.Value.Types != null).SelectMany(bd => bd.Value.Types));

            this.TypeAssets = CreateAssetList(resources.Distinct().Select(t => t.ResourcePath).OrderBy(s => s));
            this.BuildBalanceAssets();
        }

        #region Properties
        public string Type
        {
            get { return this._Item.Type; }
            set
            {
                this._Item.Type = value;
                this.NotifyOfPropertyChange(() => this.Type);
                this.BuildBalanceAssets();
            }
        }

        public string Balance
        {
            get { return this._Item.Balance; }
            set
            {
                this._Item.Balance = value;
                this.NotifyOfPropertyChange(() => this.Balance);
                this.BuildPartAssets();
            }
        }

        public string Manufacturer
        {
            get { return this._Item.Manufacturer; }
            set
            {
                this._Item.Manufacturer = value;
                this.NotifyOfPropertyChange(() => this.Manufacturer);
            }
        }

        public int ManufacturerGradeIndex
        {
            get { return this._Item.ManufacturerGradeIndex; }
            set
            {
                this._Item.ManufacturerGradeIndex = value;
                this.NotifyOfPropertyChange(() => this.ManufacturerGradeIndex);
            }
        }

        public string AlphaPart
        {
            get { return this._Item.AlphaPart; }
            set
            {
                this._Item.AlphaPart = value;
                this.NotifyOfPropertyChange(() => this.AlphaPart);
            }
        }

        public string BetaPart
        {
            get { return this._Item.BetaPart; }
            set
            {
                this._Item.BetaPart = value;
                this.NotifyOfPropertyChange(() => this.BetaPart);
            }
        }

        public string GammaPart
        {
            get { return this._Item.GammaPart; }
            set
            {
                this._Item.GammaPart = value;
                this.NotifyOfPropertyChange(() => this.GammaPart);
            }
        }

        public string DeltaPart
        {
            get { return this._Item.DeltaPart; }
            set
            {
                this._Item.DeltaPart = value;
                this.NotifyOfPropertyChange(() => this.DeltaPart);
            }
        }

        public string EpsilonPart
        {
            get { return this._Item.EpsilonPart; }
            set
            {
                this._Item.EpsilonPart = value;
                this.NotifyOfPropertyChange(() => this.EpsilonPart);
            }
        }

        public string ZetaPart
        {
            get { return this._Item.ZetaPart; }
            set
            {
                this._Item.ZetaPart = value;
                this.NotifyOfPropertyChange(() => this.ZetaPart);
            }
        }

        public string EtaPart
        {
            get { return this._Item.EtaPart; }
            set
            {
                this._Item.EtaPart = value;
                this.NotifyOfPropertyChange(() => this.EtaPart);
            }
        }

        public string ThetaPart
        {
            get { return this._Item.ThetaPart; }
            set
            {
                this._Item.ThetaPart = value;
                this.NotifyOfPropertyChange(() => this.ThetaPart);
            }
        }

        public string MaterialPart
        {
            get { return this._Item.MaterialPart; }
            set
            {
                this._Item.MaterialPart = value;
                this.NotifyOfPropertyChange(() => this.MaterialPart);
            }
        }

        public string PrefixPart
        {
            get { return this._Item.PrefixPart; }
            set
            {
                this._Item.PrefixPart = value;
                this.NotifyOfPropertyChange(() => this.PrefixPart);
            }
        }

        public string TitlePart
        {
            get { return this._Item.TitlePart; }
            set
            {
                this._Item.TitlePart = value;
                this.NotifyOfPropertyChange(() => this.TitlePart);
            }
        }

        public int GameStage
        {
            get { return this._Item.GameStage; }
            set
            {
                this._Item.GameStage = value;
                this.NotifyOfPropertyChange(() => this.GameStage);
            }
        }

        public int UniqueId
        {
            get { return this._Item.UniqueId; }
            set
            {
                this._Item.UniqueId = value;
                this.NotifyOfPropertyChange(() => this.UniqueId);
            }
        }

        public int AssetLibrarySetId
        {
            get { return this._Item.AssetLibrarySetId; }
            set
            {
                this._Item.AssetLibrarySetId = value;
                this.NotifyOfPropertyChange(() => this.AssetLibrarySetId);
            }
        }
        #endregion Properties

        #region Display Properties
        public virtual string DisplayName
        {
            get { return "Base Item"; }
        }

        public virtual string DisplayGroup
        {
            get
            {
                if (InfoManager.ItemTypes.ContainsKey(this.Type) == false)
                {
                    return "Unknown";
                }

                switch (InfoManager.ItemTypes[this.Type].Type)
                {
                    case ItemType.Artifact:
                    {
                        return "Relic";
                    }

                    case ItemType.ClassMod:
                    {
                        return "Class Mod";
                    }

                    case ItemType.GrenadeMod:
                    {
                        return "Grenade Mod";
                    }

                    case ItemType.Shield:
                    {
                        return "Shield";
                    }

                    case ItemType.UsableCustomizationItem:
                    {
                        return "Customization";
                    }

                    case ItemType.UsableItem:
                    {
                        return "Personal";
                    }
                }

                return "Unknown";
            }
        }
        #endregion

        #region Assets
        private static IEnumerable<string> CreateAssetList(IEnumerable<string> items)
        {
            var list = new List<string>()
            {
                "None"
            };

            if (items != null)
            {
                list.AddRange(items);
            }

            return list;
        }

        #region Fields
        private IEnumerable<string> _BalanceAssets;
        private IEnumerable<string> _ManufacturerAssets;
        private IEnumerable<string> _AlphaPartAssets;
        private IEnumerable<string> _BetaPartAssets;
        private IEnumerable<string> _GammaPartAssets;
        private IEnumerable<string> _DeltaPartAssets;
        private IEnumerable<string> _EpsilonPartAssets;
        private IEnumerable<string> _ZetaPartAssets;
        private IEnumerable<string> _EtaPartAssets;
        private IEnumerable<string> _ThetaPartAssets;
        private IEnumerable<string> _MaterialPartAssets;
        #endregion

        #region Properties
        public IEnumerable<string> TypeAssets { get; private set; }

        public IEnumerable<string> BalanceAssets
        {
            get { return this._BalanceAssets; }
            private set
            {
                this._BalanceAssets = value;
                this.NotifyOfPropertyChange(() => this.BalanceAssets);
            }
        }

        public IEnumerable<string> ManufacturerAssets
        {
            get { return this._ManufacturerAssets; }
            private set
            {
                this._ManufacturerAssets = value;
                this.NotifyOfPropertyChange(() => this.ManufacturerAssets);
            }
        }

        public IEnumerable<string> AlphaPartAssets
        {
            get { return this._AlphaPartAssets; }
            private set
            {
                this._AlphaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.AlphaPartAssets);
            }
        }

        public IEnumerable<string> BetaPartAssets
        {
            get { return this._BetaPartAssets; }
            private set
            {
                this._BetaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.BetaPartAssets);
            }
        }

        public IEnumerable<string> GammaPartAssets
        {
            get { return this._GammaPartAssets; }
            private set
            {
                this._GammaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.GammaPartAssets);
            }
        }

        public IEnumerable<string> DeltaPartAssets
        {
            get { return this._DeltaPartAssets; }
            private set
            {
                this._DeltaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.DeltaPartAssets);
            }
        }

        public IEnumerable<string> EpsilonPartAssets
        {
            get { return this._EpsilonPartAssets; }
            private set
            {
                this._EpsilonPartAssets = value;
                this.NotifyOfPropertyChange(() => this.EpsilonPartAssets);
            }
        }

        public IEnumerable<string> ZetaPartAssets
        {
            get { return this._ZetaPartAssets; }
            private set
            {
                this._ZetaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.ZetaPartAssets);
            }
        }

        public IEnumerable<string> EtaPartAssets
        {
            get { return this._EtaPartAssets; }
            private set
            {
                this._EtaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.EtaPartAssets);
            }
        }

        public IEnumerable<string> ThetaPartAssets
        {
            get { return this._ThetaPartAssets; }
            private set
            {
                this._ThetaPartAssets = value;
                this.NotifyOfPropertyChange(() => this.ThetaPartAssets);
            }
        }

        public IEnumerable<string> MaterialPartAssets
        {
            get { return this._MaterialPartAssets; }
            private set
            {
                this._MaterialPartAssets = value;
                this.NotifyOfPropertyChange(() => this.MaterialPartAssets);
            }
        }
        #endregion

        private readonly string[] _NoneAssets = new[]
        {
            "None"
        };

        private void BuildBalanceAssets()
        {
            if (InfoManager.ItemTypes.ContainsKey(this.Type) == false)
            {
                this.BalanceAssets = CreateAssetList(null);
            }
            else
            {
                var type = InfoManager.ItemTypes[this.Type];
                this.BalanceAssets = CreateAssetList(
                    InfoManager.ItemBalance.Items
                        .Where(kv => kv.Value.IsSuitableFor(type) == true)
                        .Select(kv => kv.Key).OrderBy(s => s));
            }

            this.BuildPartAssets();
        }

        private void BuildPartAssets()
        {
            if (InfoManager.ItemTypes.ContainsKey(this.Type) == false || 
                this.BalanceAssets.Contains(this.Balance) == false ||
                InfoManager.ItemBalance.ContainsKey(this.Balance) == false ||
                this.Balance == "None")
            {
                this.ManufacturerAssets = _NoneAssets;
                this.AlphaPartAssets = _NoneAssets;
                this.AlphaPartAssets = _NoneAssets;
                this.BetaPartAssets = _NoneAssets;
                this.GammaPartAssets = _NoneAssets;
                this.DeltaPartAssets = _NoneAssets;
                this.EpsilonPartAssets = _NoneAssets;
                this.ZetaPartAssets = _NoneAssets;
                this.EtaPartAssets = _NoneAssets;
                this.ThetaPartAssets = _NoneAssets;
                this.MaterialPartAssets = _NoneAssets;
            }
            else
            {
                var type = InfoManager.ItemTypes[this.Type];
                var balance = InfoManager.ItemBalance[this.Balance].Merge(type);
                this.ManufacturerAssets = CreateAssetList(balance.Manufacturers.OrderBy(s => s).Distinct());
                this.AlphaPartAssets = CreateAssetList(balance.Parts.AlphaParts.OrderBy(s => s).Distinct());
                this.BetaPartAssets = CreateAssetList(balance.Parts.BetaParts.OrderBy(s => s).Distinct());
                this.GammaPartAssets = CreateAssetList(balance.Parts.GammaParts.OrderBy(s => s).Distinct());
                this.DeltaPartAssets = CreateAssetList(balance.Parts.DeltaParts.OrderBy(s => s).Distinct());
                this.EpsilonPartAssets = CreateAssetList(balance.Parts.EpsilonParts.OrderBy(s => s).Distinct());
                this.ZetaPartAssets = CreateAssetList(balance.Parts.ZetaParts.OrderBy(s => s).Distinct());
                this.EtaPartAssets = CreateAssetList(balance.Parts.EtaParts.OrderBy(s => s).Distinct());
                this.ThetaPartAssets = CreateAssetList(balance.Parts.ThetaParts.OrderBy(s => s).Distinct());
                this.MaterialPartAssets = CreateAssetList(balance.Parts.MaterialParts.OrderBy(s => s).Distinct());
            }
        }
        #endregion
    }
}
