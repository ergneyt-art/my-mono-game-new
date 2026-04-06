using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.InterfaceElements
{
    public class ToolTip
    {
        public Rectangle Bounds { get; set; }
        public string Text { get; set; }
        public bool IsShow { get; set; }
        public ToolTip(string text)
        {
            Bounds = new Rectangle(0, 0, 0, 0);
            Text = text;
        }

        public void Show()
        {
            IsShow = true;
        }

        public void Hide()
        {
            IsShow = false;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D texture)
        {
            if (IsShow)
            {
                spriteBatch.Draw(texture, Bounds, Color.Black * 0.7f);
                spriteBatch.DrawString(font, Text, new Vector2(Bounds.X + 5, Bounds.Y + 5), Color.White);
            }
        }
    }
}
