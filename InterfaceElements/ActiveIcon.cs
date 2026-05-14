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
    public class ActiveIcon : BaseElementWithTooltip
    {
        public Texture2D Texture { get; private set; }
        public ActiveIcon(Rectangle bounds, GameContext context, string tooltipText = null) : base(bounds, context, tooltipText)
        {

        }

        public void Update()
        {
             UpdateHoveredState();
        }

        public void SetTexture(Texture2D texture)
        {
            this.Texture = texture;
        }

        public override void Draw()
        {
            if (IsVisible)
            {
                base.Draw();
                if (Texture != null)
                {
                    Context.SpriteBatch.Draw(Texture, Bounds, Color.White);
                }
                else 
                {
                    if (IsHovered)
                    {
                        Context.SpriteBatch.Draw(Context.Pixel, Bounds, Color.LightBlue);
                    }
                    else
                    {
                        Context.SpriteBatch.Draw(Context.Pixel, Bounds, Color.White);
                    }
                }
            }
        }
    }
}
