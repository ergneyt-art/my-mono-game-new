using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.MenuClasses
{
    public class LoadGameMenu : BaseMenu
    {
        public LoadGameMenu(string title, Viewport viewport, SpriteFont font, Texture2D pixel) : base(title, viewport, font, pixel)
        {
            _spacing = 10;
            AddButtonToLeftPanel("Back", ScreenAction.GoToMainMenu);
        }

        public override ScreenAction Update()
        {
            foreach (var button in _buttons)
            {
                if (button.Update() != ScreenAction.None) return button.Action;
            }
            return ScreenAction.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SetTitle(spriteBatch);
            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch, _font, _pixel);
            }
        }
    }
}
