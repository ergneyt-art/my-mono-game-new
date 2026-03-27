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
        private BaseMenu _currentScreen;
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
            _mainMenuScreen = new MainMenuScreen("Main menu", GraphicsDevice.Viewport.Height, GraphicsDevice.Viewport.Width, _font, _pixel);
            _characterMenuScreen = new CharacterMenuScreen("Character menu", GraphicsDevice.Viewport.Height, GraphicsDevice.Viewport.Width, _font, _pixel);
            _currentScreen = _mainMenuScreen;

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var action = _currentScreen.Update();
            SwitchMenu(action);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void SwitchMenu(ScreenAction action)
        {
            if (action == ScreenAction.None) { return; } 
            switch (action)
            {
                case ScreenAction.GoToMainMenu:
                    _currentScreen = _mainMenuScreen;
                    break;
                case ScreenAction.GoToLoadGameMenu:
                    break;
                case ScreenAction.GoToCharacterMenu:
                    _currentScreen = _characterMenuScreen;
                    break;
                case ScreenAction.GoToSettingsMenu:
                    break;
                case ScreenAction.ExitGame:
                    break;
                default:
                    break;
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _currentScreen.Draw(_spriteBatch);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
