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
        private int buttonWidth = 100;
        private int buttonHeight = 50;
        private int startX = 5;
        private int startY = 100;

        public MainMenuScreen(string title, Viewport viewport, SpriteFont font, Texture2D pixel) : 
            base(title, viewport, font, pixel)
        {
            _spacing = 10;
            this.AddButtonToCenterPanelTop("Start Game", ScreenAction.GoToCharacterMenu);
            this.AddButtonToCenterPanelTop("Load Game", ScreenAction.GoToLoadGameMenu);
            this.AddButtonToCenterPanelTop("Settings", ScreenAction.GoToSettingsMenu);
            this.AddButtonToCenterPanelTop("About Game", ScreenAction.GoToAboutGameMenu);
            this.AddButtonToCenterPanelTop("Exit", ScreenAction.ExitGame);

            /*
            startX = (viewport.Width / 2) - (viewport.Height / 2);
            _buttons.Add(new Button(new Rectangle(startX, startY, buttonWidth, buttonHeight), ScreenAction.GoToCharacterMenu, "Start Game", _font));
            _buttons.Add(new Button(new Rectangle(startX, startY + (buttonHeight + _spacing), buttonWidth, buttonHeight), ScreenAction.GoToLoadGameMenu, "Load Game", _font));
            _buttons.Add(new Button(new Rectangle(startX, startY + (buttonHeight + _spacing) * 2, buttonWidth, buttonHeight), ScreenAction.GoToSettingsMenu, "Settings", _font));
            _buttons.Add(new Button(new Rectangle(startX, startY + (buttonHeight + _spacing) * 3, buttonWidth, buttonHeight), ScreenAction.GoToAboutGameMenu, "About Game", _font));
            _buttons.Add(new Button(new Rectangle(startX, startY + (buttonHeight + _spacing) * 4, buttonWidth, buttonHeight), ScreenAction.ExitGame, "Exit", _font));
            */
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
