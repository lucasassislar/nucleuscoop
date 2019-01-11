using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nucleus.Gaming.Windows.Interop;

namespace Nucleus.Gaming.Platform.Windows.Controls {
    public class TitleBarControl : UserControl {
        private PictureBox icon;

        private Label titleLabel;
        private string text = "Form";

        private Button minimize;
        private Button maximize;
        private Button close;

        public bool EnableMaximize { get; set; } = true;

        [Browsable(true)]
        public override string Text {
            get {
                if (titleLabel == null) {
                    return text;
                }
                return titleLabel.Text;
            }
            set {
                if (titleLabel == null) {
                    text = value;
                    return;
                }
                titleLabel.Text = value;
            }
        }

        private Font btnFont;
        private Font titleFont;

        public TitleBarControl() {
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            this.Size = new Size(1000, 21);

            this.BackColor = Color.FromArgb(255, 32, 34, 37);
            this.Padding = Padding.Empty;
            this.Margin = Padding.Empty;
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            InitializeButtons();
        }

        private Image cachedIcon;

        public Image Icon {
            get {
                if (icon == null) {
                    return cachedIcon;
                } else { 
                    return icon.Image;
                }
            }
            set {
                if (icon == null) {
                    cachedIcon = value;
                } else {
                    icon.Image = value;
                }
            }
        }

        private void InitializeButtons() {
            btnFont = new Font(this.Font.FontFamily, 6, FontStyle.Regular);
            titleFont = new Font(this.Font.FontFamily, 10, FontStyle.Regular);

            titleLabel = new Label();
            titleLabel.Text = text;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 2);
            titleLabel.Font = titleFont;
            titleLabel.DoubleClick += TitleLabel_DoubleClick;
            Controls.Add(titleLabel);

            icon = new PictureBox();
            icon.Location = new Point(2, 2);
            icon.Size = new Size(17, 17);
            icon.SizeMode = PictureBoxSizeMode.StretchImage;
            icon.Image = cachedIcon;
            this.Controls.Add(icon);

            close = MakeFlatBtn();
            maximize = MakeFlatBtn();
            minimize = MakeFlatBtn();

            close.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 240, 30, 30);
            close.Location = new Point(this.Width - 26, 0);
            close.Left = this.Width - 26;
            close.Text = "X";
            close.Click += Close_Click;

            if (EnableMaximize) {
                maximize.Left = close.Left - 26;
                maximize.Text = "[]";
                maximize.Click += Maximize_Click;
                minimize.Left = maximize.Left - 26;
            } else {
                minimize.Left = close.Left - 26;
            }

            minimize.Text = "_";
            minimize.Click += Minimize_Click;
        }

        private void Minimize_Click(object sender, EventArgs e) {
            if (this.ParentForm != null) {
                this.ParentForm.WindowState = FormWindowState.Minimized;
            }
        }

        private void Maximize_Click(object sender, EventArgs e) {
            SwapMaximized();
        }

        private void TitleLabel_DoubleClick(object sender, EventArgs e) {
            SwapMaximized();
        }

        private DateTime lastUpdateDate;
        private void SwapMaximized() {
            if (!EnableMaximize) {
                return;
            }

            Form parent = this.ParentForm;
            if (parent != null) {
                DateTime now = DateTime.Now;
                // TODO: is this bad
                if (now - lastUpdateDate < TimeSpan.FromMilliseconds(300)) {
                    return;
                }
                lastUpdateDate = now;

                if (parent.WindowState == FormWindowState.Maximized) {
                    parent.WindowState = FormWindowState.Normal;
                } else {
                    parent.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private bool mouseDown;
        private Point dragStartPoint;

        protected override void OnMouseDown(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                Form parent = this.ParentForm;
                if (parent != null) {
                    mouseDown = true;
                    dragStartPoint = e.Location;
                    //User32Interop.ReleaseCapture();
                    //User32Interop.SendMessage(parent.Handle, User32_WS.WM_NCLBUTTONDOWN, User32_WS.HT_CAPTION, 0);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            if (mouseDown) {
                Form parent = this.ParentForm;
                if (parent != null) {
                    if (parent.WindowState == FormWindowState.Maximized) {
                        var ox = parent.Location.X;
                        var width = parent.Width;
                        parent.WindowState = FormWindowState.Normal;
                        // update the drag start point to be relative
                        // scale parent location x over the original size
                        var nwidth = parent.Width;
                        float factor = nwidth / (float)width;
                        int newX = (int)(factor * ox);

                        dragStartPoint = new Point(parent.Location.X + newX, dragStartPoint.Y);
                    }

                    parent.Location = new Point(
                        parent.Location.X - dragStartPoint.X + e.X,
                        parent.Location.Y - dragStartPoint.Y + e.Y);
                    this.Update();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left) {
                Form parent = this.ParentForm;
                if (parent != null) {
                    mouseDown = false;
                    Point pos = Cursor.Position;
                    if (parent.WindowState == FormWindowState.Normal &&
                        pos.Y < 30) {
                        SwapMaximized();
                    }
                }
            }
        }

        protected override void OnDoubleClick(EventArgs e) {
            base.OnDoubleClick(e);

            SwapMaximized();
        }

        protected override void OnClick(EventArgs e) {
            base.OnClick(e);

            titleLabel.Focus();
        }

        private Button MakeFlatBtn() {
            Button btn = new Button();
            btn.Font = btnFont;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 120, 120, 120);
            btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn.Size = new Size(26, 21);
            btn.Text = "X";
            Controls.Add(btn);
            return btn;
        }


        private void Close_Click(object sender, EventArgs e) {
            if (this.ParentForm != null) {
                this.ParentForm.Close();
            }
            //Application.Exit();
        }

        protected override void OnParentChanged(EventArgs e) {
            base.OnParentChanged(e);

            if (Parent != null) {
                Parent.Resize += Parent_Resize;
                this.Width = Parent.ClientSize.Width;
            }
        }

        private void Parent_Resize(object sender, EventArgs e) {
            if (Parent != null) {
                this.Width = Parent.ClientSize.Width;
            }
        }
    }
}
