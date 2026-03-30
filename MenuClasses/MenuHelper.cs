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
        protected SpriteFont _font;
        protected Texture2D _pixel;
        protected MenuLayout _menuLayout;
        protected int _spacing = 10;
        private const int _defaultButtonWidth = 100;
        private const int _defaultButtonHeight = 50;

        public BaseMenu(string title, Viewport viewport, SpriteFont font, Texture2D pixel)
        {
            Title = title;
            _menuLayout = new MenuLayout(viewport);
            _font = font;
            _pixel = pixel;
            _buttons = new List<Button>();
        }

        abstract public ScreenAction Update();

        abstract public void Draw(SpriteBatch spriteBatch);

        protected void AddButtonToRightPanel(string text, ScreenAction action, int width = _defaultButtonWidth, int height = _defaultButtonHeight)
        {
            if (_menuLayout.RightPanel.Bottom < (_menuLayout.RightPanelCurrentY + _spacing + height))
            {
                throw new InvalidOperationException("Not enough space to add more buttons to the right panel.");
            }

            _menuLayout.RightPanelCurrentY += _spacing;

            int x_axis = (int)(_menuLayout.RightPanel.Center.X - width / 2);
            int y_axis = _menuLayout.RightPanelCurrentY;
            var button = new Button(new Rectangle(x_axis, y_axis, width, height), action, text, _font);

            _menuLayout.RightPanelCurrentY += height;
            _buttons.Add(button);
        }

        protected void AddButtonToLeftPanel(string text, ScreenAction action, int width = _defaultButtonWidth, int height = _defaultButtonHeight)
        {
            if (_menuLayout.LeftPanel.Bottom < (_menuLayout.LeftPanelCurrentY + _spacing + height))
            {
                throw new InvalidOperationException("Not enough space to add more buttons to the left panel.");
            }
            _menuLayout.LeftPanelCurrentY += _spacing;
            int x_axis = (int)(_menuLayout.LeftPanel.Center.X - width / 2);
            int y_axis = _menuLayout.LeftPanelCurrentY;
            var button = new Button(new Rectangle(x_axis, y_axis, width, height), action, text, _font);
            _menuLayout.LeftPanelCurrentY += height;
            _buttons.Add(button);
        }

        protected void AddButtonToCenterPanelBottom(string text, ScreenAction action, int width = _defaultButtonWidth, int height = _defaultButtonHeight)
        {
            if (_menuLayout.ContentContainer.Right < (_menuLayout.ContentContainerCurrentX + _spacing + width))
            {
                throw new InvalidOperationException("Not enough space to add more buttons to the center panel.");
            }
            _menuLayout.ContentContainerCurrentX += _spacing;
            int x_axis = _menuLayout.ContentContainerCurrentX;
            int y_axis = _menuLayout.ContentContainer.Bottom - height - _spacing;
            var newButton = new Button(new Rectangle(x_axis, y_axis, width, height), action, text, _font);

            foreach (var bottom in _buttons)
            {
                if (bottom.IsVisible && bottom.Bounds.Contains(newButton.Bounds))
                {
                    _menuLayout.ContentContainerCurrentX -= _spacing;
                    throw new InvalidOperationException("New button overlaps with an existing button. Please adjust the layout or reduce the number of buttons.");
                }
            }

            _menuLayout.ContentContainerCurrentX += width;
            _buttons.Add(newButton);
        }

        protected void AddButtonToCenterPanelTop(string text, ScreenAction action, int width = _defaultButtonWidth, int height = _defaultButtonHeight)
        {
            if (_menuLayout.ContentContainer.Right < (_menuLayout.ContentContainerCurrentX + _spacing + width))
            {
                throw new InvalidOperationException("Not enough space to add more buttons to the center panel.");
            }
            _menuLayout.ContentContainerCurrentX += _spacing;
            int x_axis = _menuLayout.ContentContainerCurrentX;
            int y_axis = _menuLayout.ContentContainer.Top + _spacing;
            var newButton = new Button(new Rectangle(x_axis, y_axis, width, height), action, text, _font);

            foreach (var bottom in _buttons) 
            {
                if (bottom.IsVisible && bottom.Bounds.Contains(newButton.Bounds))
                {
                    _menuLayout.ContentContainerCurrentX -= _spacing;
                    throw new InvalidOperationException("New button overlaps with an existing button. Please adjust the layout or reduce the number of buttons.");
                }
            }

            _menuLayout.ContentContainerCurrentX += width;
            _buttons.Add(newButton);
        }


        protected void SetTitle(SpriteBatch spriteBatch)
        {
            Vector2 size = _font.MeasureString(Title);
            float x_axis = _menuLayout.Screen.Center.X - size.X / 2;
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
