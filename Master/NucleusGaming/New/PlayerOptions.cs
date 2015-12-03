using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nucleus.Gaming;
using Nucleus.Gaming.Controls;
using System.Collections;
using SplitTool.Controls;

namespace Nucleus.Gaming
{
    public partial class PlayerOptions : ControlListBox, IUserInputForm
    {
        public PlayerOptions()
        {
        }

        private GameProfile profile;
        public void Initialize(UserGameInfo game, GameProfile profile)
        {
            this.profile = profile;
            this.Controls.Clear();

            var options = game.Game.Options;
            var vals = profile.Options;
            foreach (var opt in options)
            {
                CoolListControl cool = new CoolListControl();
                cool.Text = opt.Value.Name;
                cool.Description = opt.Value.Description;
                cool.Width = this.Width;

                this.Controls.Add(cool);

                // Check the value type and add a control for it
                if (opt.Value.Value is bool)
                {
                    SizeableCheckbox box = new SizeableCheckbox();
                    int border = 10;

                    box.Checked = (bool)vals[opt.Key];
                    box.Width = 40;
                    box.Height = 40;
                    box.Left = cool.Width - box.Width - border;
                    box.Top = (cool.Height / 2) - (box.Height / 2);
                    box.Anchor = AnchorStyles.Right;
                    cool.AddControl(box, false);

                    box.Tag = opt;
                    box.CheckedChanged += box_CheckedChanged;
                }
                else if (opt.Value.Value is int)
                {
                    NumericUpDown num = new NumericUpDown();
                    int border = 10;

                    num.Value = (int)vals[opt.Key];

                    num.Width = 150;
                    num.Height = 40;
                    num.Left = cool.Width - num.Width - border;
                    num.Top = (cool.Height / 2) - (num.Height / 2);
                    num.Anchor = AnchorStyles.Right;
                    cool.AddControl(num, false);

                    num.Tag = opt;
                    num.ValueChanged += num_ValueChanged;
                }
                else if (opt.Value.Value is Enum)
                {
                    ComboBox box = new ComboBox();
                    int border = 10;

                    Enum value = (Enum)vals[opt.Key];
                    Array values = Enum.GetValues(value.GetType());
                    for (int i = 0; i < values.Length; i++)
                    {
                        box.Items.Add(((IList)values)[i]);
                    }
                    box.SelectedIndex = box.Items.IndexOf(value);

                    box.Width = 150;
                    box.Height = 40;
                    box.Left = cool.Width - box.Width - border;
                    box.Top = (cool.Height / 2) - (box.Height / 2);
                    box.Anchor = AnchorStyles.Right;
                    cool.AddControl(box, false);

                    box.Tag = opt;
                    box.SelectedValueChanged += box_SelectedValueChanged;
                }
            }

            UpdateSizes();
        }

        private void ChangeOption(object tag, object value)
        {
            // boxing but wahtever
            var sel = (KeyValuePair<string, GameOption>)tag;
            GameOption option = sel.Value;
            profile.Options[sel.Key] = value;
        }

        void box_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox check = (ComboBox)sender;
            if (check.SelectedItem == null)
            {
                return;
            }
            ChangeOption(check.Tag, check.SelectedItem);
        }

        void num_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown check = (NumericUpDown)sender;
            ChangeOption(check.Tag, check.Value);
        }

        void box_CheckedChanged(object sender, EventArgs e)
        {
            SizeableCheckbox check = (SizeableCheckbox)sender;
            ChangeOption(check.Tag, check.Checked);
        }


        public bool CanProceed
        {
            get { return true; }
        }

        public bool CanPlay
        {
            get { return true; }
        }

        public event Action Proceed;

        public string Title
        {
            get { return "Player Options"; } 
        }
    }
}
