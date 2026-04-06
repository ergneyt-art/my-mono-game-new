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
