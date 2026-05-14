using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGame.Helpers;
using MyMonoGame.MenuClasses;

namespace MyMonoGame.InterfaceElements
{
    public class Button<T> : BaseActiveElement where T : Enum
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

        public Button(Rectangle bounds, T action, string text, GameContext context) : base(bounds, context)
        {
            Text = text;
            _textPosition = TextHelper.RecalculateTextPosition(Text, Bounds, Context.Font);
            Action = action;
        }

        public bool GetClickedStatus()
        {
            if (IsClicked)
            {
                IsClicked = false;
                return true;
            }
            else
            {
                return false;
            }
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

        public override void Draw()
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
                Context.SpriteBatch.Draw(Context.Pixel, Bounds, color);
                Context.SpriteBatch.DrawString(Context.Font, Text, _textPosition, TextColor);
                /*
                if (IsHovered && this.Tooltip != null && this.Tooltip.IsShow)
                {
                    this.Tooltip.Draw(spriteBatch, _font, pixel);
                }
                */
            }
        }
    }
}
