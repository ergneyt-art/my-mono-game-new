using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MyMonoGame
{
    public class Enigma : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private Texture2D _pixel;
        private MainMenuScreen _mainMenuScreen;
        private CharacterMenuScreen _characterMenuScreen;

        public Enigma()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("DefaultFont");
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
            _mainMenuScreen = new MainMenuScreen(_font, _pixel);
            _characterMenuScreen = new CharacterMenuScreen(_font, _pixel, GraphicsDevice.Viewport.Height, GraphicsDevice.Viewport.Width);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var clickedButton = string.Empty;
            if (_mainMenuScreen.IsCurrentMenu)
            {
                clickedButton = _mainMenuScreen.Update();
                SwitchMenu(clickedButton);
            }
            
            else if (_characterMenuScreen.IsCurrentMenu) 
            {
                clickedButton = _characterMenuScreen.Update();
                SwitchMenu(clickedButton);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void SwitchMenu(string pressedButton)
        {
            if (string.IsNullOrEmpty(pressedButton)) { return; }
            switch (pressedButton)
            {
                case "StartGame":
                    _mainMenuScreen.IsCurrentMenu = false;
                    _characterMenuScreen.IsCurrentMenu = true;
                    break;
                case "GoToMainMenu":
                    _mainMenuScreen.IsCurrentMenu = true;
                    _characterMenuScreen.IsCurrentMenu = false;
                    break;
                default:
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (_mainMenuScreen.IsCurrentMenu)
            {
                _mainMenuScreen.Draw(_spriteBatch);
            }
            else if (_characterMenuScreen.IsCurrentMenu)
            {
                _characterMenuScreen.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
