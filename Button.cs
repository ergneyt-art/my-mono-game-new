using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyMonoGame
{
    public class Button
    {
        public Rectangle Bounds;
        public string Text;

        public bool IsVisible { get; private set; } = true;
        public bool IsEnabled { get; private set; } = true;
        public bool IsHovered { get; private set; }
        public bool IsClicked { get; private set; }

        private Color ButtonUnenabledColor = Color.Gray;
        private Color ButtonHoverdColor = Color.DarkBlue;
        private Color ButtonBaseColor = Color.Blue;
        private Color TextColor = Color.White;
        private Vector2 _textPosition;

        private MouseState _previousMouse;

        public Button(Rectangle bounds, string text, SpriteFont font)
        {
            Bounds = bounds;
            Text = text;
            Vector2 size = font.MeasureString(Text);
            RecalculateTextPosition(text, font);
        }

        private void RecalculateTextPosition(string text, SpriteFont font)
        {
            Vector2 size = font.MeasureString(Text);
            float x_axis = this.Bounds.X + Bounds.Width / 2 - size.X / 2;
            float y_axis = this.Bounds.Y + Bounds.Height / 2 - size.Y / 2;
            _textPosition = new Vector2(x_axis, y_axis);
        }

        public void SetVisible(bool visible)
        {
            this.IsVisible = visible;
        }

        public void SetEnabled(bool enabled) 
        {
            this.IsEnabled = enabled;
        }

        public void Update() 
        {
            if (this.IsVisible && this.IsEnabled)
            {
                MouseState mouse = Mouse.GetState();
                IsHovered = Bounds.Contains(mouse.Position);

                bool leftClicked = mouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released;

                IsClicked = IsHovered && leftClicked;

                _previousMouse = mouse;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D pixel)
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
                spriteBatch.DrawString(font, Text, _textPosition, TextColor);
            }
        }
    }
}
