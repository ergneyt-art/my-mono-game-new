using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.InterfaceElements
{
    public class ValueBar : BaseElementWithTooltip
    {
        public int CurrentValue { get; set; }
        public int Value { get; set; }
        public Color Color { get; set; }

        public ValueBar(Rectangle bounds, GameContext context) : base(bounds, context)
        {

        }

        public void SetValue(int currentValue, int value)
        {
            CurrentValue = currentValue;
            Value = value;
        }

        /*
        public void Update(GameTime gameTime)
        {
            // You can add any logic here to update the value bar if needed
        }
        */

        public override void Draw()
        {
            // Draw the background of the bar
            Context.SpriteBatch.Draw(Context.Pixel, Bounds, Color.Gray);
            // Calculate the width of the filled portion of the bar
            float fillPercentage = (float)CurrentValue / Value;
            int fillWidth = (int)(Bounds.Width * fillPercentage);
            // Draw the filled portion of the bar
            Context.SpriteBatch.Draw(Context.Pixel, new Rectangle(Bounds.Left, Bounds.Top, fillWidth, Bounds.Height), Color);
            var text = $"{CurrentValue}/{Value}";
            Context.SpriteBatch.DrawString(Context.Font, text, new Vector2(Bounds.Center.X - TextHelper.GetTextWidth(text, Context.Font) / 2, Bounds.Center.Y - Context.Font.LineSpacing / 2), Color.White);
        }
    }
}
