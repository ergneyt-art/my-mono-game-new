using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.InterfaceElements
{
    public class TextBlock : BaseInterfaceElement
    {
        public string Text { get; set; } = string.Empty;

        public TextBlock(Rectangle bounds, string text, SpriteFont font) : base(bounds, font)
        {
            Text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var text = TextHelper.SplitText(Text, _font, Bounds.Width);
            var textSize = _font.MeasureString(Text);
            var counter = 0;
            foreach (var item in text) 
            {
                spriteBatch.DrawString(_font, item, new Vector2(Bounds.X, Bounds.Y + (textSize.Y * counter)), Color.White);
                counter++;
            }
        }
    }
}
