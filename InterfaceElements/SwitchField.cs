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
    public class SwitchField<T> : BaseInterfaceElement where T : Enum
    {
        private string _label;
        public T Value { get; set; }

        private const int _swichButtonWidth = 15;

        private Button<SwitchFieldAction> _nextButton;
        private Button<SwitchFieldAction> _previousButton;

        private Rectangle _labelBox;
        private Rectangle _valueBox;
        public SwitchField(Rectangle bound, string label, T initialValue, SpriteFont font) : base(bound, font)
        {
            _labelBox = new Rectangle(Bounds.X, Bounds.Y, bound.Width, bound.Height / 2);
            _valueBox = new Rectangle(Bounds.X, _labelBox.Bottom, bound.Width, bound.Height / 2);
            var nextButtonRect = new Rectangle(_valueBox.Right - _swichButtonWidth, _valueBox.Top, _swichButtonWidth, _valueBox.Height);
            var previousButtonRect = new Rectangle(_valueBox.Left, _valueBox.Top, _swichButtonWidth, _valueBox.Height);
            _nextButton = new Button<SwitchFieldAction>(nextButtonRect, SwitchFieldAction.SwitchToNextValue, ">", _font);
            _previousButton = new Button<SwitchFieldAction>(previousButtonRect, SwitchFieldAction.SwitchToPreviousValue, "<", font);
            _label = label;
            Value = initialValue;
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

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (!IsVisible) return;
            Vector2 labelPosition = RecalculateTextPosition(_label, _labelBox);
            Vector2 valuePosition = RecalculateTextPosition(Value.ToString(), _valueBox);
            spriteBatch.DrawString(_font, _label, labelPosition, Color.White);
            spriteBatch.DrawString(_font, Value.ToString(), valuePosition, Color.White);
            _nextButton.Draw(spriteBatch, texture);
            _previousButton.Draw(spriteBatch, texture);
        }

        private void SwitchToNextValue()
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            int nextIndex = GetNextEnumValueIndex();
            Value = values[nextIndex];
        }

        private void SwitchToPreviousValue()
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            int previousIndex = GetPreviousEnumValueIndex();
            Value = values[previousIndex];
        }

        private int GetNextEnumValueIndex()
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            int currentIndex = values.IndexOf(Value);
            if (currentIndex >= values.Count) return 0;
            return (currentIndex + 1) % values.Count;
        }

        private int GetPreviousEnumValueIndex()
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            int currentIndex = values.IndexOf(Value);
            if (currentIndex == 0) return values.Count - 1;
            return (currentIndex - 1 + values.Count) % values.Count;
        }
    }
}
