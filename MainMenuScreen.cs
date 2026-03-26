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
        private SpriteFont _font;
        private Texture2D _pixel;

        private Button StartButton;
        private Button LoadGameButton;
        private Button SettingsButton;
        private Button CreatorsButton;
        private Button ExitGameButton;

        public bool StartRequested { get; private set; }
        public bool LoadRequested { get; private set; }
        public bool CreatorRequested { get; private set; }
        public bool ExitRequested { get; private set; }

        public MainMenuScreen(SpriteFont font, Texture2D pixel)
        {
            _font = font;
            _pixel = pixel;
            StartButton = new Button(new Rectangle(100, 130, 200, 50), "Start Game", _font);
            LoadGameButton = new Button(new Rectangle(100, 200, 200, 50), "Load Game", _font);
            SettingsButton = new Button(new Rectangle(100, 270, 200, 50), "Settings", _font);
            CreatorsButton = new Button(new Rectangle(100, 340, 200, 50), "About Game", _font);
            ExitGameButton = new Button(new Rectangle(100, 410, 200, 50), "Exit", _font);
        }

        public void Update()
        {
            StartButton.Update();
            LoadGameButton.Update();
            SettingsButton.Update();
            CreatorsButton.Update();
            ExitGameButton.Update();

            if (StartButton.IsClicked)
            {
                Console.WriteLine("Start game button clicked!");
            }

            if (LoadGameButton.IsClicked)
            {
                Console.WriteLine("Load game button cliced!");
            }

            if (SettingsButton.IsClicked)
            {
                Console.WriteLine("Settings button clicked!");
            }

            if (CreatorsButton.IsClicked)
            {
                Console.WriteLine("Creators button clicked!");
            }

            if (ExitGameButton.IsClicked)
            {
                Console.WriteLine("Exit button clicked!");
            }
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
