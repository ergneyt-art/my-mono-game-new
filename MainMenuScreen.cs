using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame
{
    public class MainMenuScreen : BaseMenu
    {
        private Dictionary<string, Button> _buttons;

        private int buttonWidth = 100;
        private int buttonHeight = 50;
        private int startX = 5;
        private int startY = 5;
        private int spacing = 10;

        public MainMenuScreen(string title, int windowHeight, int windowWeight, SpriteFont font, Texture2D pixel) : base(title, windowHeight, windowWeight, font, pixel)
        {
            _buttons = new Dictionary<string, Button>();

            _buttons.Add("StartGame", new Button(new Rectangle(startX, startY, buttonWidth, buttonHeight), ScreenAction.GoToCharacterMenu, "Start Game", _font));
            _buttons.Add("LoadGame", new Button(new Rectangle(startX, startY + (buttonHeight + spacing), buttonWidth, buttonHeight), ScreenAction.GoToLoadGameMenu, "Load Game", _font));
            _buttons.Add("Settings", new Button(new Rectangle(startX, startY + (buttonHeight + spacing) * 2, buttonWidth, buttonHeight), ScreenAction.GoToSettingsMenu, "Settings", _font));
            _buttons.Add("AboutGame", new Button(new Rectangle(startX, startY + (buttonHeight + spacing) * 3, buttonWidth, buttonHeight), ScreenAction.GoToGameInfoMenu, "About Game", _font));
            _buttons.Add("Exit", new Button(new Rectangle(startX, startY + (buttonHeight + spacing) * 4, buttonWidth, buttonHeight), ScreenAction.ExitGame, "Exit", _font));
        }

        public override ScreenAction Update()
        {
            foreach (var button in _buttons) 
            {
                if (button.Value.Update() != ScreenAction.None) { return button.Value.Action; }
            }

            return ScreenAction.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SetTitle(spriteBatch);
            foreach (var button in _buttons)
            {
                button.Value.Draw(spriteBatch, _font, _pixel);
            }
        }

    }
}
