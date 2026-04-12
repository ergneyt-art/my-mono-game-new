using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.Helpers;
using MyMonoGame.InterfaceElements;
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

        protected PanelCursor _leftPanelCursor;
        protected PanelCursor _rightPanelCursor;
        protected PanelCursor _centerPanelCursor;

        protected SpriteFont _font;
        protected Texture2D _pixel;
        protected MenuLayout _menuLayout;
        protected const int _defaultSpacing = 10;
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
            _leftPanelCursor = new PanelCursor(_menuLayout.LeftPanel);
            _centerPanelCursor = new PanelCursor(_menuLayout.ContentContainer);
            _rightPanelCursor = new PanelCursor(_menuLayout.RightPanel);
        }

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
                    button.HideElement();
                }
            }
        }

        public void HideLeftPanelButtons()
        {
            foreach (var button in _leftPanelButtons)
            {
                if (button.Bounds.Intersects(_menuLayout.LeftPanel))
                {
                    button.HideElement();
                }
            }
        }

        public void HideCenterPanelButtons()
        {
            foreach (var button in _centerPanelButtons)
            {
                if (button.Bounds.Intersects(_menuLayout.ContentContainer))
                {
                    button.HideElement();
                }
            }
        }

        public void HideAllButtons()
        {
            foreach (var button in _buttons)
            {
                button.HideElement();
            }
        }

        protected void TurnOffAllButtons()
        {
            foreach (var button in _buttons)
            {
                button.SetEnabled(false);
            }
        }

        protected Button<T> AddButton(string text, T action, PanelCursor panelCursor, Direction direction = Direction.Down, int width = _defaultButtonWidth, int height = _defaultButtonHeight, int spacing = _defaultSpacing)
        {
            var rect = panelCursor.GetNextRect(direction, width, height, spacing);
            var button = new Button<T>(rect, action, text, _font);
            _buttons.Add(button);
            return button;
        }

        protected void AddButtonToLeftPanel(string label, T action, Direction direction = Direction.Down, int width = _defaultButtonWidth, int height = _defaultButtonHeight, int spacing = _defaultSpacing)
        {
            UpdateCursorPosition(_leftPanelButtons, _leftPanelCursor, _menuLayout.LeftPanel, direction, width, height, spacing);
            var rect = _leftPanelCursor.GetNextRect(direction, width, height, spacing);
            var button = new Button<T>(rect, action, label, _font);
            _leftPanelButtons.Add(button);
            _buttons.Add(button);
        }

        protected void AddButtonToRightPanel(string label, T action, Direction direction = Direction.Down, int width = _defaultButtonWidth, int height = _defaultButtonHeight, int spacing = _defaultSpacing)
        {
            UpdateCursorPosition(_rightPanelButtons, _rightPanelCursor, _menuLayout.RightPanel, direction, width, height, spacing);
            var rect = _rightPanelCursor.GetNextRect(direction, width, height, spacing);
            var button = new Button<T>(rect, action, label, _font);
            _rightPanelButtons.Add(button);
            _buttons.Add(button);
        }

        protected void AddButtonToCenterPanel(string label, T action, Direction direction = Direction.Down, int width = _defaultButtonWidth, int height = _defaultButtonHeight, int spacing = _defaultSpacing)
        {
            UpdateCursorPosition(_centerPanelButtons, _centerPanelCursor, _menuLayout.ContentContainer, direction, width, height, spacing);
            var rect = _centerPanelCursor.GetNextRect(direction, width, height, spacing);
            var button = new Button<T>(rect, action, label, _font);
            _centerPanelButtons.Add(button);
            _buttons.Add(button);
        }

        private int UpdateCursorPosition(List<Button<T>> buttons, PanelCursor cursor, Rectangle panel, Direction direction, int width, int height, int spacing)
        {
            var point = 0;
            switch (direction)
            {
                case Direction.Up:
                    point = buttons.Count > 0 ? buttons.Min(b => b.Bounds.Top) : panel.Bottom;
                    cursor.SetPosition(panel.Center.X - width / 2, point - spacing);
                    break;
                case Direction.Down:
                    point = buttons.Count > 0 ? buttons.Max(b => b.Bounds.Bottom) : panel.Top;
                    cursor.SetPosition(panel.Center.X - width / 2, point + spacing);
                    break;
                case Direction.Left:
                    point = buttons.Count > 0 ? buttons.Min(b => b.Bounds.Left) : panel.Right;
                    cursor.SetPosition(point - spacing, panel.Center.Y - height / 2);
                    break;
                case Direction.Right:
                    point = buttons.Count > 0 ? buttons.Max(b => b.Bounds.Right) : panel.Left;
                    cursor.SetPosition(point + spacing, panel.Center.Y - height / 2);
                    break;
                default:
                    break;
            }
            return point;
        }

        #endregion


        protected void SetTitle(SpriteBatch spriteBatch, int spacing = _defaultSpacing)
        {
            Vector2 size = _font.MeasureString(Title);
            float x_axis = _menuLayout.HeaderContainer.Center.X - size.X / 2;
            float y_axis = size.Y + spacing;
            var position = new Vector2(x_axis, y_axis);
            spriteBatch.DrawString(_font, Title, position, Color.White);
        }
    }
}
