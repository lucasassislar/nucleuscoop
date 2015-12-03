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
    public class MissionData : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private string _Mission;
        private MissionStatus _Status;
        private bool _IsFromDLC;
        private int _DLCPackageId;
        private List<int> _ObjectivesProgress;
        private int _ActiveObjectiveSetIndex;
        private List<int> _SubObjectiveSetIndexes;
        private bool _NeedsRewards;
        private int _Unknown9;
        private bool _HeardKickoff;
        private int _GameStage;
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

            if (this.ObjectivesProgress == null ||
                this.ObjectivesProgress.Count == 0)
            {
                this.ObjectivesProgress = null;
            }

            if (this.SubObjectiveSetIndexes == null ||
                this.SubObjectiveSetIndexes.Count == 0)
            {
                this.SubObjectiveSetIndexes = null;
            }
        }

        public void Decompose()
        {
            if (this._ComposeState != ComposeState.Composed)
            {
                throw new InvalidOperationException();
            }
            this._ComposeState = ComposeState.Decomposed;

            if (this.ObjectivesProgress == null)
            {
                this.ObjectivesProgress = new List<int>();
            }

            if (this.SubObjectiveSetIndexes == null)
            {
                this.SubObjectiveSetIndexes = new List<int>();
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

        [ProtoMember(2, IsRequired = true)]
        public MissionStatus Status
        {
            get { return this._Status; }
            set
            {
                if (value != this._Status)
                {
                    this._Status = value;
                    this.NotifyPropertyChanged("Status");
                }
            }
        }

        [ProtoMember(3, IsRequired = true)]
        public bool IsFromDLC
        {
            get { return this._IsFromDLC; }
            set
            {
                if (value != this._IsFromDLC)
                {
                    this._IsFromDLC = value;
                    this.NotifyPropertyChanged("IsFromDLC");
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public int DLCPackageId
        {
            get { return this._DLCPackageId; }
            set
            {
                if (value != this._DLCPackageId)
                {
                    this._DLCPackageId = value;
                    this.NotifyPropertyChanged("DLCPackageId");
                }
            }
        }

        [ProtoMember(5, IsRequired = true, IsPacked = true)]
        public List<int> ObjectivesProgress
        {
            get { return this._ObjectivesProgress; }
            set
            {
                if (value != this._ObjectivesProgress)
                {
                    this._ObjectivesProgress = value;
                    this.NotifyPropertyChanged("ObjectivesProgress");
                }
            }
        }

        [ProtoMember(6, IsRequired = true)]
        public int ActiveObjectiveSetIndex
        {
            get { return this._ActiveObjectiveSetIndex; }
            set
            {
                if (value != this._ActiveObjectiveSetIndex)
                {
                    this._ActiveObjectiveSetIndex = value;
                    this.NotifyPropertyChanged("ActiveObjectiveSetIndex");
                }
            }
        }

        [ProtoMember(7, IsRequired = true, IsPacked = true)]
        public List<int> SubObjectiveSetIndexes
        {
            get { return this._SubObjectiveSetIndexes; }
            set
            {
                if (value != this._SubObjectiveSetIndexes)
                {
                    this._SubObjectiveSetIndexes = value;
                    this.NotifyPropertyChanged("SubObjectiveSetIndexes");
                }
            }
        }

        [ProtoMember(8, IsRequired = true)]
        public bool NeedsRewards
        {
            get { return this._NeedsRewards; }
            set
            {
                if (value != this._NeedsRewards)
                {
                    this._NeedsRewards = value;
                    this.NotifyPropertyChanged("NeedsRewards");
                }
            }
        }

        [ProtoMember(9, IsRequired = false)]
        public int Unknown9
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
        public bool HeardKickoff
        {
            get { return this._HeardKickoff; }
            set
            {
                if (value != this._HeardKickoff)
                {
                    this._HeardKickoff = value;
                    this.NotifyPropertyChanged("HeardKickoff");
                }
            }
        }

        [ProtoMember(11, IsRequired = true)]
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
            return this.Mission ?? base.ToString();
        }
    }
}
