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
    public class ResourceData : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private string _Resource;
        private string _Pool;
        private float _Amount;
        private int _UpgradeLevel;
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
        public string Resource
        {
            get { return this._Resource; }
            set
            {
                if (value != this._Resource)
                {
                    this._Resource = value;
                    this.NotifyPropertyChanged("Resource");
                }
            }
        }

        [ProtoMember(2, IsRequired = true)]
        public string Pool
        {
            get { return this._Pool; }
            set
            {
                if (value != this._Pool)
                {
                    this._Pool = value;
                    this.NotifyPropertyChanged("Pool");
                }
            }
        }

        [ProtoMember(3, IsRequired = true)]
        public float Amount
        {
            get { return this._Amount; }
            set
            {
                if (value.Equals(this._Amount) == false)
                {
                    this._Amount = value;
                    this.NotifyPropertyChanged("Amount");
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public int UpgradeLevel
        {
            get { return this._UpgradeLevel; }
            set
            {
                if (value != this._UpgradeLevel)
                {
                    this._UpgradeLevel = value;
                    this.NotifyPropertyChanged("UpgradeLevel");
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

        public override string ToString()
        {
            return this.Resource ?? base.ToString();
        }
    }
}
