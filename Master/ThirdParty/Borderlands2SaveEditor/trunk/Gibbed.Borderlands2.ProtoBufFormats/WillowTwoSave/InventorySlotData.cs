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
    public struct InventorySlotData : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private int _InventorySlotMax;
        private int _WeaponReadyMax;
        private int _NumQuickSlotsFlourished;
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
        public int InventorySlotMax
        {
            get { return this._InventorySlotMax; }
            set
            {
                if (value != this._InventorySlotMax)
                {
                    this._InventorySlotMax = value;
                    this.NotifyPropertyChanged("InventorySlotMax");
                }
            }
        }

        [ProtoMember(2, IsRequired = true)]
        public int WeaponReadyMax
        {
            get { return this._WeaponReadyMax; }
            set
            {
                if (value != this._WeaponReadyMax)
                {
                    this._WeaponReadyMax = value;
                    this.NotifyPropertyChanged("WeaponReadyMax");
                }
            }
        }

        [ProtoMember(3, IsRequired = true)]
        public int NumQuickSlotsFlourished
        {
            get { return this._NumQuickSlotsFlourished; }
            set
            {
                if (value != this._NumQuickSlotsFlourished)
                {
                    this._NumQuickSlotsFlourished = value;
                    this.NotifyPropertyChanged("NumQuickSlotsFlourished");
                }
            }
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            return (InventorySlotData)obj == this;
        }

        public static bool operator ==(InventorySlotData a, InventorySlotData b)
        {
            return a._InventorySlotMax == b._InventorySlotMax &&
                   a._WeaponReadyMax == b._WeaponReadyMax &&
                   a._NumQuickSlotsFlourished == b._NumQuickSlotsFlourished;
        }

        public static bool operator !=(InventorySlotData a, InventorySlotData b)
        {
            return a._InventorySlotMax != b._InventorySlotMax ||
                   a._InventorySlotMax != b._InventorySlotMax ||
                   a._NumQuickSlotsFlourished != b._NumQuickSlotsFlourished;
        }

        public override int GetHashCode()
        {
            int hash = 37;
            hash = hash * 23 + this._InventorySlotMax.GetHashCode();
            hash = hash * 23 + this._InventorySlotMax.GetHashCode();
            hash = hash * 23 + this._NumQuickSlotsFlourished.GetHashCode();
            return hash;
        }

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
