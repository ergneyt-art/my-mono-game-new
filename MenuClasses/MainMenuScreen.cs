using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.MenuClasses
{
    public class MainMenuScreen : BaseMenu
    {
        public MainMenuScreen(string title, Viewport viewport, SpriteFont font, Texture2D pixel) : 
            base(title, viewport, font, pixel)
        {
            _spacing = 10;
            this.AddButtonToCenterPanel("Start Game", ScreenAction.GoToCharacterMenu);
            this.AddButtonToCenterPanel("Load Game", ScreenAction.GoToLoadGameMenu);
            this.AddButtonToCenterPanel("Settings", ScreenAction.GoToSettingsMenu);
            this.AddButtonToCenterPanel("About Game", ScreenAction.GoToAboutGameMenu);
            this.AddButtonToCenterPanel("Exit", ScreenAction.ExitGame);

            this.AddButtonToCenterPanel("Test", ScreenAction.Test);
        }

        public override ScreenAction Update()
        {
            foreach (var button in _buttons) 
            {
                if (button.Update() != ScreenAction.None) { return button.Action; }
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
