using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    /// <summary>
    /// Common base class for menu-like screens that return actions from UI input.
    /// </summary>
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

        protected GameContext Context { get; set; }
        protected MenuLayout _menuLayout;
        protected const int _defaultSpacing = 10;
        protected const int _defaultButtonWidth = 100;
        protected const int _defaultButtonHeight = 50;
        protected InfoDialog _infoDialog;

        /// <summary>
        /// Creates a menu with a custom layout configuration.
        /// </summary>
        public BaseMenu(string title, MenuLayoutConfig screenConfig, Rectangle frame, GameContext context)
        {
            Title = title;
            _menuLayout = new MenuLayout(frame, screenConfig);
            Context = context;
            _buttons = new List<Button<T>>();
            _leftPanelButtons = new List<Button<T>>();
            _centerPanelButtons = new List<Button<T>>();
            _rightPanelButtons = new List<Button<T>>();
            _leftPanelCursor = _menuLayout.GetCursor(MenuLayoutArea.LeftPanel);
            _centerPanelCursor = _menuLayout.GetCursor(MenuLayoutArea.Content);
            _rightPanelCursor = _menuLayout.GetCursor(MenuLayoutArea.RightPanel);
        }

        /// <summary>
        /// Creates a menu with the default layout configuration.
        /// </summary>
        public BaseMenu(string title, Rectangle frame, GameContext context)
        {
            Title = title;
            _menuLayout = new MenuLayout(frame);
            Context = context;
            _buttons = new List<Button<T>>();
            _leftPanelButtons = new List<Button<T>>();
            _centerPanelButtons = new List<Button<T>>();
            _rightPanelButtons = new List<Button<T>>();
            _leftPanelCursor = new PanelCursor(_menuLayout.LeftPanel);
            _centerPanelCursor = new PanelCursor(_menuLayout.ContentContainer);
            _rightPanelCursor = new PanelCursor(_menuLayout.RightPanel);
        }

        /// <summary>
        /// Enables or disables menu buttons depending on modal dialog state.
        /// </summary>
        virtual protected void ButtonsEnabledManage()
        {
            if (_infoDialog != null && _infoDialog.IsOpen) 
            {
                foreach (var button in _buttons)
                {
                    button.DisallowInteraction();
                }
            }
            else
            {
                foreach (var button in _buttons)
                {
                    button.AllowInteraction();
                }
            }
        }

        /// <summary>
        /// Updates this menu and returns the requested screen action.
        /// </summary>
        abstract public ScreenAction Update();

        /// <summary>
        /// Draws the menu title, buttons, and active dialog if present.
        /// </summary>
        virtual public void Draw()
        {
            SetTitle(Context.SpriteBatch);
            foreach (var button in _buttons)
            {
                button.Draw();
            }
            if (_infoDialog != null)
            {
                _infoDialog.Draw();
            }
        }

        #region Button management methods

        /// <summary>
        /// Hides buttons that belong to the right panel.
        /// </summary>
        public void HideRightPanelButtons()
        {
            foreach (var button in _rightPanelButtons)
            {
                if (button.Bounds.Intersects(_menuLayout.RightPanel))
                {
                    button.Hide();
                }
            }
        }

        /// <summary>
        /// Hides buttons that belong to the left panel.
        /// </summary>
        public void HideLeftPanelButtons()
        {
            foreach (var button in _leftPanelButtons)
            {
                if (button.Bounds.Intersects(_menuLayout.LeftPanel))
                {
                    button.Hide();
                }
            }
        }

        /// <summary>
        /// Hides buttons that belong to the content panel.
        /// </summary>
        public void HideCenterPanelButtons()
        {
            foreach (var button in _centerPanelButtons)
            {
                if (button.Bounds.Intersects(_menuLayout.ContentContainer))
                {
                    button.Hide();
                }
            }
        }

        /// <summary>
        /// Hides every button owned by the menu.
        /// </summary>
        public void HideAllButtons()
        {
            foreach (var button in _buttons)
            {
                button.Hide();
            }
        }

        protected void TurnOffAllButtons()
        {
            foreach (var button in _buttons)
            {
                button.DisallowInteraction();
            }
        }

        /// <summary>
        /// Creates a button using the provided cursor and stores it in the main button list.
        /// </summary>
        protected Button<T> AddButton(string text, T action, PanelCursor panelCursor, Direction direction = Direction.Down, int width = _defaultButtonWidth, int height = _defaultButtonHeight, int spacing = _defaultSpacing)
        {
            var rect = panelCursor.GetNextRect(direction, width, height, spacing);
            var button = new Button<T>(rect, action, text, Context);
            _buttons.Add(button);
            return button;
        }

        /// <summary>
        /// Adds a button to the left panel and tracks it in the left button group.
        /// </summary>
        protected void AddButtonToLeftPanel(string label, T action, Direction direction = Direction.Down, int width = _defaultButtonWidth, int height = _defaultButtonHeight, int spacing = _defaultSpacing)
        {
            UpdateCursorPosition(_leftPanelButtons, _leftPanelCursor, _menuLayout.LeftPanel, direction, width, height, spacing);
            var rect = _leftPanelCursor.GetNextRect(direction, width, height, spacing);
            var button = new Button<T>(rect, action, label, Context);
            _leftPanelButtons.Add(button);
            _buttons.Add(button);
        }

        /// <summary>
        /// Adds a button to the right panel and tracks it in the right button group.
        /// </summary>
        protected void AddButtonToRightPanel(string label, T action, Direction direction = Direction.Down, int width = _defaultButtonWidth, int height = _defaultButtonHeight, int spacing = _defaultSpacing)
        {
            UpdateCursorPosition(_rightPanelButtons, _rightPanelCursor, _menuLayout.RightPanel, direction, width, height, spacing);
            var rect = _rightPanelCursor.GetNextRect(direction, width, height, spacing);
            var button = new Button<T>(rect, action, label, Context);
            _rightPanelButtons.Add(button);
            _buttons.Add(button);
        }

        /// <summary>
        /// Adds a button to the content panel and tracks it in the center button group.
        /// </summary>
        protected void AddButtonToCenterPanel(string label, T action, Direction direction = Direction.Down, int width = _defaultButtonWidth, int height = _defaultButtonHeight, int spacing = _defaultSpacing)
        {
            UpdateCursorPosition(_centerPanelButtons, _centerPanelCursor, _menuLayout.ContentContainer, direction, width, height, spacing);
            var rect = _centerPanelCursor.GetNextRect(direction, width, height, spacing);
            var button = new Button<T>(rect, action, label, Context);
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
            Vector2 size = Context.Font.MeasureString(Title);
            float x_axis = _menuLayout.HeaderContainer.Center.X - size.X / 2;
            float y_axis = size.Y + spacing;
            var position = new Vector2(x_axis, y_axis);
            spriteBatch.DrawString(Context.Font, Title, position, Color.White);
        }
    }
}
