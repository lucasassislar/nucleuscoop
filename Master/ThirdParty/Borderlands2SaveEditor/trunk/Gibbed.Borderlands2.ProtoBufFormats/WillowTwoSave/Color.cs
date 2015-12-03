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
    public struct Color : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private byte _A;
        private byte _R;
        private byte _G;
        private byte _B;
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
        public byte A
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

        [ProtoMember(2, IsRequired = true)]
        public byte R
        {
            get { return this._R; }
            set
            {
                if (value != this._R)
                {
                    this._R = value;
                    this.NotifyPropertyChanged("R");
                }
            }
        }

        [ProtoMember(3, IsRequired = true)]
        public byte G
        {
            get { return this._G; }
            set
            {
                if (value != this._G)
                {
                    this._G = value;
                    this.NotifyPropertyChanged("G");
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public byte B
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
        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            return (Color)obj == this;
        }

        public static bool operator ==(Color a, Color b)
        {
            return a._A == b._A &&
                   a._R == b._R &&
                   a._G == b._G &&
                   a._B == b._B;
        }

        public static bool operator !=(Color a, Color b)
        {
            return a._A != b._A ||
                   a._R != b._R ||
                   a._G != b._G ||
                   a._B != b._B;
        }

        public override int GetHashCode()
        {
            int hash = 37;
            hash = hash * 23 + this._A.GetHashCode();
            hash = hash * 23 + this._R.GetHashCode();
            hash = hash * 23 + this._G.GetHashCode();
            hash = hash * 23 + this._B.GetHashCode();
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
