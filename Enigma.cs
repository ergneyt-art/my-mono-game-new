using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGame.GameObjects;
using MyMonoGame.Helpers;
using MyMonoGame.MenuClasses;
using System;
using System.Collections.Generic;

namespace MyMonoGame
{
    public class Enigma : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        public GameAssets Assets { get; private set; }

        private Texture2D _pixel;
        private BaseMenu<ScreenAction> _currentScreen;
        private MainMenuScreen _mainMenuScreen;
        private LoadGameMenu _loadGameMenu;
        private SettingsMenu _settingsMenu;
        private AboutGameMenu _aboutMenu;
        private PartyMenuScreen _partyMenuScreen;
        private CharacterEditorScreen _characterEditorScreen;

        private const int _defaultScreenWidth = 1280;
        private const int _defaultScreenHeight = 800;

        public Enigma()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = _defaultScreenWidth;
            _graphics.PreferredBackBufferHeight = _defaultScreenHeight;

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
            // Content.Load<Texture2D>("Characters/human-female");
            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
            Assets = new GameAssets(Content);
            _mainMenuScreen = new MainMenuScreen("Main menu", GraphicsDevice.Viewport.Bounds, _font, _pixel);
            _loadGameMenu = new LoadGameMenu("Load Game", GraphicsDevice.Viewport.Bounds, _font, _pixel);
            _settingsMenu = new SettingsMenu("Settings", GraphicsDevice.Viewport.Bounds, _font, _pixel);
            _aboutMenu = new AboutGameMenu("About game", GraphicsDevice.Viewport.Bounds, _font, _pixel);
            _partyMenuScreen = new PartyMenuScreen("Party menu", GraphicsDevice.Viewport.Bounds, _font, _pixel);
            _characterEditorScreen = new CharacterEditorScreen("Character menu", GraphicsDevice.Viewport.Bounds, _font, _pixel);
            _characterEditorScreen.SetCharacterTexture(Assets);
            _partyMenuScreen.SetCharacterTexture(Assets);
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
                    _currentScreen = _loadGameMenu;
                    break;
                case ScreenAction.GoToAboutGameMenu:
                    _currentScreen = _aboutMenu;
                    break;
                case ScreenAction.GoToPartyMenu:
                    _currentScreen = _partyMenuScreen;
                    break;
                case ScreenAction.GoToSettingsMenu:
                    _currentScreen = _settingsMenu;
                    break;
                case ScreenAction.GoToCharacterMenu:
                    _characterEditorScreen.LoadCharacter(_partyMenuScreen.CurrentChar);
                    _currentScreen = _characterEditorScreen;
                    break;
                case ScreenAction.SaveCharacter:
                    _currentScreen = _partyMenuScreen;
                    break;
                case ScreenAction.ExitGame:
                    this.Exit();
                    break;
                case ScreenAction.Test:
                    _currentScreen.HideLeftPanelButtons();
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
