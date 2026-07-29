using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.Helpers
{
    /// <summary>
    /// Stores shared game services and resources that UI elements need to draw themselves.
    /// </summary>
    public class GameContext
    {
        /// <summary>
        /// Loaded textures and other content assets used by the game.
        /// </summary>
        public GameAssets Assets { get; set; }

        /// <summary>
        /// Default font used by interface elements.
        /// </summary>
        public SpriteFont Font { get; set; }

        /// <summary>
        /// One-pixel white texture used for drawing colored rectangles.
        /// </summary>
        public Texture2D Pixel { get; set; }

        /// <summary>
        /// Shared sprite batch used during the current draw pass.
        /// </summary>
        public SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// Current back buffer width. Intended for layout and tooltip boundary checks.
        /// </summary>
        public int ScreenWidth { get; set; } = 0;

        /// <summary>
        /// Current back buffer height. Intended for layout and tooltip boundary checks.
        /// </summary>
        public int ScreenHeight { get; set; } = 0;

        /// <summary>
        /// Creates a context with the shared resources required by UI elements.
        /// </summary>
        public GameContext(GameAssets assets, SpriteFont font, Texture2D pixel, SpriteBatch spriteBatch) 
        { 
            Assets = assets;
            Font = font;
            Pixel = pixel;
            SpriteBatch = spriteBatch;
        }
    }
}
