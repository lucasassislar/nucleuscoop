using Nucleus.Gaming;
using Nucleus.Gaming.Platform.Windows.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Controls {
    public class CheckedTextControl : UserControl, IDynamicSized, IRadioControl {
        public object SharedData { get; set; }
        public Color ColorSelected { get; set; } = Color.FromArgb(66, 70, 77);
        public Color ColorUnselected { get; set; } = Color.FromArgb(47, 49, 54);
        public Color ColorMouseOver { get; set; } = Color.FromArgb(54, 57, 63);
        public bool EnableClicking { get; set; } = true;

        public bool Checked {
            get { return checkbox.Checked; }
            set { checkbox.Checked = value; }
        }

        public string TitleText { get; protected set; }

        private SizeableCheckbox checkbox;
        private Label title;
        private bool isSelected;

        public CheckedTextControl() {
            checkbox = new SizeableCheckbox();

            title = new Label();
            checkbox = new SizeableCheckbox();

            BackColor = ColorUnselected;
            Size = new Size(200, 52);

            Controls.Add(checkbox);
            Controls.Add(title);

            DPIManager.Register(this);
        }

        public void UpdateTitleText(string titleText) {
            TitleText = titleText;
            title.Text = titleText;
        }

        public void UpdateSize(float scale) {
            if (IsDisposed) {
                DPIManager.Unregister(this);
                return;
            }

            SuspendLayout();

            int border = DPIManager.Adjust(8, scale);
            int dborder = border * 2;

            checkbox.Location = new Point(12, 11);
            checkbox.Size = new Size(30, 30);

            Height = DPIManager.Adjust(52, scale);

            Size labelSize = TextRenderer.MeasureText(TitleText, title.Font);
            float reservedSpaceLabel = this.Width - checkbox.Width;

            if (labelSize.Width > reservedSpaceLabel) {
                // make text smaller
                int charSize = TextRenderer.MeasureText("g", title.Font).Width;
                int toRemove = (int)((reservedSpaceLabel - labelSize.Width) / (float)charSize);
                toRemove = Math.Max(toRemove + 3, 7);
                title.Text = TitleText.Remove(TitleText.Length - toRemove, toRemove) + "...";
            } else {
                title.Text = TitleText;
            }
            title.Size = labelSize;

            float height = this.Height / 2.0f;
            float lheight = labelSize.Height / 2.0f;

            title.Location = new Point(checkbox.Width + checkbox.Left + border, (int)(height - lheight));

            ResumeLayout();
        }

        public void RadioSelected() {
            BackColor = ColorSelected;
            isSelected = true;
        }

        public void RadioUnselected() {
            BackColor = ColorUnselected;
            isSelected = false;
        }

        public void UserOver() {
            BackColor = ColorMouseOver;
        }

        public void UserLeave() {
            BackColor = isSelected ? ColorSelected : ColorUnselected;
        }

        public override string ToString() {
            return Text;
        }

        protected override void OnControlAdded(ControlEventArgs e) {
            base.OnControlAdded(e);

            Control c = e.Control;
            c.Click += C_Click;
            c.MouseEnter += C_MouseEnter;
            c.MouseLeave += C_MouseLeave;
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            UpdateSize(DPIManager.Scale);
        }

        private void C_MouseEnter(object sender, EventArgs e) {
            OnMouseEnter(e);
        }

        private void C_MouseLeave(object sender, EventArgs e) {
            OnMouseLeave(e);
        }

        private void C_Click(object sender, EventArgs e) {
            OnClick(e);
        }
    }
}
