using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyMonoGame.MenuClasses
{
    public abstract class BaseMenu
    {
        protected string Title;
        protected List<Button> _buttons;
        protected int _windowHeight;
        protected int _windowWidth;
        protected SpriteFont _font;
        protected Texture2D _pixel;

        public BaseMenu(string title, int windowHeight, int windowWidth, SpriteFont font, Texture2D pixel)
        {
            Title = title;
            _windowHeight = windowHeight;
            _windowWidth = windowWidth;
            _font = font;
            _pixel = pixel;
            _buttons = new List<Button>();
        }

        abstract public ScreenAction Update();

        abstract public void Draw(SpriteBatch spriteBatch);

        protected void SetTitle(SpriteBatch spriteBatch)
        {
            Vector2 size = _font.MeasureString(Title);
            float x_axis = _windowWidth / 2 - size.X / 2;
            float y_axis = size.Y + 5;
            var position = new Vector2(x_axis, y_axis);
            spriteBatch.DrawString(_font, Title, position, Color.White);
        }
    }

    public enum ScreenAction
    {
        None,
        GoToCharacterMenu,
        GoToMainMenu,
        GoToLoadGameMenu,
        GoToSettingsMenu,
        GoToAboutGameMenu,
        AddCharacter,
        EditCharacter,
        DeleteCharacter,
        StartGame,
        ExitGame
    }

    public enum GameScreen
    {
        MainMenu,
        LoadGameMenu,
        SettingsMenu,
        CharacterMenu,
    }
}
