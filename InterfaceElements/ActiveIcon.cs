using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.InterfaceElements
{
    public class ActiveIcon : BaseElementWithTooltip
    {
        public Texture2D Texture { get; private set; }
        public ActiveIcon(Rectangle bounds, SpriteFont font, string tooltipText = null) : base(bounds, font, tooltipText)
        {

        }

        public void SetTexture(Texture2D texture)
        {
            this.Texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D pixel = null)
        {
            if (IsVisible)
            {
                if (Texture != null)
                {
                    spriteBatch.Draw(Texture, Bounds, Color.White);
                }
                base.Draw(spriteBatch);
            }
        }
    }
}
