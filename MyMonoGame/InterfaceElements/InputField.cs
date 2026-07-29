using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGame.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.InterfaceElements
{
    public class InputField : BaseActiveElement
    {
        public string Text { get; set; }
        public bool IsActive { get; private set; }

        private const int _maxTextLength = 20;
        KeyboardState _previousKeyboard;
        MouseState _previousMouse;

        public InputField(Rectangle bound, GameContext context) : base(bound, context)
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

        public override void Draw()
        {
            if (!IsVisible) return;
            if (IsActive)
            {
                Context.SpriteBatch.Draw(Context.Pixel, Bounds, Color.White);
            }
            else
            {
                Context.SpriteBatch.Draw(Context.Pixel, Bounds, Color.Gray);
            }
            Vector2 textPosition = TextHelper.RecalculateTextPosition(Text, Bounds, Context.Font);
            Context.SpriteBatch.DrawString(Context.Font, Text, textPosition, Color.Black);
        }
    }
}
