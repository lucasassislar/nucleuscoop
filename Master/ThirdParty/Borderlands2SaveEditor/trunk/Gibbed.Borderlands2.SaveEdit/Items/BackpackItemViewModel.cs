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
using Gibbed.Borderlands2.FileFormats.Items;
using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal class BackpackItemViewModel : BaseItemViewModel, IBackpackSlotViewModel
    {
        private readonly BackpackItem _Item;

        public IBackpackSlot BackpackSlot
        {
            get { return this._Item; }
        }

        public BackpackItemViewModel(BackpackItem item)
            : base(item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this._Item = item;
        }

        public int Quantity
        {
            get { return this._Item.Quantity; }
            set
            {
                this._Item.Quantity = value;
                this.NotifyOfPropertyChange(() => this.Quantity);
            }
        }

        public bool Equipped
        {
            get { return this._Item.Equipped; }
            set
            {
                this._Item.Equipped = value;
                this.NotifyOfPropertyChange(() => this.Equipped);
                this.NotifyOfPropertyChange(() => this.DisplayGroup);
            }
        }

        public PlayerMark Mark
        {
            get { return this._Item.Mark; }
            set
            {
                this._Item.Mark = value;
                this.NotifyOfPropertyChange(() => this.Mark);
            }
        }

        public override string DisplayName
        {
            get { return "Backpack Item"; }
        }

        public override string DisplayGroup
        {
            get
            {
                if (this.Equipped == true)
                {
                    return "Equipped";
                }

                return base.DisplayGroup;
            }
        }
    }
}
