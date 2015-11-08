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

namespace SplitTool.Controls
{
    public partial class PlayerOptions : ControlListBox, ICanProceed
    {
        public PlayerOptions()
        {
            InitializeComponent();
        }

        public void UpdateItems(GameInfo prof)
        {
            this.Controls.Clear();

            var options = prof.Options;
            foreach (var opt in options.Values)
            {
                CoolListControl cool = new CoolListControl();
                cool.Text = opt.Name;
                cool.Description = opt.Description;
                cool.Width = this.Width;

                this.Controls.Add(cool);

                // Check the value type and add a control for it
                if (opt.Value is bool)
                {
                    SizeableCheckbox box = new SizeableCheckbox();
                    int border = 10;

                    box.Checked = (bool)opt.Value;
                    box.Width = 40;
                    box.Height = 40;
                    box.Left = cool.Width - box.Width - border;
                    box.Top = (cool.Height / 2) - (box.Height / 2);
                    box.Anchor = AnchorStyles.Right;
                    cool.AddControl(box, false);

                    box.Tag = opt;
                    box.CheckedChanged += box_CheckedChanged;
                }
                else if (opt.Value is int)
                {
                    NumericUpDown num = new NumericUpDown();
                    int border = 10;

                    num.Value = (int)opt.Value;

                    num.Width = 150;
                    num.Height = 40;
                    num.Left = cool.Width - num.Width - border;
                    num.Top = (cool.Height / 2) - (num.Height / 2);
                    num.Anchor = AnchorStyles.Right;
                    cool.AddControl(num, false);

                    num.Tag = opt;
                    num.ValueChanged += num_ValueChanged;
                }
                else if (opt.Value is Enum)
                {
                    ComboBox box = new ComboBox();
                    int border = 10;

                    Array values = Enum.GetValues(opt.Value.GetType());
                    for (int i = 0; i < values.Length; i++)
                    {
                        box.Items.Add(((IList)values)[i]);
                    }
                    box.SelectedIndex = values.Length - 1;

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
        }

        void box_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox check = (ComboBox)sender;
            GameOption option = (GameOption)check.Tag;
            option.Value = check.SelectedValue;
        }

        void num_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown check = (NumericUpDown)sender;
            GameOption option = (GameOption)check.Tag;
            option.Value = (int)check.Value;
        }

        void box_CheckedChanged(object sender, EventArgs e)
        {
            SizeableCheckbox check = (SizeableCheckbox)sender;
            GameOption option = (GameOption)check.Tag;
            option.Value = check.Checked;
        }

        public bool CanProceed
        {
            get { return true; }
        }


        public void Restart()
        {
        }


        public string StepTitle
        {
            get { return "Game Options"; }
        }

        public bool AutoProceed
        {
            get { return false; }
        }

        public void AutoProceeded()
        {
        }
    }
}
