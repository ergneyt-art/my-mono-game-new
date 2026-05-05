using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyMonoGame.Helpers
{
    public static class TextHelper
    {
        public static int GetTextWidth(string text, SpriteFont font)
        {
            return (int)font.MeasureString(text).X;
        }

        public static int GetTextHeight(string text, SpriteFont font)
        {
            return (int)font.MeasureString(text).Y;
        }

        public static List<string> SplitText(string sourceText, SpriteFont font, int areaWidth)
        {
            var textSize = font.MeasureString(sourceText);
            if (textSize.X > areaWidth)
            {
                var splitText = sourceText.Split(' ');
                var currentLine = string.Empty;
                var result = new List<string>();
                foreach (var word in splitText)
                {
                    if (font.MeasureString(currentLine + word).X < areaWidth)
                    {
                        currentLine += word + " ";
                    }
                    else
                    {
                        result.Add(currentLine);
                        currentLine = word + " ";
                    }
                }
                result.Add(currentLine);
                return result;
            }
            else
            {
                return new List<string> { sourceText };

            }
        }
    }
}
