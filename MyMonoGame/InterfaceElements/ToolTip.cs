using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.InterfaceElements
{
    public class ToolTip : BaseElement
    {
        // public Rectangle Bounds { get; set; }
        public string Text { get; set; }
        public ToolTip(string text, GameContext context) : base(new Rectangle(0, 0, 0, 0), context)
        {
            Text = text;
        }

        public override void Draw()
        {
            if (IsVisible)
            {
                Context.SpriteBatch.Draw(Context.Pixel, Bounds, Color.Black * 0.7f);
                Context.SpriteBatch.DrawString(Context.Font, Text, new Vector2(Bounds.X + 5, Bounds.Y + 5), Color.White);
            }
        }
    }
}
