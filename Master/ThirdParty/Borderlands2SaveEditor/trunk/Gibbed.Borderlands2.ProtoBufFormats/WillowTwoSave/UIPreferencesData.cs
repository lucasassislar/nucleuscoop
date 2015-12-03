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
using System.ComponentModel;
using ProtoBuf;

namespace Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave
{
    [ProtoContract]
    public class UIPreferencesData : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private byte[] _CharacterName;
        private Color _PrimaryColor;
        private Color _SecondaryColor;
        private Color _TertiaryColor;
        #endregion

        #region IComposable Members
        private ComposeState _ComposeState;

        public void Compose()
        {
            if (this._ComposeState != ComposeState.Decomposed)
            {
                throw new InvalidOperationException();
            }
            this._ComposeState = ComposeState.Composed;

            this.PrimaryColor.Compose();
            this.SecondaryColor.Compose();
            this.TertiaryColor.Compose();
        }

        public void Decompose()
        {
            if (this._ComposeState != ComposeState.Composed)
            {
                throw new InvalidOperationException();
            }
            this._ComposeState = ComposeState.Decomposed;

            this.PrimaryColor.Decompose();
            this.SecondaryColor.Decompose();
            this.TertiaryColor.Decompose();
        }
        #endregion

        #region Properties
        [ProtoMember(1, IsRequired = true)]
        public byte[] CharacterName
        {
            get { return this._CharacterName; }
            set
            {
                if (value != this._CharacterName)
                {
                    this._CharacterName = value;
                    this.NotifyPropertyChanged("CharacterName");
                }
            }
        }

        [ProtoMember(2, IsRequired = true)]
        public Color PrimaryColor
        {
            get { return this._PrimaryColor; }
            set
            {
                if (value != this._PrimaryColor)
                {
                    this._PrimaryColor = value;
                    this.NotifyPropertyChanged("PrimaryColor");
                }
            }
        }

        [ProtoMember(3, IsRequired = true)]
        public Color SecondaryColor
        {
            get { return this._SecondaryColor; }
            set
            {
                if (value != this._SecondaryColor)
                {
                    this._SecondaryColor = value;
                    this.NotifyPropertyChanged("SecondaryColor");
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public Color TertiaryColor
        {
            get { return this._TertiaryColor; }
            set
            {
                if (value != this._TertiaryColor)
                {
                    this._TertiaryColor = value;
                    this.NotifyPropertyChanged("TertiaryColor");
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
