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

        public ValueBar(Rectangle bounds, SpriteFont font) : base(bounds, font)
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

        public override void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {
            // Draw the background of the bar
            spriteBatch.Draw(pixel, Bounds, Color.Gray);
            // Calculate the width of the filled portion of the bar
            float fillPercentage = (float)CurrentValue / Value;
            int fillWidth = (int)(Bounds.Width * fillPercentage);
            // Draw the filled portion of the bar
            spriteBatch.Draw(pixel, new Rectangle(Bounds.Left, Bounds.Top, fillWidth, Bounds.Height), Color);
            var text = $"{CurrentValue}/{Value}";
            spriteBatch.DrawString(Font, text, new Vector2(Bounds.Center.X - TextHelper.GetTextWidth(text, Font) / 2, Bounds.Center.Y - Font.LineSpacing / 2), Color.White);
        }
    }
}
