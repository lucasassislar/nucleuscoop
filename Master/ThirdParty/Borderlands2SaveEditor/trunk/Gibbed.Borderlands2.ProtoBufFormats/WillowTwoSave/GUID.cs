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
    [ProtoContract(Name = "GUID")]
    public struct Guid : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private uint _A;
        private uint _B;
        private uint _C;
        private uint _D;
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
        [ProtoMember(1, DataFormat = DataFormat.FixedSize, IsRequired = true)]
        public uint A
        {
            get { return this._A; }
            set
            {
                if (value != this._A)
                {
                    this._A = value;
                    this.NotifyPropertyChanged("A");
                }
            }
        }

        [ProtoMember(2, DataFormat = DataFormat.FixedSize, IsRequired = true)]
        public uint B
        {
            get { return this._B; }
            set
            {
                if (value != this._B)
                {
                    this._B = value;
                    this.NotifyPropertyChanged("B");
                }
            }
        }

        [ProtoMember(3, DataFormat = DataFormat.FixedSize, IsRequired = true)]
        public uint C
        {
            get { return this._C; }
            set
            {
                if (value != this._C)
                {
                    this._C = value;
                    this.NotifyPropertyChanged("C");
                }
            }
        }

        [ProtoMember(4, DataFormat = DataFormat.FixedSize, IsRequired = true)]
        public uint D
        {
            get { return this._D; }
            set
            {
                if (value != this._D)
                {
                    this._D = value;
                    this.NotifyPropertyChanged("D");
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

            return (Guid)obj == this;
        }

        public static bool operator ==(Guid a, Guid b)
        {
            return a._A == b._A &&
                   a._B == b._B &&
                   a._C == b._C &&
                   a._D == b._D;
        }

        public static bool operator !=(Guid a, Guid b)
        {
            return a._A != b._A ||
                   a._B != b._B ||
                   a._C != b._C ||
                   a._D != b._D;
        }

        public override int GetHashCode()
        {
            int hash = 37;
            hash = hash * 23 + this._A.GetHashCode();
            hash = hash * 23 + this._B.GetHashCode();
            hash = hash * 23 + this._C.GetHashCode();
            hash = hash * 23 + this._D.GetHashCode();
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
