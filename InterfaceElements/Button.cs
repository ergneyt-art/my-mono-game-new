using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGame.MenuClasses;

namespace MyMonoGame.InterfaceElements
{
    public class Button<T> : BaseInterfaceElement where T : Enum
    {
        public T Action { get; set; }
        public string Text { get; private set; }
        public bool IsClicked { get; private set; }

        private Color ButtonUnenabledColor = Color.Gray;
        private Color ButtonHoverdColor = Color.DarkBlue;
        private Color ButtonBaseColor = Color.Blue;
        private Color TextColor = Color.White;
        private Vector2 _textPosition;

        private MouseState _previousMouse;

        public Button(Rectangle bounds, T action, string text, SpriteFont font) : base(bounds, font)
        {
            Text = text;
            _textPosition = RecalculateTextPosition(text);
            Action = action;
        }



        public void Update() 
        {
            UpdateHoveredState();
            if (this.IsVisible && this.IsEnabled)
            {
                MouseState mouse = Mouse.GetState();

                bool leftClicked = mouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released;

                _previousMouse = mouse;

                if (IsHovered && leftClicked)
                {
                    IsClicked = true;
                    return;
                }
            }
            IsClicked = false;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {
            if (IsVisible)
            {
                Color color = ButtonBaseColor;
                if (!this.IsEnabled) 
                { 
                    color = ButtonUnenabledColor; 
                }
                else if (this.IsHovered)
                {
                    color = ButtonHoverdColor;
                }
                spriteBatch.Draw(pixel, Bounds, color);
                spriteBatch.DrawString(_font, Text, _textPosition, TextColor);
                if (IsHovered && this.Tooltip != null && this.Tooltip.IsShow)
                {
                    this.Tooltip.Draw(spriteBatch, _font, pixel);
                }
            }
        }
    }
}
