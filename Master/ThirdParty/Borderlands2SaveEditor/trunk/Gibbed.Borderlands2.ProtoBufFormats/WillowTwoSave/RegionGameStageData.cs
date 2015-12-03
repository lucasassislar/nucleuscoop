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
    public class RegionGameStageData : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private string _Region;
        private int _GameStage;
        private int _PlaythroughIndex;
        private int _Unknown4;
        private int _Unknown5;
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
        public string Region
        {
            get { return this._Region; }
            set
            {
                if (value != this._Region)
                {
                    this._Region = value;
                    this.NotifyPropertyChanged("Region");
                }
            }
        }

        [ProtoMember(2, IsRequired = true)]
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

        [ProtoMember(3, IsRequired = true)]
        public int PlaythroughIndex
        {
            get { return this._PlaythroughIndex; }
            set
            {
                if (value != this._PlaythroughIndex)
                {
                    this._PlaythroughIndex = value;
                    this.NotifyPropertyChanged("PlaythroughIndex");
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public int Unknown4
        {
            get { return this._Unknown4; }
            set
            {
                if (value != this._Unknown4)
                {
                    this._Unknown4 = value;
                    this.NotifyPropertyChanged("Unknown4");
                }
            }
        }

        [ProtoMember(5, IsRequired = true)]
        public int Unknown5
        {
            get { return this._Unknown5; }
            set
            {
                if (value != this._Unknown5)
                {
                    this._Unknown5 = value;
                    this.NotifyPropertyChanged("Unknown5");
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
