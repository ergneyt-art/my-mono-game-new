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
    public class TextBlock : BaseElement
    {
        public string Text { get; set; } = string.Empty;

        public TextBlock(Rectangle bounds, string text, GameContext context) : base(bounds, context)
        {
            Text = text;
        }

        public override void Draw()
        {
            var text = TextHelper.SplitText(Text, Context.Font, Bounds.Width);
            var textSize = Context.Font.MeasureString(Text);
            var counter = 0;
            foreach (var item in text) 
            {
                Context.SpriteBatch.DrawString(Context.Font, item, new Vector2(Bounds.X, Bounds.Y + (textSize.Y * counter)), Color.White);
                counter++;
            }
        }
    }
}
