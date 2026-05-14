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
    public class SwitchField<T> : BaseActiveElement where T : Enum
    {
        private string _label;
        public T Value { get; set; }

        private const int _swichButtonWidth = 15;

        private Button<SwitchFieldAction> _nextButton;
        private Button<SwitchFieldAction> _previousButton;

        private Rectangle _labelBox;
        private Rectangle _valueBox;
        public SwitchField(Rectangle bound, string label, T initialValue, GameContext context) : base(bound, context)
        {
            _labelBox = new Rectangle(Bounds.X, Bounds.Y, bound.Width, bound.Height / 2);
            _valueBox = new Rectangle(Bounds.X, _labelBox.Bottom, bound.Width, bound.Height / 2);
            var nextButtonRect = new Rectangle(_valueBox.Right - _swichButtonWidth, _valueBox.Top, _swichButtonWidth, _valueBox.Height);
            var previousButtonRect = new Rectangle(_valueBox.Left, _valueBox.Top, _swichButtonWidth, _valueBox.Height);
            _nextButton = new Button<SwitchFieldAction>(nextButtonRect, SwitchFieldAction.SwitchToNextValue, ">", Context);
            _previousButton = new Button<SwitchFieldAction>(previousButtonRect, SwitchFieldAction.SwitchToPreviousValue, "<", Context);
            _label = label;
            Value = initialValue;
        }

        public void Update()
        {
            if (IsVisible && IsEnabled)
            {
                _nextButton.Update();
                _previousButton.Update();
                if (_nextButton.GetClickedStatus())
                {
                    SwitchToNextValue();
                }
                else if (_previousButton.GetClickedStatus())
                {
                    SwitchToPreviousValue();
                }
            }
        }

        public override void Draw()
        {
            if (!IsVisible) return;
            Vector2 labelPosition = TextHelper.RecalculateTextPosition(_label, _labelBox, Context.Font);
            Vector2 valuePosition = TextHelper.RecalculateTextPosition(Value.ToString(), _valueBox, Context.Font);
            Context.SpriteBatch.DrawString(Context.Font, _label, labelPosition, Color.White);
            Context.SpriteBatch.DrawString(Context.Font, Value.ToString(), valuePosition, Color.White);
            _nextButton.Draw();
            _previousButton.Draw();
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
