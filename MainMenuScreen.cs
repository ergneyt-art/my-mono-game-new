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
    public class MainMenuScreen
    {
        public bool IsCurrentMenu = true;
        private SpriteFont _font;
        private Texture2D _pixel;

        private Button StartButton;
        private Button LoadGameButton;
        private Button SettingsButton;
        private Button CreatorsButton;
        private Button ExitGameButton;

        private int WidthMargin = 50;
        private int HeightMargin = 100;
        private int MarginBetweenButtons = 20;
        private int ButtonWidth = 200;
        private int ButtonHeight = 50;

        public MainMenuScreen(SpriteFont font, Texture2D pixel)
        {
            _font = font;
            _pixel = pixel;
            StartButton = new Button(new Rectangle(WidthMargin, HeightMargin, ButtonWidth, ButtonHeight), "Start Game", _font);
            LoadGameButton = new Button(new Rectangle(WidthMargin, (HeightMargin + ButtonHeight + MarginBetweenButtons), ButtonWidth, ButtonHeight), "Load Game", _font);
            SettingsButton = new Button(new Rectangle(WidthMargin, (HeightMargin + (ButtonHeight + MarginBetweenButtons) * 2), ButtonWidth, ButtonHeight), "Settings", _font);
            CreatorsButton = new Button(new Rectangle(WidthMargin, (HeightMargin + (ButtonHeight + MarginBetweenButtons) * 3), ButtonWidth, ButtonHeight), "About Game", _font);
            ExitGameButton = new Button(new Rectangle(WidthMargin, (HeightMargin + (ButtonHeight + MarginBetweenButtons) * 4), ButtonWidth, ButtonHeight), "Exit", _font);
        }

        public string Update()
        {
            StartButton.Update();
            LoadGameButton.Update();
            SettingsButton.Update();
            CreatorsButton.Update();
            ExitGameButton.Update();

            if (StartButton.IsClicked)
            {
                IsCurrentMenu = false;
                Console.WriteLine("Start game button clicked!");
                return "StartGame";
                
            }

            if (LoadGameButton.IsClicked)
            {
                // IsCurrentMenu = false;
                Console.WriteLine("Load game button cliced!");
            }

            if (SettingsButton.IsClicked)
            {
                // IsCurrentMenu = false;
                Console.WriteLine("Settings button clicked!");
            }

            if (CreatorsButton.IsClicked)
            {
                // IsCurrentMenu = false;
                Console.WriteLine("Creators button clicked!");
            }

            if (ExitGameButton.IsClicked)
            {
                IsCurrentMenu = false;
                Console.WriteLine("Exit button clicked!");
            }

            return string.Empty;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            StartButton.Draw(spriteBatch, _font, _pixel);
            LoadGameButton.Draw(spriteBatch, _font, _pixel);
            SettingsButton.Draw(spriteBatch, _font, _pixel);
            CreatorsButton.Draw(spriteBatch, _font, _pixel);
            ExitGameButton.Draw(spriteBatch, _font, _pixel);
        }

    }
}
