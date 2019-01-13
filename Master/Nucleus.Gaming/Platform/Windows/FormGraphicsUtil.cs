using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Gaming.Platform.Windows {
    public static class FormGraphicsUtil {
        public static Bitmap BuildCharToBitmap(Size size, int fontSize, Color color, string str, int rectBorder = 0, int reduce = 0) {
            using (Font font = new Font("Arial", fontSize)) {
                return BuildCharToBitmap(size, font, color, str, rectBorder, reduce);
            }
        }

        public static Bitmap BuildCharToBitmap(Size size, Font font, Color color, string str, int rectBorder = 0, int reduce = 0) {
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bmp);

            SizeF fontSize = g.MeasureString(str, font);
            fontSize.Width -= reduce;
            Point pos = new Point((int)((size.Width / 2) - (fontSize.Width / 2)), (int)((size.Height / 2) - (fontSize.Height / 2)));

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            if (rectBorder > 0) {
                using (Pen p = new Pen(color)) {
                    g.DrawRectangle(p, new Rectangle(rectBorder, rectBorder, size.Width - rectBorder, size.Height - rectBorder));
                }
            }

            TextRenderer.DrawText(g, str, font, pos, color);
            return bmp;
        }
    }
}
