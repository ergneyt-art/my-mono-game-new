using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public ToolTip(string text, SpriteFont font) : base(new Rectangle(0, 0, 0, 0), font)
        {
            Text = text;
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(texture, Bounds, Color.Black * 0.7f);
                spriteBatch.DrawString(Font, Text, new Vector2(Bounds.X + 5, Bounds.Y + 5), Color.White);
            }
        }
    }
}
