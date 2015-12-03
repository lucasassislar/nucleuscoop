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

using System.ComponentModel.Composition;
using Caliburn.Micro;
using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.SaveEdit
{
    [Export(typeof(CurrencyOnHandViewModel))]
    internal class CurrencyOnHandViewModel : PropertyChangedBase
    {
        #region Fields
        private int _Credits;
        private int _Eridium;
        private int _SeraphCrystals;
        private int _ReservedA;
        private int _ReservedB;
        private int _ReservedC;
        private int _ReservedD;
        private int _ReservedE;
        private int _ReservedF;
        private int _ReservedG;
        private int _ReservedH;
        private int _ReservedI;
        private int _ReservedJ;
        #endregion

        #region Properties
        public int Credits
        {
            get { return this._Credits; }
            set
            {
                if (this._Credits != value)
                {
                    this._Credits = value;
                    this.NotifyOfPropertyChange(() => this.Credits);
                }
            }
        }

        public int Eridium
        {
            get { return this._Eridium; }
            set
            {
                if (this._Eridium != value)
                {
                    this._Eridium = value;
                    this.NotifyOfPropertyChange(() => this.Eridium);
                }
            }
        }

        public int SeraphCrystals
        {
            get { return this._SeraphCrystals; }
            set
            {
                if (this._SeraphCrystals != value)
                {
                    this._SeraphCrystals = value;
                    this.NotifyOfPropertyChange(() => this.SeraphCrystals);
                }
            }
        }

        public int ReservedA
        {
            get { return this._ReservedA; }
            set
            {
                if (this._ReservedA != value)
                {
                    this._ReservedA = value;
                    this.NotifyOfPropertyChange(() => this.ReservedA);
                }
            }
        }

        public int ReservedB
        {
            get { return this._ReservedB; }
            set
            {
                if (this._ReservedB != value)
                {
                    this._ReservedB = value;
                    this.NotifyOfPropertyChange(() => this.ReservedB);
                }
            }
        }

        public int ReservedC
        {
            get { return this._ReservedC; }
            set
            {
                if (this._ReservedC != value)
                {
                    this._ReservedC = value;
                    this.NotifyOfPropertyChange(() => this.ReservedC);
                }
            }
        }

        public int ReservedD
        {
            get { return this._ReservedD; }
            set
            {
                if (this._ReservedD != value)
                {
                    this._ReservedD = value;
                    this.NotifyOfPropertyChange(() => this.ReservedD);
                }
            }
        }

        public int ReservedE
        {
            get { return this._ReservedE; }
            set
            {
                if (this._ReservedE != value)
                {
                    this._ReservedE = value;
                    this.NotifyOfPropertyChange(() => this.ReservedE);
                }
            }
        }

        public int ReservedF
        {
            get { return this._ReservedF; }
            set
            {
                if (this._ReservedF != value)
                {
                    this._ReservedF = value;
                    this.NotifyOfPropertyChange(() => this.ReservedF);
                }
            }
        }

        public int ReservedG
        {
            get { return this._ReservedG; }
            set
            {
                if (this._ReservedG != value)
                {
                    this._ReservedG = value;
                    this.NotifyOfPropertyChange(() => this.ReservedG);
                }
            }
        }

        public int ReservedH
        {
            get { return this._ReservedH; }
            set
            {
                if (this._ReservedH != value)
                {
                    this._ReservedH = value;
                    this.NotifyOfPropertyChange(() => this.ReservedH);
                }
            }
        }

        public int ReservedI
        {
            get { return this._ReservedI; }
            set
            {
                if (this._ReservedI != value)
                {
                    this._ReservedI = value;
                    this.NotifyOfPropertyChange(() => this.ReservedI);
                }
            }
        }

        public int ReservedJ
        {
            get { return this._ReservedJ; }
            set
            {
                if (this._ReservedJ != value)
                {
                    this._ReservedJ = value;
                    this.NotifyOfPropertyChange(() => this.ReservedJ);
                }
            }
        }
        #endregion

        [ImportingConstructor]
        public CurrencyOnHandViewModel(IEventAggregator events)
        {
            events.Subscribe(this);
        }

        public void ImportData(WillowTwoPlayerSaveGame saveGame)
        {
            this.Credits = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 0 ? saveGame.CurrencyOnHand[0] : 0;
            this.Eridium = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 1 ? saveGame.CurrencyOnHand[1] : 0;
            this.SeraphCrystals = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 2 ? saveGame.CurrencyOnHand[2] : 0;
            this.ReservedA = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 3 ? saveGame.CurrencyOnHand[3] : 0;
            this.ReservedB = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 4 ? saveGame.CurrencyOnHand[4] : 0;
            this.ReservedC = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 5 ? saveGame.CurrencyOnHand[5] : 0;
            this.ReservedD = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 6 ? saveGame.CurrencyOnHand[6] : 0;
            this.ReservedE = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 7 ? saveGame.CurrencyOnHand[7] : 0;
            this.ReservedF = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 8 ? saveGame.CurrencyOnHand[8] : 0;
            this.ReservedG = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 9 ? saveGame.CurrencyOnHand[9] : 0;
            this.ReservedH = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 10 ? saveGame.CurrencyOnHand[10] : 0;
            this.ReservedI = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 11 ? saveGame.CurrencyOnHand[11] : 0;
            this.ReservedJ = saveGame.CurrencyOnHand != null && saveGame.CurrencyOnHand.Count > 12 ? saveGame.CurrencyOnHand[12] : 0;
        }

        public void ExportData(WillowTwoPlayerSaveGame saveGame)
        {
            saveGame.CurrencyOnHand.Clear();
            saveGame.CurrencyOnHand.Add(this.Credits);
            saveGame.CurrencyOnHand.Add(this.Eridium);
            saveGame.CurrencyOnHand.Add(this.SeraphCrystals);
            saveGame.CurrencyOnHand.Add(this.ReservedA);
            saveGame.CurrencyOnHand.Add(this.ReservedB);
            saveGame.CurrencyOnHand.Add(this.ReservedC);
            saveGame.CurrencyOnHand.Add(this.ReservedD);
            saveGame.CurrencyOnHand.Add(this.ReservedE);
            saveGame.CurrencyOnHand.Add(this.ReservedF);
            saveGame.CurrencyOnHand.Add(this.ReservedG);
            saveGame.CurrencyOnHand.Add(this.ReservedH);
            saveGame.CurrencyOnHand.Add(this.ReservedI);
            saveGame.CurrencyOnHand.Add(this.ReservedJ);
        }
    }
}
