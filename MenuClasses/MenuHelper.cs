using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
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
        protected const int _defaultButtonWidth = 100;
        protected const int _defaultButtonHeight = 50;

        public BaseMenu(string title, Viewport viewport, SpriteFont font, Texture2D pixel)
        {
            Title = title;
            _menuLayout = new MenuLayout(viewport);
            _font = font;
            _pixel = pixel;
            _buttons = new List<Button>();
        }

        protected void ShowInfoWindow(string message, SpriteBatch spriteBatch)
        {
            Vector2 size = _font.MeasureString(message);
            float x_axis = _menuLayout.ContentContainer.Center.X - size.X / 2;
            float y_axis = _menuLayout.ContentContainer.Center.Y - size.Y / 2;
            var position = new Vector2(x_axis, y_axis);
            spriteBatch.DrawString(_font, message, position, Color.White);
        }

        abstract public ScreenAction Update();

        abstract public void Draw(SpriteBatch spriteBatch);

        #region Button management methods

        public void HideRightPanelButtons()
        {
            foreach (var button in _buttons)
            {
                if (button.Bounds.Intersects(_menuLayout.RightPanel))
                {
                    button.HideButton();
                }
            }
        }

        public void HideLeftPanelButtons()
        {
            foreach (var button in _buttons)
            {
                if (button.Bounds.Intersects(_menuLayout.LeftPanel))
                {
                    button.HideButton();
                }
            }
        }

        public void HideCenterPanelButtons()
        {
            foreach (var button in _buttons)
            {
                if (button.Bounds.Intersects(_menuLayout.ContentContainer))
                {
                    button.HideButton();
                }
            }
        }

        public void HideAllButtons()
        {
            foreach (var button in _buttons)
            {
                button.HideButton();
            }
        }

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

        protected void AddButtonToCenterPanel(string text, ScreenAction action, AddButtonMode mode = AddButtonMode.Center, int width = _defaultButtonWidth, int height = _defaultButtonHeight)
        {
            var x = 0;
            var y = 0;
            switch (mode)
            {
                case AddButtonMode.Center:
                    x = _menuLayout.ContentContainer.Center.X - width / 2;
                    _menuLayout.ContentContainerCurrentY += _spacing;
                    y = _menuLayout.ContentContainerCurrentY;
                    _buttons.Add(CreateButton(x, y, width, height, text, action, _font));
                    _menuLayout.ContentContainerCurrentY += height;
                    break;
                case AddButtonMode.Left:
                    x = _spacing;
                    _menuLayout.ContentContainerCurrentY += _spacing;
                    y = _menuLayout.ContentContainerCurrentY;
                    _buttons.Add(CreateButton(x, y, width, height, text, action, _font));
                    _menuLayout.ContentContainerCurrentY += height;
                    break;
                case AddButtonMode.Right:
                    x = _menuLayout.ContentContainer.Right - _spacing;
                    _menuLayout.ContentContainerCurrentY += _spacing;
                    y = _menuLayout.ContentContainerCurrentY;
                    _buttons.Add(CreateButton(x, y, width, height, text, action, _font));
                    _menuLayout.ContentContainerCurrentY += height;
                    break;
                case AddButtonMode.Top:
                    x = _menuLayout.ContentContainerCurrentX;
                    y = _menuLayout.ContentContainer.Top + _spacing;
                    _buttons.Add(CreateButton(x, y, width, height, text, action, _font));
                    _menuLayout.ContentContainerCurrentX += width;
                    break;
                case AddButtonMode.Bottom:
                    x = _menuLayout.ContentContainerCurrentX;
                    y = _menuLayout.ContentContainer.Bottom - height - _spacing;
                    _buttons.Add(CreateButton(x, y, width, height, text, action, _font));
                    _menuLayout.ContentContainerCurrentX += width;
                    break;
                default:
                    throw new ArgumentException("Invalid AddButtonMode value.");

            }
        }

        private Button CreateButton(int x, int y, int width, int height, string text, ScreenAction action, SpriteFont font) 
        {
            var newButton = new Button(new Rectangle(x, y, width, height), action, text, _font);
            foreach (var bottom in _buttons)
            {
                if (bottom.IsVisible && bottom.Bounds.Contains(newButton.Bounds))
                {
                    _menuLayout.ContentContainerCurrentX -= _spacing;
                    throw new InvalidOperationException("New button overlaps with an existing button. Please adjust the layout or reduce the number of buttons.");
                }
            }
            return newButton;
        }

        #endregion


        protected void SetTitle(SpriteBatch spriteBatch)
        {
            Vector2 size = _font.MeasureString(Title);
            float x_axis = _menuLayout.HeaderContainer.Center.X - size.X / 2;
            float y_axis = size.Y + _spacing;
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
        Test,
        ExitGame
    }

    public enum AddButtonMode
    {
        Top,
        Left,
        Right,
        Bottom,
        Center,
    }

    public enum GameScreen
    {
        MainMenu,
        LoadGameMenu,
        SettingsMenu,
        CharacterMenu,
    }
}
