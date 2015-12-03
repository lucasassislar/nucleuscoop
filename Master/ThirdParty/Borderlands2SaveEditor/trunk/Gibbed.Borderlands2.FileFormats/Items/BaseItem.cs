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
    public class BaseItem : IBaseSlot, IPackable, INotifyPropertyChanged
    {
        #region Fields
        private string _Type = "None";
        private string _Balance = "None";
        private string _Manufacturer = "None";
        private int _ManufacturerGradeIndex;
        private string _AlphaPart = "None";
        private string _BetaPart = "None";
        private string _GammaPart = "None";
        private string _DeltaPart = "None";
        private string _EpsilonPart = "None";
        private string _ZetaPart = "None";
        private string _EtaPart = "None";
        private string _ThetaPart = "None";
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

            this.Type = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemTypes);
            this.Balance = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.BalanceDefs);
            this.Manufacturer = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.Manufacturers);
            this.ManufacturerGradeIndex = reader.ReadInt32(7);
            this.GameStage = reader.ReadInt32(7);
            this.AlphaPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.BetaPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.GammaPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.DeltaPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.EpsilonPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.ZetaPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.EtaPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.ThetaPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.MaterialPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.PrefixPart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
            this.TitlePart = alm.Decode(reader, this.AssetLibrarySetId, AssetGroup.ItemParts);
        }

        public void Write(BitWriter writer)
        {
            var alm = InfoManager.AssetLibraryManager;

            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemTypes, this.Type);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.BalanceDefs, this.Balance);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.Manufacturers, this.Manufacturer);
            writer.WriteInt32(this.ManufacturerGradeIndex, 7);
            writer.WriteInt32(this.GameStage, 7);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.AlphaPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.BetaPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.GammaPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.DeltaPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.EpsilonPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.ZetaPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.EtaPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.ThetaPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.MaterialPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.PrefixPart);
            alm.Encode(writer, this.AssetLibrarySetId, AssetGroup.ItemParts, this.TitlePart);
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

        public string AlphaPart
        {
            get { return this._AlphaPart; }
            set
            {
                if (value != this._AlphaPart)
                {
                    this._AlphaPart = value;
                    this.NotifyPropertyChanged("AlphaPart");
                }
            }
        }

        public string BetaPart
        {
            get { return this._BetaPart; }
            set
            {
                if (value != this._BetaPart)
                {
                    this._BetaPart = value;
                    this.NotifyPropertyChanged("BetaPart");
                }
            }
        }

        public string GammaPart
        {
            get { return this._GammaPart; }
            set
            {
                if (value != this._GammaPart)
                {
                    this._GammaPart = value;
                    this.NotifyPropertyChanged("GammaPart");
                }
            }
        }

        public string DeltaPart
        {
            get { return this._DeltaPart; }
            set
            {
                if (value != this._DeltaPart)
                {
                    this._DeltaPart = value;
                    this.NotifyPropertyChanged("DeltaPart");
                }
            }
        }

        public string EpsilonPart
        {
            get { return this._EpsilonPart; }
            set
            {
                if (value != this._EpsilonPart)
                {
                    this._EpsilonPart = value;
                    this.NotifyPropertyChanged("EpsilonPart");
                }
            }
        }

        public string ZetaPart
        {
            get { return this._ZetaPart; }
            set
            {
                if (value != this._ZetaPart)
                {
                    this._ZetaPart = value;
                    this.NotifyPropertyChanged("ZetaPart");
                }
            }
        }

        public string EtaPart
        {
            get { return this._EtaPart; }
            set
            {
                if (value != this._EtaPart)
                {
                    this._EtaPart = value;
                    this.NotifyPropertyChanged("EtaPart");
                }
            }
        }

        public string ThetaPart
        {
            get { return this._ThetaPart; }
            set
            {
                if (value != this._ThetaPart)
                {
                    this._ThetaPart = value;
                    this.NotifyPropertyChanged("ThetaPart");
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
            return new BaseItem()
            {
                Type = this.Type,
                Balance = this.Balance,
                Manufacturer = this.Manufacturer,
                ManufacturerGradeIndex = this.ManufacturerGradeIndex,
                AlphaPart = this.AlphaPart,
                BetaPart = this.BetaPart,
                GammaPart = this.GammaPart,
                DeltaPart = this.DeltaPart,
                EpsilonPart = this.EpsilonPart,
                ZetaPart = this.ZetaPart,
                EtaPart = this.EtaPart,
                ThetaPart = this.ThetaPart,
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
