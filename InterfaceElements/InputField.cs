using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.InterfaceElements
{
    public class InputField : BaseInterfaceElement
    {
        public string Text { get; set; }
        public bool IsActive { get; private set; }

        private const int _maxTextLength = 20;
        KeyboardState _previousKeyboard;
        MouseState _previousMouse;

        public InputField(Rectangle bound, SpriteFont font) : base(bound, font)
        {
            Text = string.Empty;
        }

        public void Update()
        {
            if (IsVisible && IsEnabled)
            {
                var mouse = Mouse.GetState();
                var keyboard = Keyboard.GetState();
                if (mouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released)
                {
                    IsActive = Bounds.Contains(mouse.Position);
                }
                if (IsActive)
                {
                    foreach (var key in keyboard.GetPressedKeys())
                    {
                        if (!_previousKeyboard.IsKeyDown(key))
                        {
                            if (key == Keys.Back && Text.Length > 0)
                            {
                                Text = Text.Substring(0, Text.Length - 1);
                            }
                            else if (Text.Length < _maxTextLength)
                            {
                                var keyString = key.ToString();
                                if (keyString.Length == 1)
                                {
                                    Text += keyString;
                                }
                                else if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
                                {
                                    Text += (key - Keys.NumPad0).ToString();
                                }
                                else if (key >= Keys.D0 && key <= Keys.D9)
                                {
                                    Text += (key - Keys.D0).ToString();
                                }
                            }
                        }
                    }
                }
                _previousKeyboard = keyboard;
                _previousMouse = mouse;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (!IsVisible) return;
            if (IsActive)
            {
                spriteBatch.Draw(texture, Bounds, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, Bounds, Color.Gray);
            }
            Vector2 textPosition = RecalculateTextPosition(Text, _font);
            spriteBatch.DrawString(_font, Text, textPosition, Color.Black);
        }

        private Vector2 RecalculateTextPosition(string text, SpriteFont font)
        {
            Vector2 size = font.MeasureString(text);
            float x_axis = Bounds.X + Bounds.Width / 2 - size.X / 2;
            float y_axis = Bounds.Y + Bounds.Height / 2 - size.Y / 2;
            return new Vector2(x_axis, y_axis);
        }
    }
}
