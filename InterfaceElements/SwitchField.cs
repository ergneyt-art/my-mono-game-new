using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGame.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyMonoGame.InterfaceElements
{
    public class SwitchField<T> where T : Enum
    {
        private string _label;
        public T Value { get; private set; }
        public Rectangle Bound { get; private set; }
        public bool IsEnabled { get; private set; } = true;
        public bool IsVisible { get; private set; } = true;

        private const double _swichButtonWidth = 0.2;

        private Button<SwitchFieldAction> _nextButton;
        private Button<SwitchFieldAction> _previousButton;

        private Rectangle _labelBox;
        private Rectangle _valueBox;
        public SwitchField(Rectangle bound, string label, T initialValue, SpriteFont font)
        {
            Bound = bound;
            _labelBox = new Rectangle(Bound.X, Bound.Y, bound.Width, bound.Height / 2);
            _valueBox = new Rectangle(Bound.X, _labelBox.Bottom, bound.Width, bound.Height / 2);
            var buttonWidth = (int)(bound.Width * _swichButtonWidth);
            var nextButtonRect = new Rectangle(_valueBox.Right - buttonWidth, _valueBox.Top, buttonWidth, _valueBox.Height);
            var previousButtonRect = new Rectangle(_valueBox.Left, _valueBox.Top, buttonWidth, _valueBox.Height);
            _nextButton = new Button<SwitchFieldAction>(nextButtonRect, SwitchFieldAction.SwitchToNextValue, ">", font);
            _previousButton = new Button<SwitchFieldAction>(previousButtonRect, SwitchFieldAction.SwitchToPreviousValue, "<", font);
            _label = label;
            _value = initialValue;
        }

        public void Update()
        {
            if (IsVisible && IsEnabled)
            {
                _nextButton.Update();
                _previousButton.Update();
                if (_nextButton.IsClicked)
                {
                    SwitchToNextValue();
                }
                else if (_previousButton.IsClicked)
                {
                    SwitchToPreviousValue();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D texture)
        {
            if (!IsVisible) return;
            Vector2 labelPosition = RecalculateTextPosition(_label, font, _labelBox);
            Vector2 valuePosition = RecalculateTextPosition(_value.ToString(), font, _valueBox);
            spriteBatch.DrawString(font, _label, labelPosition, Color.White);
            spriteBatch.DrawString(font, _value.ToString(), valuePosition, Color.White);
            _nextButton.Draw(spriteBatch, font, texture);
            _previousButton.Draw(spriteBatch, font, texture);
        }



        private Vector2 RecalculateTextPosition(string text, SpriteFont font, Rectangle frame)
        {
            Vector2 size = font.MeasureString(text);
            float x_axis = frame.Center.X - (size.X / 2);
            float y_axis = frame.Center.Y - (size.Y / 2);
            return new Vector2(x_axis, y_axis);
        }

        private void SwitchToNextValue()
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            int nextIndex = GetNextEnumValueIndex();
            _value = values[nextIndex];
        }

        private void SwitchToPreviousValue()
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            int previousIndex = GetPreviousEnumValueIndex();
            _value = values[previousIndex];
        }

        private int GetNextEnumValueIndex()
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            int currentIndex = values.IndexOf(_value);
            if (currentIndex >= values.Count) return 0;
            return (currentIndex + 1) % values.Count;
        }

        private int GetPreviousEnumValueIndex()
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            int currentIndex = values.IndexOf(_value);
            if (currentIndex == 0) return values.Count - 1;
            return (currentIndex - 1 + values.Count) % values.Count;
        }
    }
}
