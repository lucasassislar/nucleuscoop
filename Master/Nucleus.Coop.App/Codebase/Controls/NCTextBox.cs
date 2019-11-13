using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Coop.App.Controls {
    public class NCTextBox : UserControl {
        public event EventHandler TextChanged;
        public event KeyEventHandler TextBoxKeyDown;

        public TextBox TextBox {
            get { return textBox; }
        }

        public int BorderSize { get; set; } = 2;
        public bool UsePasswordChar {
            get { return this.textBox.UseSystemPasswordChar; }
            set { this.textBox.UseSystemPasswordChar = value; }
        }
        public string WaterMarkText {
            get { return waterMarkText; }
            set {
                waterMarkText = value;
                EnableWaterMark();
            }
        }
        public Color WaterMarkColor {
            get { return waterMarkColor; }
            set {
                waterMarkColor = value;
                EnableWaterMark();
            }
        }
        public Color TextBoxBackColor {
            get { return textBox.BackColor; }
            set { textBox.BackColor = value; }
        }
        public Color TextBoxForeColor {
            get { return textBoxForeColor; }
            set {
                textBoxForeColor = value;
                textBox.ForeColor = textBoxForeColor;
            }
        }
        public Color BorderColor {
            get { return borderColor; }
            set {
                if (borderColor != value) {
                    if (borderPen != null) {
                        borderPen.Dispose();
                    }
                    borderPen = new Pen(value);
                }
                borderColor = value;
            }
        }

        private Color borderColor;
        private Pen borderPen;
        private TextBox textBox;
        private bool enablingWatermark;
        private bool waterMarkActive;
        private string waterMarkText;
        private Color waterMarkColor;
        private Color textBoxForeColor;

        public NCTextBox() {
            textBox = new TextBox() {
                BorderStyle = BorderStyle.None,
                Location = new Point(0, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right
            };
            Control container = new ContainerControl() {
                Dock = DockStyle.Fill,
                Padding = new Padding(-1)
            };
            container.Controls.Add(textBox);
            this.Controls.Add(container);

            UpdateSize();

            textBox.GotFocus += TextBox_GotFocus;
            textBox.LostFocus += TextBox_LostFocus;
            textBox.TextChanged += TextBox_TextChanged;
            textBox.ForeColor = TextBoxForeColor;
            textBox.KeyDown += TextBox_KeyDown;

            EnableWaterMark();
        }

        public override string Text {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);

            UpdateSize();
        }

        private void UpdateSize() {
            textBox.Location = new Point(BorderSize, BorderSize);
            int borderSize2 = BorderSize * 2;
            textBox.Width = this.Width - borderSize2 - 2;
            this.Height = textBox.Height + borderSize2 + 2;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e) {
            if (TextBoxKeyDown != null) {
                TextBoxKeyDown(sender, e);
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e) {
            if (!enablingWatermark && TextChanged != null) {
                TextChanged(sender, e);
            }
        }

        private void EnableWaterMark() {
            if (!this.waterMarkActive && string.IsNullOrEmpty(this.textBox.Text)) {
                this.waterMarkActive = true;

                this.enablingWatermark = true;
                this.textBox.Text = WaterMarkText;
                this.enablingWatermark = false;

                this.textBox.ForeColor = WaterMarkColor;
            } else if (this.waterMarkActive) {
                this.enablingWatermark = true;
                this.textBox.Text = WaterMarkText;
                this.enablingWatermark = false;

                this.textBox.ForeColor = WaterMarkColor;
            }
        }

        private void DisableWatermark() {
            if (this.waterMarkActive) {
                this.waterMarkActive = false;
                this.enablingWatermark = true;
                this.textBox.Text = "";
                this.enablingWatermark = false;

                this.textBox.ForeColor = textBoxForeColor;
            }
        }

        private void TextBox_GotFocus(object sender, EventArgs e) {
            DisableWatermark();
        }

        private void TextBox_LostFocus(object sender, EventArgs e) {
            EnableWaterMark();
        }
    }
}
