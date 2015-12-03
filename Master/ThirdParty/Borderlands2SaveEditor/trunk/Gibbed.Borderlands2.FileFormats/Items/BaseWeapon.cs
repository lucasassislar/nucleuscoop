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

using System.ComponentModel;
using Gibbed.Borderlands2.GameInfo;

namespace Gibbed.Borderlands2.FileFormats.Items
{
    public class BaseWeapon : IBaseSlot, IPackable, INotifyPropertyChanged
    {
        #region Fields
        private string _Type = "None";
        private string _Balance = "None";
        private string _Manufacturer = "None";
        private int _ManufacturerGradeIndex;
        private string _BodyPart = "None";
        private string _GripPart = "None";
        private string _BarrelPart = "None";
        private string _SightPart = "None";
        private string _StockPart = "None";
        private string _ElementalPart = "None";
        private string _Accessory1Part = "None";
        private string _Accessory2Part = "None";
        private string _MaterialPart = "None";
        private string _PrefixPart = "None";
        private string _TitlePart = "None";
        private int _GameStage;
        private int _UniqueId;
        private int _AssetLibrarySetId;
        #endregion

        #region IPackable Members
        public void Read(BitReader reader)
        {
            var alm = InfoManager.AssetLibraryManager;

            this.Type = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponTypes);
            this.Balance = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.BalanceDefs);
            this.Manufacturer = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.Manufacturers);
            this.ManufacturerGradeIndex = reader.ReadInt32(7);
            this.GameStage = reader.ReadInt32(7);
            this.BodyPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.GripPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.BarrelPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.SightPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.StockPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.ElementalPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.Accessory1Part = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.Accessory2Part = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.MaterialPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.PrefixPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
            this.TitlePart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.WeaponParts);
        }

        public void Write(BitWriter writer)
        {
            var alm = InfoManager.AssetLibraryManager;

            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponTypes, this.Type);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.BalanceDefs, this.Balance);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.Manufacturers, this.Manufacturer);
            writer.WriteInt32(this.ManufacturerGradeIndex, 7);
            writer.WriteInt32(this.GameStage, 7);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.BodyPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.GripPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.BarrelPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.SightPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.StockPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.ElementalPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.Accessory1Part);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.Accessory2Part);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.MaterialPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.PrefixPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.WeaponParts, this.TitlePart);
        }
        #endregion

        #region Properties
        public string Type
        {
            get { return this._Type; }
            set
            {
                if (value != this._Type)
                {
                    this._Type = value;
                    this.NotifyPropertyChanged("Type");
                }
            }
        }

        public string Balance
        {
            get { return this._Balance; }
            set
            {
                if (value != this._Balance)
                {
                    this._Balance = value;
                    this.NotifyPropertyChanged("Balance");
                }
            }
        }

        public string Manufacturer
        {
            get { return this._Manufacturer; }
            set
            {
                if (value != this._Manufacturer)
                {
                    this._Manufacturer = value;
                    this.NotifyPropertyChanged("Manufacturer");
                }
            }
        }

        public int ManufacturerGradeIndex
        {
            get { return this._ManufacturerGradeIndex; }
            set
            {
                if (value != this._ManufacturerGradeIndex)
                {
                    this._ManufacturerGradeIndex = value;
                    this.NotifyPropertyChanged("ManufacturerGradeIndex");
                }
            }
        }

        public string BodyPart
        {
            get { return this._BodyPart; }
            set
            {
                if (value != this._BodyPart)
                {
                    this._BodyPart = value;
                    this.NotifyPropertyChanged("BodyPart");
                }
            }
        }

        public string GripPart
        {
            get { return this._GripPart; }
            set
            {
                if (value != this._GripPart)
                {
                    this._GripPart = value;
                    this.NotifyPropertyChanged("GripPart");
                }
            }
        }

        public string BarrelPart
        {
            get { return this._BarrelPart; }
            set
            {
                if (value != this._BarrelPart)
                {
                    this._BarrelPart = value;
                    this.NotifyPropertyChanged("BarrelPart");
                }
            }
        }

        public string SightPart
        {
            get { return this._SightPart; }
            set
            {
                if (value != this._SightPart)
                {
                    this._SightPart = value;
                    this.NotifyPropertyChanged("SightPart");
                }
            }
        }

        public string StockPart
        {
            get { return this._StockPart; }
            set
            {
                if (value != this._StockPart)
                {
                    this._StockPart = value;
                    this.NotifyPropertyChanged("StockPart");
                }
            }
        }

        public string ElementalPart
        {
            get { return this._ElementalPart; }
            set
            {
                if (value != this._ElementalPart)
                {
                    this._ElementalPart = value;
                    this.NotifyPropertyChanged("ElementalPart");
                }
            }
        }

        public string Accessory1Part
        {
            get { return this._Accessory1Part; }
            set
            {
                if (value != this._Accessory1Part)
                {
                    this._Accessory1Part = value;
                    this.NotifyPropertyChanged("Accessory1Part");
                }
            }
        }

        public string Accessory2Part
        {
            get { return this._Accessory2Part; }
            set
            {
                if (value != this._Accessory2Part)
                {
                    this._Accessory2Part = value;
                    this.NotifyPropertyChanged("Accessory2Part");
                }
            }
        }

        public string MaterialPart
        {
            get { return this._MaterialPart; }
            set
            {
                if (value != this._MaterialPart)
                {
                    this._MaterialPart = value;
                    this.NotifyPropertyChanged("MaterialPart");
                }
            }
        }

        public string PrefixPart
        {
            get { return this._PrefixPart; }
            set
            {
                if (value != this._PrefixPart)
                {
                    this._PrefixPart = value;
                    this.NotifyPropertyChanged("PrefixPart");
                }
            }
        }

        public string TitlePart
        {
            get { return this._TitlePart; }
            set
            {
                if (value != this._TitlePart)
                {
                    this._TitlePart = value;
                    this.NotifyPropertyChanged("TitlePart");
                }
            }
        }

        public int GameStage
        {
            get { return this._GameStage; }
            set
            {
                if (value != this._GameStage)
                {
                    this._GameStage = value;
                    this.NotifyPropertyChanged("GameStage");
                }
            }
        }

        public int UniqueId
        {
            get { return this._UniqueId; }
            set
            {
                if (value != this._UniqueId)
                {
                    this._UniqueId = value;
                    this.NotifyPropertyChanged("UniqueId");
                }
            }
        }

        public int AssetLibrarySetId
        {
            get { return this._AssetLibrarySetId; }
            set
            {
                if (value != this._AssetLibrarySetId)
                {
                    this._AssetLibrarySetId = value;
                    this.NotifyPropertyChanged("AssetLibrarySetId");
                }
            }
        }
        #endregion

        #region ICloneable Members
        public virtual object Clone()
        {
            return new BaseWeapon()
            {
                Type = this.Type,
                Balance = this.Balance,
                Manufacturer = this.Manufacturer,
                ManufacturerGradeIndex = this.ManufacturerGradeIndex,
                BodyPart = this.BodyPart,
                GripPart = this.GripPart,
                BarrelPart = this.BarrelPart,
                SightPart = this.SightPart,
                StockPart = this.StockPart,
                ElementalPart = this.ElementalPart,
                Accessory1Part = this.Accessory1Part,
                Accessory2Part = this.Accessory2Part,
                MaterialPart = this.MaterialPart,
                PrefixPart = this.PrefixPart,
                TitlePart = this.TitlePart,
                GameStage = this.GameStage,
                UniqueId = this.UniqueId,
                AssetLibrarySetId = this.AssetLibrarySetId,
            };
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
