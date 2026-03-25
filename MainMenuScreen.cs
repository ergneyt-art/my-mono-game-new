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

        private Rectangle _startButtonRect;
        private Rectangle _loadButtonRect;
        private Rectangle _creatorsButtonRect;
        private Rectangle _exitButtonRect;


        private bool _isStartHovered;
        private bool _isLoadHovered;
        private bool _isCreatorsHovered;
        private bool _isExitHovered;

        private MouseState _previousMouse;

        public bool StartRequested { get; private set; }
        public bool LoadRequested { get; private set; }
        public bool CreatorRequested { get; private set; }
        public bool ExitRequested { get; private set; }

        public MainMenuScreen(SpriteFont font, Texture2D pixel)
        {
            _font = font;
            _pixel = pixel;

            _startButtonRect = new Rectangle(100, 200, 200, 50);
            _loadButtonRect = new Rectangle(100, 270, 200, 50);
            _creatorsButtonRect = new Rectangle(100, 340, 200, 50);
            _exitButtonRect = new Rectangle(100, 410, 200, 50);
        }

        public void Update()
        {
            var mouse = Mouse.GetState();
            _isStartHovered = _startButtonRect.Contains(mouse.Position);
            _isLoadHovered = _loadButtonRect.Contains(mouse.Position);
            _isCreatorsHovered = _creatorsButtonRect.Contains(mouse.Position);
            _isExitHovered = _exitButtonRect.Contains(mouse.Position);

            bool leftClicked =
                mouse.LeftButton == ButtonState.Pressed &&
                _previousMouse.LeftButton == ButtonState.Released;

            if (leftClicked && _isStartHovered)
            {
                StartRequested = true;
                Console.WriteLine("Start button clicked!");
            }

            if (leftClicked && _isLoadHovered)
            {
                LoadRequested = true;
                Console.WriteLine("Load button cliced!");
            }

            if (leftClicked && _isCreatorsHovered)
            {
                CreatorRequested = true;
                Console.WriteLine("Creators button clicked!");
            }

            if (leftClicked && _isExitHovered)
            {
                ExitRequested = true;
                Console.WriteLine("Exit button clicked!");
            }

            _previousMouse = mouse;

            // логика мыши, hover, click
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color startButtonColor = _isStartHovered ? Color.DarkGray : Color.Gray;
            Color loadButtonColor = _isLoadHovered ? Color.DarkGray : Color.Gray;
            Color creatorsButtonColor = _isCreatorsHovered ? Color.DarkGray : Color.Gray;
            Color exitButtonColor = _isExitHovered ? Color.DarkGray : Color.Gray;

            spriteBatch.Draw(_pixel, _startButtonRect, startButtonColor);
            spriteBatch.Draw(_pixel, _loadButtonRect, loadButtonColor);
            spriteBatch.Draw(_pixel, _creatorsButtonRect, creatorsButtonColor);
            spriteBatch.Draw(_pixel, _exitButtonRect, exitButtonColor);

            spriteBatch.DrawString(_font, "Start Game", new Vector2(120, 215), Color.White);
            spriteBatch.DrawString(_font, "Load Game", new Vector2(120, 285), Color.White);
            spriteBatch.DrawString(_font, "Creators", new Vector2(120, 355), Color.White);
            spriteBatch.DrawString(_font, "Exit Game", new Vector2(120, 425), Color.White);

            // рисуем текст, кнопки
        }

    }
}
