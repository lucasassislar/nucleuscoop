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

using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.FileFormats.Items
{
    public sealed class BackpackWeapon : BaseWeapon, IBackpackSlot
    {
        #region Fields
        private QuickWeaponSlot _QuickSlot = QuickWeaponSlot.None;
        private PlayerMark _Mark = PlayerMark.Standard;
        #endregion

        #region Properties
        public QuickWeaponSlot QuickSlot
        {
            get { return this._QuickSlot; }
            set
            {
                if (value != this._QuickSlot)
                {
                    this._QuickSlot = value;
                    this.NotifyPropertyChanged("QuickSlot");
                }
            }
        }

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

        #region ICloneable Members
        public override object Clone()
        {
            return new BackpackWeapon()
            {
                Type = this.Type,
                Balance = this.Balance,
                Manufacturer = this.Manufacturer,
                ManufacturerGradeIndex = this.ManufacturerGradeIndex,
                BodyPart = this.BodyPart,
                GripPart = this.GripPart,
                BarrelPart = this.BarrelPart,
                SightPart = this.SightPart,
                StockPart = this.StockPart,
                ElementalPart = this.ElementalPart,
                Accessory1Part = this.Accessory1Part,
                Accessory2Part = this.Accessory2Part,
                MaterialPart = this.MaterialPart,
                PrefixPart = this.PrefixPart,
                TitlePart = this.TitlePart,
                GameStage = this.GameStage,
                UniqueId = this.UniqueId,
                AssetLibrarySetId = this.AssetLibrarySetId,
                QuickSlot = this.QuickSlot,
                Mark = this.Mark,
            };
        }
        #endregion
    }
}
