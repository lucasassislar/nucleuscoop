using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Nucleus.Gaming.Platform.Windows.Controls
{
    /// <summary>
    /// Panel that uses custom images on the edge (untested in the latest version)
    /// </summary>
    public partial class BorderPanel : Panel
    {
        protected Bitmap edge;
        protected Bitmap eRight;
        protected Bitmap eBRight;
        protected Bitmap eLeft;

        protected Bitmap len;
        public Bitmap EdgeImage
        {
            get { return edge; }
            set
            {
                edge = value;

                eRight = (Bitmap)edge.Clone();
                eRight.RotateFlip(RotateFlipType.Rotate90FlipNone);

                eBRight = (Bitmap)edge.Clone();
                eBRight.RotateFlip(RotateFlipType.Rotate180FlipNone);

                eLeft = (Bitmap)edge.Clone();
                eLeft.RotateFlip(RotateFlipType.Rotate270FlipNone);

                this.Invalidate();
            }
        }

        public Color BackgroundColor
        {
            get;
            set;
        }

        public Bitmap BorderImage
        {
            get { return len; }
            set
            {
                len = value;

                right = (Bitmap)len.Clone();
                right.RotateFlip(RotateFlipType.Rotate90FlipNone);

                bottom = (Bitmap)len.Clone();
                bottom.RotateFlip(RotateFlipType.Rotate180FlipNone);

                left = (Bitmap)len.Clone();
                left.RotateFlip(RotateFlipType.Rotate270FlipNone);

                this.Invalidate();
            }
        }

        private Bitmap right;
        private Bitmap left;
        private Bitmap bottom;

        public BorderPanel()
        {
            BackgroundColor = Color.FromArgb(25, 25, 25);
            InitializeComponent();

            this.Resize += BorderPanel_Resize;
        }

        void BorderPanel_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            if (edge != null)
            {
                g.SmoothingMode = SmoothingMode.None;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.None;

                Rectangle bounds = this.Bounds;

                int x = 0;
                int y = 0;

                // Top-Left to Top-Right
                g.FillRectangle(Brushes.Red, new Rectangle(edge.Width, 0, bounds.Width - edge.Width - edge.Width, len.Height));
                g.DrawImage(len, new Rectangle(edge.Width, 0, bounds.Width - edge.Width - edge.Width, len.Height));
                g.DrawImage(bottom, new Rectangle(edge.Width, bounds.Height - bottom.Height, bounds.Width - edge.Width - edge.Width, len.Height));

                // Top-Right to Bottom-Right
                g.DrawImage(right, new Rectangle(bounds.Width - right.Width, edge.Height, right.Width, bounds.Height - edge.Height - edge.Height));
                g.DrawImage(left, new Rectangle(0, edge.Height, right.Width, bounds.Height - edge.Height - edge.Height));

                Brush brush = new SolidBrush(this.BackgroundColor);
                g.FillRectangle(brush, new Rectangle(left.Width, edge.Height, bounds.Width - left.Width - right.Width, bounds.Height - edge.Height - edge.Height));
                g.FillRectangle(brush, new Rectangle(edge.Width, len.Height, bounds.Width - edge.Width - edge.Width, edge.Height - len.Height));
                g.FillRectangle(brush, new Rectangle(edge.Width, bounds.Height - edge.Height, bounds.Width - edge.Width - edge.Width, edge.Height - len.Height));

                brush.Dispose();

                // Top Left
                g.DrawImage(edge, new Rectangle(0, 0, edge.Width, edge.Height));

                //Top Right
                x = bounds.Width - edge.Width;
                y = 0;
                g.DrawImage(eRight, new Rectangle(x, y, edge.Width, edge.Height));

                // Bottom Right
                y = bounds.Height - edge.Height;
                g.DrawImage(eBRight, new Rectangle(x, y, edge.Width, edge.Height));

                // Bottom Left
                x = 0;
                y = bounds.Height - edge.Height;
                g.DrawImage(eLeft, new Rectangle(x, y, edge.Width, edge.Height));
            }
        }
    }
}
