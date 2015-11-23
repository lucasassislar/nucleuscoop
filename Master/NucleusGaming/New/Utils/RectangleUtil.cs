using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming
{
    public static class RectangleUtil
    {
        public static Rectangle Union(params Rectangle[] rects)
        {
            Rectangle r = new Rectangle();
            for (int i = 0; i < rects.Length; i++)
            {
                r = Rectangle.Union(r, rects[i]);
            }
            return r;
        }

        public static Rectangle Center(Rectangle rect, Rectangle parent)
        {
            float rectWidth = rect.Width / 2.0f;
            float rectHeight = rect.Height / 2.0f;

            float parentWidth = parent.Width / 2.0f;
            float parentHeight = parent.Height / 2.0f;

            return new Rectangle(
                (int)(parentWidth - rectWidth) + parent.X,
                (int)(parentHeight - rectHeight) + parent.Y,
                rect.Width,
                rect.Height);
        }

        public static PointF Center(SizeF rect, Rectangle parent)
        {
            float rectWidth = rect.Width / 2.0f;
            float rectHeight = rect.Height / 2.0f;

            float parentWidth = parent.Width / 2.0f;
            float parentHeight = parent.Height / 2.0f;

            return new PointF(
                (parentWidth - rectWidth) + parent.X,
                (parentHeight - rectHeight) + parent.Y);
        }

        /// <summary>
        /// Returns a value between 0 and 1 telling how much the first rectangle is inside the parent
        /// </summary>
        /// <param name="first"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static float PcInside(Rectangle first, Rectangle parent)
        {
            float pc = 0;

            if (parent.Contains(first))
            {
                return 1;
            }
            else if (parent.IntersectsWith(first))
            {
                Rectangle intersection = Rectangle.Intersect(first, parent);
                float peri = first.Width * first.Height;
                float nperi = intersection.Width * intersection.Height;

                return nperi / peri;

            }
            return pc;
        }
    }
}
