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
using System.ComponentModel;
using ProtoBuf;

namespace Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave
{
    [ProtoContract]
    public class PendingMissionRewards : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private string _Mission;
        private List<WeaponData> _WeaponRewards;
        private List<ItemData> _ItemRewards;
        private List<PackedWeaponDataOptional> _PackedWeaponRewards;
        private List<PackedItemDataOptional> _PackedItemRewards;
        private int _Unknown6;
        private int _Unknown7;
        #endregion

        #region IComposable Members
        private ComposeState _ComposeState = ComposeState.Composed;

        public void Compose()
        {
            if (this._ComposeState != ComposeState.Decomposed)
            {
                throw new InvalidOperationException();
            }
            this._ComposeState = ComposeState.Composed;

            if (this.WeaponRewards == null ||
                this.WeaponRewards.Count == 0)
            {
                this.WeaponRewards = null;
            }
            else
            {
                this.WeaponRewards.Compose();
            }

            if (this.ItemRewards == null ||
                this.ItemRewards.Count == 0)
            {
                this.ItemRewards = null;
            }
            else
            {
                this.ItemRewards.Compose();
            }

            if (this.PackedWeaponRewards == null ||
                this.PackedWeaponRewards.Count == 0)
            {
                this.PackedWeaponRewards = null;
            }
            else
            {
                this.PackedWeaponRewards.Compose();
            }

            if (this.PackedItemRewards == null ||
                this.PackedItemRewards.Count == 0)
            {
                this.PackedItemRewards = null;
            }
            else
            {
                this.PackedItemRewards.Compose();
            }
        }

        public void Decompose()
        {
            if (this._ComposeState != ComposeState.Composed)
            {
                throw new InvalidOperationException();
            }
            this._ComposeState = ComposeState.Decomposed;

            if (this.WeaponRewards == null)
            {
                this.WeaponRewards = new List<WeaponData>();
            }
            else
            {
                this.WeaponRewards.Decompose();
            }

            if (this.ItemRewards == null)
            {
                this.ItemRewards = new List<ItemData>();
            }
            else
            {
                this.ItemRewards.Decompose();
            }

            if (this.PackedWeaponRewards == null)
            {
                this.PackedWeaponRewards = new List<PackedWeaponDataOptional>();
            }
            else
            {
                this.PackedWeaponRewards.Decompose();
            }

            if (this.PackedItemRewards == null)
            {
                this.PackedItemRewards = new List<PackedItemDataOptional>();
            }
            else
            {
                this.PackedItemRewards.Decompose();
            }
        }
        #endregion

        #region Properties
        [ProtoMember(1, IsRequired = true)]
        public string Mission
        {
            get { return this._Mission; }
            set
            {
                if (value != this._Mission)
                {
                    this._Mission = value;
                    this.NotifyPropertyChanged("Mission");
                }
            }
        }

        [ProtoMember(2, IsRequired = false)]
        public List<WeaponData> WeaponRewards
        {
            get { return this._WeaponRewards; }
            set
            {
                if (value != this._WeaponRewards)
                {
                    this._WeaponRewards = value;
                    this.NotifyPropertyChanged("WeaponRewards");
                }
            }
        }

        [ProtoMember(3, IsRequired = false)]
        public List<ItemData> ItemRewards
        {
            get { return this._ItemRewards; }
            set
            {
                if (value != this._ItemRewards)
                {
                    this._ItemRewards = value;
                    this.NotifyPropertyChanged("ItemRewards");
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public List<PackedWeaponDataOptional> PackedWeaponRewards
        {
            get { return this._PackedWeaponRewards; }
            set
            {
                if (value != this._PackedWeaponRewards)
                {
                    this._PackedWeaponRewards = value;
                    this.NotifyPropertyChanged("PackedWeaponRewards");
                }
            }
        }

        [ProtoMember(5, IsRequired = true)]
        public List<PackedItemDataOptional> PackedItemRewards
        {
            get { return this._PackedItemRewards; }
            set
            {
                if (value != this._PackedItemRewards)
                {
                    this._PackedItemRewards = value;
                    this.NotifyPropertyChanged("PackedItemRewards");
                }
            }
        }

        [ProtoMember(6, IsRequired = true)]
        public int Unknown6
        {
            get { return this._Unknown6; }
            set
            {
                if (value != this._Unknown6)
                {
                    this._Unknown6 = value;
                    this.NotifyPropertyChanged("Unknown6");
                }
            }
        }

        [ProtoMember(7, IsRequired = true)]
        public int Unknown7
        {
            get { return this._Unknown7; }
            set
            {
                if (value != this._Unknown7)
                {
                    this._Unknown7 = value;
                    this.NotifyPropertyChanged("Unknown7");
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
