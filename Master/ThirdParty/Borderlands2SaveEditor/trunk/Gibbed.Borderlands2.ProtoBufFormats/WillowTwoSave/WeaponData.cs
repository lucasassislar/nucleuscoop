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
using ProtoBuf;

namespace Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave
{
    [ProtoContract]
    public class WeaponData : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private string _Balance;
        private string _Manufacturer;
        private string _Type;
        private string _BodyPart;
        private string _GripPart;
        private string _BarrelPart;
        private string _SightPart;
        private string _StockPart;
        private string _Unknown9; // unused?
        private string _Unknown10; // unused?
        private string _Unknown11; // unused?
        private string _Unknown12; // unused?
        private string _MaterialPart;
        private string _PrefixPart;
        private string _TitlePart;
        private int _Unknown16; // unused?
        private int _ManufacturerGradeIndex;
        private QuickWeaponSlot _QuickSlot;
        private PlayerMark _Mark;
        private string _ElementalPart;
        private string _Accessory1Part;
        private string _Accessory2Part;
        #endregion

        #region IComposable Members
        public void Compose()
        {
        }

        public void Decompose()
        {
        }
        #endregion

        #region Properties
        [ProtoMember(1, IsRequired = true)]
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

        [ProtoMember(2, IsRequired = true)]
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

        [ProtoMember(3, IsRequired = true)]
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

        [ProtoMember(4, IsRequired = true)]
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

        [ProtoMember(5, IsRequired = true)]
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

        [ProtoMember(6, IsRequired = true)]
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

        [ProtoMember(7, IsRequired = true)]
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

        [ProtoMember(8, IsRequired = true)]
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

        [ProtoMember(9, IsRequired = true)]
        public string Unknown9
        {
            get { return this._Unknown9; }
            set
            {
                if (value != this._Unknown9)
                {
                    this._Unknown9 = value;
                    this.NotifyPropertyChanged("Unknown9");
                }
            }
        }

        [ProtoMember(10, IsRequired = true)]
        public string Unknown10
        {
            get { return this._Unknown10; }
            set
            {
                if (value != this._Unknown10)
                {
                    this._Unknown10 = value;
                    this.NotifyPropertyChanged("Unknown10");
                }
            }
        }

        [ProtoMember(11, IsRequired = true)]
        public string Unknown11
        {
            get { return this._Unknown11; }
            set
            {
                if (value != this._Unknown11)
                {
                    this._Unknown11 = value;
                    this.NotifyPropertyChanged("Unknown11");
                }
            }
        }

        [ProtoMember(12, IsRequired = true)]
        public string Unknown12
        {
            get { return this._Unknown12; }
            set
            {
                if (value != this._Unknown12)
                {
                    this._Unknown12 = value;
                    this.NotifyPropertyChanged("Unknown12");
                }
            }
        }

        [ProtoMember(13, IsRequired = true)]
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

        [ProtoMember(14, IsRequired = true)]
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

        [ProtoMember(15, IsRequired = true)]
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

        [ProtoMember(16, IsRequired = true)]
        public int Unknown16
        {
            get { return this._Unknown16; }
            set
            {
                if (value != this._Unknown16)
                {
                    this._Unknown16 = value;
                    this.NotifyPropertyChanged("Unknown16");
                }
            }
        }

        [ProtoMember(17, IsRequired = true)]
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

        [ProtoMember(18, IsRequired = true)]
        public QuickWeaponSlot QuickSlot
        {
            get { return this._QuickSlot; }
            set
            {
                if (value != this._QuickSlot)
                {
                    this._QuickSlot = value;
                    this.NotifyPropertyChanged("QuickSlot");
                }
            }
        }

        [ProtoMember(19, IsRequired = true)]
        public PlayerMark Mark
        {
            get { return this._Mark; }
            set
            {
                if (value != this._Mark)
                {
                    this._Mark = value;
                    this.NotifyPropertyChanged("Mark");
                }
            }
        }

        [ProtoMember(20, IsRequired = true)]
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

        [ProtoMember(21, IsRequired = true)]
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

        [ProtoMember(22, IsRequired = true)]
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
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
