using Nucleus.DPI;
using Nucleus.Gaming;
using Nucleus.Platform.Windows.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;
#if false
using Nucleus.Gaming.Package;
#endif

namespace Nucleus.Coop.App.Controls {
    /// <summary>
    /// 
    /// </summary>
    public class HandlerControl : UserControl, IDynamicSized, IRadioControl {
#if false
        public GameHandlerMetadata Metadata { get; private set; }
#endif

        public string TitleText { get; set; }
        public bool EnableClicking { get; set; } = true;
        public Color SelectedColor { get; set; } = Color.FromArgb(80, 80, 80);
        public Color NotSelectedColor { get; set; } = Color.FromArgb(30, 30, 30);
        public Color HoverColor { get; set; } = Color.FromArgb(60, 60, 60);
        public Image Image {
            get { return this.picture.Image; }
            set { this.picture.Image = value; }
        }

        private bool isSelected;
        private PictureBox picture;
        private Label title;

#if true
        public HandlerControl() {
#else
        public HandlerControl(GameHandlerMetadata metadata) {
            Metadata = metadata;
#endif

            picture = new PictureBox();
            picture.SizeMode = PictureBoxSizeMode.StretchImage;

            title = new Label();

#if false
            if (metadata == null) {
                title.Text = "No handlers";
            } else {
                title.Text = metadata.ToString();
            }
#endif

            TitleText = title.Text;

            BackColor = Color.FromArgb(30, 30, 30);
            Size = new Size(200, 52);

            Controls.Add(picture);
            Controls.Add(title);

            DPI.DPIManager.Register(this);
        }

        ~HandlerControl() {
            DPI.DPIManager.Unregister(this);
        }

        public void UpdateSize(float scale) {
            if (IsDisposed) {
                DPI.DPIManager.Unregister(this);
                return;
            }

            SuspendLayout();

            int border = DPI.DPIManager.Adjust(4, scale);
            int dborder = border * 2;

            picture.Location = new Point(border, border);
            picture.Size = new Size(DPI.DPIManager.Adjust(44, scale), DPI.DPIManager.Adjust(44, scale));

            Height = DPI.DPIManager.Adjust(52, scale);

            Size labelSize = TextRenderer.MeasureText(TitleText, title.Font);
            float reservedSpaceLabel = this.Width - picture.Width;

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

            title.Location = new Point(picture.Width + picture.Left + border, (int)(height - lheight));

            ResumeLayout();
        }

        public void RadioSelected() {
            BackColor = SelectedColor;
            isSelected = true;
        }

        public void RadioUnselected() {
            BackColor = NotSelectedColor;
            isSelected = false;
        }

        public void UserOver() {
            BackColor = HoverColor;
        }

        public void UserLeave() {
            BackColor = isSelected ? SelectedColor : NotSelectedColor;
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
            UpdateSize(DPI.DPIManager.Scale);
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
