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
    public abstract class BaseMenu<T> where T : Enum
    {
        protected string Title;
        protected List<Button<T>> _leftPanelButtons;
        protected List<Button<T>> _rightPanelButtons;
        protected List<Button<T>> _centerPanelButtons;
        protected List<Button<T>> _buttons;
        protected SpriteFont _font;
        protected Texture2D _pixel;
        protected MenuLayout _menuLayout;
        protected int _spacing = 10;
        protected const int _defaultButtonWidth = 100;
        protected const int _defaultButtonHeight = 50;
        protected InfoDialog _infoDialog;

        public BaseMenu(string title, Rectangle frame, SpriteFont font, Texture2D pixel)
        {
            Title = title;
            _menuLayout = new MenuLayout(frame);
            _font = font;
            _pixel = pixel;
            _buttons = new List<Button<T>>();
            _leftPanelButtons = new List<Button<T>>();
            _centerPanelButtons = new List<Button<T>>();
            _rightPanelButtons = new List<Button<T>>();
        }

        /*
        protected void ShowInfoWindow(string message, SpriteBatch spriteBatch)
        {
            Vector2 size = _font.MeasureString(message);
            float x_axis = _menuLayout.ContentContainer.Center.X - size.X / 2;
            float y_axis = _menuLayout.ContentContainer.Center.Y - size.Y / 2;
            var position = new Vector2(x_axis, y_axis);
            spriteBatch.DrawString(_font, message, position, Color.White);
        }
        */

        virtual protected void ButtonsEnabledManage()
        {
            if (_infoDialog != null && _infoDialog.IsOpen) 
            {
                foreach (var button in _buttons)
                {
                    button.SetEnabled(false);
                }
            }
            else
            {
                foreach (var button in _buttons)
                {
                    button.SetEnabled(true);
                }
            }
        }

        abstract public ScreenAction Update();

        abstract public void Draw(SpriteBatch spriteBatch);

        #region Button management methods

        public void HideRightPanelButtons()
        {
            foreach (var button in _rightPanelButtons)
            {
                if (button.Bounds.Intersects(_menuLayout.RightPanel))
                {
                    button.HideButton();
                }
            }
        }

        public void HideLeftPanelButtons()
        {
            foreach (var button in _leftPanelButtons)
            {
                if (button.Bounds.Intersects(_menuLayout.LeftPanel))
                {
                    button.HideButton();
                }
            }
        }

        public void HideCenterPanelButtons()
        {
            foreach (var button in _centerPanelButtons)
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

        protected void TurnOffAllButtons()
        {
            foreach (var button in _buttons)
            {
                button.SetEnabled(false);
            }
        }

        protected void AddButtonToRightPanel(string text, T action, int width = _defaultButtonWidth, int height = _defaultButtonHeight)
        {
            var rect = _menuLayout.GetNextRightPanelRect(width, height, _spacing);
            var button = new Button<T>(rect, action, text, _font);
            _rightPanelButtons.Add(button);
            _buttons.Add(button);
        }

        protected void AddButtonToLeftPanel(string text, T action, int width = _defaultButtonWidth, int height = _defaultButtonHeight)
        {
            var rect = _menuLayout.GetNextLeftPanelRect(width, height, _spacing);
            var button = new Button<T>(rect, action, text, _font);
            _leftPanelButtons.Add(button);
            _buttons.Add(button);
        }

        protected void AddButtonToCenterPanel(string text, T action, AddButtonMode mode = AddButtonMode.Center, int width = _defaultButtonWidth, int height = _defaultButtonHeight)
        {
            Rectangle rect = default; 

            switch (mode)
            {
                case AddButtonMode.Center:
                    rect = _menuLayout.GetNextContentCenterRect(width, height, _spacing);
                    break;
                case AddButtonMode.Left:
                    break;
                case AddButtonMode.Right:
                    break;
                case AddButtonMode.Top:
                    rect = _menuLayout.GetNextContentTopRect(width, height, _spacing);
                    break;
                case AddButtonMode.Bottom:
                    rect = _menuLayout.GetNextContentBottomRect(width, height, _spacing);
                    break;
                default:
                    throw new ArgumentException("Invalid AddButtonMode value.");
            }

            var button = new Button<T>(rect, action, text, _font);
            _leftPanelButtons.Add(button);
            _buttons.Add(button);
        }

        private Button<T> CreateButton(int x, int y, int width, int height, string text, T action, SpriteFont font) 
        {
            var newButton = new Button<T>(new Rectangle(x, y, width, height), action, text, _font);
            foreach (var bottom in _buttons)
            {
                if (bottom.IsVisible && bottom.Bounds.Contains(newButton.Bounds))
                {
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

    public enum GameScreen
    {
        MainMenu,
        LoadGameMenu,
        SettingsMenu,
        CharacterMenu,
    }
}
