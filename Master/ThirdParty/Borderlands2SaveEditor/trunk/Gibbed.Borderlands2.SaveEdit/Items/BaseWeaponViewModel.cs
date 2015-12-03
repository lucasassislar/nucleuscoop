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
    internal class BaseWeaponViewModel : PropertyChangedBase, IBaseSlotViewModel
    {
        private readonly BaseWeapon _Weapon;

        public IBaseSlot BaseSlot
        {
            get { return this._Weapon; }
        }

        public BaseWeaponViewModel(BaseWeapon weapon)
        {
            if (weapon == null)
            {
                throw new ArgumentNullException("weapon");
            }

            this._Weapon = weapon;

            this.TypeAssets =
                CreateAssetList(
                    InfoManager.WeaponBalance.Items.Where(kv => kv.Value.Type != null).Select(
                        kv => kv.Value.Type.ResourcePath).Distinct().OrderBy(s => s));
            this.BuildBalanceAssets();
        }

        #region Properties
        public string Type
        {
            get { return this._Weapon.Type; }
            set
            {
                this._Weapon.Type = value;
                this.NotifyOfPropertyChange(() => this.Type);
                this.BuildBalanceAssets();
            }
        }

        public string Balance
        {
            get { return this._Weapon.Balance; }
            set
            {
                this._Weapon.Balance = value;
                this.NotifyOfPropertyChange(() => this.Balance);
                this.BuildPartAssets();
            }
        }

        public string Manufacturer
        {
            get { return this._Weapon.Manufacturer; }
            set
            {
                this._Weapon.Manufacturer = value;
                this.NotifyOfPropertyChange(() => this.Manufacturer);
            }
        }

        public int ManufacturerGradeIndex
        {
            get { return this._Weapon.ManufacturerGradeIndex; }
            set
            {
                this._Weapon.ManufacturerGradeIndex = value;
                this.NotifyOfPropertyChange(() => this.ManufacturerGradeIndex);
            }
        }

        public string BodyPart
        {
            get { return this._Weapon.BodyPart; }
            set
            {
                this._Weapon.BodyPart = value;
                this.NotifyOfPropertyChange(() => this.BodyPart);
            }
        }

        public string GripPart
        {
            get { return this._Weapon.GripPart; }
            set
            {
                this._Weapon.GripPart = value;
                this.NotifyOfPropertyChange(() => this.GripPart);
            }
        }

        public string BarrelPart
        {
            get { return this._Weapon.BarrelPart; }
            set
            {
                this._Weapon.BarrelPart = value;
                this.NotifyOfPropertyChange(() => this.BarrelPart);
            }
        }

        public string SightPart
        {
            get { return this._Weapon.SightPart; }
            set
            {
                this._Weapon.SightPart = value;
                this.NotifyOfPropertyChange(() => this.SightPart);
            }
        }

        public string StockPart
        {
            get { return this._Weapon.StockPart; }
            set
            {
                this._Weapon.StockPart = value;
                this.NotifyOfPropertyChange(() => this.StockPart);
            }
        }

        public string ElementalPart
        {
            get { return this._Weapon.ElementalPart; }
            set
            {
                this._Weapon.ElementalPart = value;
                this.NotifyOfPropertyChange(() => this.ElementalPart);
            }
        }

        public string Accessory1Part
        {
            get { return this._Weapon.Accessory1Part; }
            set
            {
                this._Weapon.Accessory1Part = value;
                this.NotifyOfPropertyChange(() => this.Accessory1Part);
            }
        }

        public string Accessory2Part
        {
            get { return this._Weapon.Accessory2Part; }
            set
            {
                this._Weapon.Accessory2Part = value;
                this.NotifyOfPropertyChange(() => this.Accessory2Part);
            }
        }

        public string MaterialPart
        {
            get { return this._Weapon.MaterialPart; }
            set
            {
                this._Weapon.MaterialPart = value;
                this.NotifyOfPropertyChange(() => this.MaterialPart);
            }
        }

        public string PrefixPart
        {
            get { return this._Weapon.PrefixPart; }
            set
            {
                this._Weapon.PrefixPart = value;
                this.NotifyOfPropertyChange(() => this.PrefixPart);
            }
        }

        public string TitlePart
        {
            get { return this._Weapon.TitlePart; }
            set
            {
                this._Weapon.TitlePart = value;
                this.NotifyOfPropertyChange(() => this.TitlePart);
            }
        }

        public int GameStage
        {
            get { return this._Weapon.GameStage; }
            set
            {
                this._Weapon.GameStage = value;
                this.NotifyOfPropertyChange(() => this.GameStage);
            }
        }

        public int UniqueId
        {
            get { return this._Weapon.UniqueId; }
            set
            {
                this._Weapon.UniqueId = value;
                this.NotifyOfPropertyChange(() => this.UniqueId);
            }
        }

        public int AssetLibrarySetId
        {
            get { return this._Weapon.AssetLibrarySetId; }
            set
            {
                this._Weapon.AssetLibrarySetId = value;
                this.NotifyOfPropertyChange(() => this.AssetLibrarySetId);
            }
        }
        #endregion

        #region Display Properties
        public virtual string DisplayName
        {
            get { return "Base Weapon"; }
        }

        public virtual string DisplayGroup
        {
            get { return "Weapons"; }
        }
        #endregion

        #region Assets
        private static IEnumerable<string> CreateAssetList(IEnumerable<string> items)
        {
            var list = new List<string>
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
        private IEnumerable<string> _BodyPartAssets;
        private IEnumerable<string> _GripPartAssets;
        private IEnumerable<string> _BarrelPartAssets;
        private IEnumerable<string> _SightPartAssets;
        private IEnumerable<string> _StockPartAssets;
        private IEnumerable<string> _ElementalPartAssets;
        private IEnumerable<string> _Accessory1PartAssets;
        private IEnumerable<string> _Accessory2PartAssets;
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

        public IEnumerable<string> BodyPartAssets
        {
            get { return this._BodyPartAssets; }
            private set
            {
                this._BodyPartAssets = value;
                this.NotifyOfPropertyChange(() => this.BodyPartAssets);
            }
        }

        public IEnumerable<string> GripPartAssets
        {
            get { return this._GripPartAssets; }
            private set
            {
                this._GripPartAssets = value;
                this.NotifyOfPropertyChange(() => this.GripPartAssets);
            }
        }

        public IEnumerable<string> BarrelPartAssets
        {
            get { return this._BarrelPartAssets; }
            private set
            {
                this._BarrelPartAssets = value;
                this.NotifyOfPropertyChange(() => this.BarrelPartAssets);
            }
        }

        public IEnumerable<string> SightPartAssets
        {
            get { return this._SightPartAssets; }
            private set
            {
                this._SightPartAssets = value;
                this.NotifyOfPropertyChange(() => this.SightPartAssets);
            }
        }

        public IEnumerable<string> StockPartAssets
        {
            get { return this._StockPartAssets; }
            private set
            {
                this._StockPartAssets = value;
                this.NotifyOfPropertyChange(() => this.StockPartAssets);
            }
        }

        public IEnumerable<string> ElementalPartAssets
        {
            get { return this._ElementalPartAssets; }
            private set
            {
                this._ElementalPartAssets = value;
                this.NotifyOfPropertyChange(() => this.ElementalPartAssets);
            }
        }

        public IEnumerable<string> Accessory1PartAssets
        {
            get { return this._Accessory1PartAssets; }
            private set
            {
                this._Accessory1PartAssets = value;
                this.NotifyOfPropertyChange(() => this.Accessory1PartAssets);
            }
        }

        public IEnumerable<string> Accessory2PartAssets
        {
            get { return this._Accessory2PartAssets; }
            private set
            {
                this._Accessory2PartAssets = value;
                this.NotifyOfPropertyChange(() => this.Accessory2PartAssets);
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
            if (InfoManager.WeaponTypes.ContainsKey(this.Type) == false)
            {
                this.BalanceAssets = CreateAssetList(null);
            }
            else
            {
                var type = InfoManager.WeaponTypes[this.Type];
                this.BalanceAssets =
                    CreateAssetList(
                        InfoManager.WeaponBalance.Items
                            .Where(bd => bd.Value.IsSuitableFor(type) == true)
                            .Select(kv => kv.Key).Distinct().OrderBy(s => s));
            }

            this.BuildPartAssets();
        }

        private void BuildPartAssets()
        {
            if (InfoManager.WeaponTypes.ContainsKey(this.Type) == false ||
                this.BalanceAssets.Contains(this.Balance) == false ||
                InfoManager.WeaponBalance.ContainsKey(this.Balance) == false ||
                this.Balance == "None")
            {
                this.ManufacturerAssets = _NoneAssets;
                this.BodyPartAssets = _NoneAssets;
                this.BodyPartAssets = _NoneAssets;
                this.GripPartAssets = _NoneAssets;
                this.BarrelPartAssets = _NoneAssets;
                this.SightPartAssets = _NoneAssets;
                this.StockPartAssets = _NoneAssets;
                this.ElementalPartAssets = _NoneAssets;
                this.Accessory1PartAssets = _NoneAssets;
                this.Accessory2PartAssets = _NoneAssets;
                this.MaterialPartAssets = _NoneAssets;
            }
            else
            {
                var type = InfoManager.WeaponTypes[this.Type];
                var balance = InfoManager.WeaponBalance[this.Balance].Merge(type);
                this.ManufacturerAssets = CreateAssetList(balance.Manufacturers.OrderBy(s => s).Distinct());
                this.BodyPartAssets = CreateAssetList(balance.Parts.BodyParts.OrderBy(s => s).Distinct());
                this.GripPartAssets = CreateAssetList(balance.Parts.GripParts.OrderBy(s => s).Distinct());
                this.BarrelPartAssets = CreateAssetList(balance.Parts.BarrelParts.OrderBy(s => s).Distinct());
                this.SightPartAssets = CreateAssetList(balance.Parts.SightParts.OrderBy(s => s).Distinct());
                this.StockPartAssets = CreateAssetList(balance.Parts.StockParts.OrderBy(s => s).Distinct());
                this.ElementalPartAssets = CreateAssetList(balance.Parts.ElementalParts.OrderBy(s => s).Distinct());
                this.Accessory1PartAssets = CreateAssetList(balance.Parts.Accessory1Parts.OrderBy(s => s).Distinct());
                this.Accessory2PartAssets = CreateAssetList(balance.Parts.Accessory2Parts.OrderBy(s => s).Distinct());
                this.MaterialPartAssets = CreateAssetList(balance.Parts.MaterialParts.OrderBy(s => s).Distinct());
            }
        }
        #endregion
    }
}
