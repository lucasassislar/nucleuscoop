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
    public class SkillData : IComposable, INotifyPropertyChanged
    {
        #region Fields
        private string _Skill;
        private int _Grade;
        private int _GradePoints;
        private int _EquippedSlotIndex;
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
        public string Skill
        {
            get { return this._Skill; }
            set
            {
                if (value != this._Skill)
                {
                    this._Skill = value;
                    this.NotifyPropertyChanged("Skill");
                }
            }
        }

        [ProtoMember(2, IsRequired = true)]
        public int Grade
        {
            get { return this._Grade; }
            set
            {
                if (value != this._Grade)
                {
                    this._Grade = value;
                    this.NotifyPropertyChanged("Grade");
                }
            }
        }

        [ProtoMember(3, IsRequired = true)]
        public int GradePoints
        {
            get { return this._GradePoints; }
            set
            {
                if (value != this._GradePoints)
                {
                    this._GradePoints = value;
                    this.NotifyPropertyChanged("GradePoints");
                }
            }
        }

        [ProtoMember(4, IsRequired = true)]
        public int EquippedSlotIndex
        {
            get { return this._EquippedSlotIndex; }
            set
            {
                if (value != this._EquippedSlotIndex)
                {
                    this._EquippedSlotIndex = value;
                    this.NotifyPropertyChanged("EquippedSlotIndex");
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
            return this.Skill ?? base.ToString();
        }
    }
}
