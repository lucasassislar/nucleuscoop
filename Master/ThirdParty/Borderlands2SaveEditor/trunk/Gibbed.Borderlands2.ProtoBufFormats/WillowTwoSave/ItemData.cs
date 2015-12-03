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
    public class ItemData : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private string _Balance;
        private string _Type;
        private string _AlphaPart;
        private string _BetaPart;
        private string _GammaPart;
        private string _DeltaPart;
        private string _EpsilonPart;
        private string _ZetaPart;
        private string _EtaPart;
        private string _ThetaPart;
        private string _MaterialPart;
        private string _Manufacturer;
        private string _PrefixPart;
        private string _TitlePart;
        private int _Quantity;
        private int _ManufacturerGradeIndex;
        private bool _Equipped;
        private PlayerMark _Mark;
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

        [ProtoMember(3, IsRequired = true)]
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

        [ProtoMember(4, IsRequired = true)]
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

        [ProtoMember(5, IsRequired = true)]
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

        [ProtoMember(6, IsRequired = true)]
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

        [ProtoMember(7, IsRequired = true)]
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

        [ProtoMember(8, IsRequired = true)]
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

        [ProtoMember(9, IsRequired = true)]
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

        [ProtoMember(10, IsRequired = true)]
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

        [ProtoMember(11, IsRequired = true)]
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

        [ProtoMember(12, IsRequired = true)]
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

        [ProtoMember(13, IsRequired = true)]
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

        [ProtoMember(14, IsRequired = true)]
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

        [ProtoMember(15, IsRequired = true)]
        public int Quantity
        {
            get { return this._Quantity; }
            set
            {
                if (value != this._Quantity)
                {
                    this._Quantity = value;
                    this.NotifyPropertyChanged("Quantity");
                }
            }
        }

        [ProtoMember(16, IsRequired = true)]
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

        [ProtoMember(17, IsRequired = true)]
        public bool Equipped
        {
            get { return this._Equipped; }
            set
            {
                if (value != this._Equipped)
                {
                    this._Equipped = value;
                    this.NotifyPropertyChanged("Equipped");
                }
            }
        }

        [ProtoMember(18, IsRequired = true)]
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
