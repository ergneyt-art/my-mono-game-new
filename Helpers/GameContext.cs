using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.Helpers
{
    public class GameContext
    {
        public GameAssets Assets { get; set; }
        public SpriteFont Font { get; set; }
        public Texture2D Pixel { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public int ScreenWidth { get; set; } = 0;
        public int ScreenHeight { get; set; } = 0;
        public GameContext(GameAssets assets, SpriteFont font, Texture2D pixel, SpriteBatch spriteBatch) 
        { 
            Assets = assets;
            Font = font;
            Pixel = pixel;
            SpriteBatch = spriteBatch;
        }
    }
}
