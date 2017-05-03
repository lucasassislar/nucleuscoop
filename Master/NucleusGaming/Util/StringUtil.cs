using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public static class StringUtil
    {
        public static readonly char[] Numbers = new char[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        public static bool IsNumber(char c)
        {
            // We COULD use Int.TryParse, but this looks way cleaner
            return Numbers.Contains(c);
        }

        /// <summary>
        /// This method can be made better
        /// </summary>
        /// <param name="maxWidth"></param>
        /// <param name="str"></param>
        /// <param name="graphics"></param>
        /// <param name="font"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string WrapString(float maxWidth, string str, Graphics graphics, Font font, out SizeF size)
        {
            size = graphics.MeasureString(str, font);

            string[] sep = str.Split(' ');
            if (sep.Length == 0)
            {
                return str;
            }

            float spaceSize = graphics.MeasureString(" ", font).Width;

            string result = sep[0];

            float currentWidth = graphics.MeasureString(result, font).Width;
            float maxUsedWidth = 0;
            int lines = 1;
            string currentLine = result;

            for (int i = 1; i < sep.Length; i++)
            {
                string word = sep[i];
                string spaceWord = " " + word;
                SizeF wordSize = graphics.MeasureString(spaceWord, font);
                currentWidth += wordSize.Width;

                if (currentWidth > maxWidth)
                {
                    currentWidth = wordSize.Width;
                    maxUsedWidth = Math.Max(maxUsedWidth, graphics.MeasureString(currentLine, font).Width);
                    result += "\n" + word;
                    currentLine = "";
                    lines++;
                }
                else
                {
                    result += spaceWord;
                    currentLine += spaceWord;
                }
            }

            if (maxUsedWidth == 0)
            {
                maxUsedWidth = Math.Max(maxUsedWidth, graphics.MeasureString(currentLine, font).Width);
            }
            size = new SizeF(maxUsedWidth, lines * font.GetHeight(graphics));

            return result;
        }
    }
}
