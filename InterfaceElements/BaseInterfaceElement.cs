using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyMonoGame.InterfaceElements
{
    public abstract class BaseInterfaceElement
    {
        public string? TooltipText;
        protected ToolTip Tooltip;
        protected SpriteFont _font;
        protected bool IsVisible = true;
        protected bool IsEnabled = true;
        protected bool IsHovered = false;
        public Rectangle Bounds { get; set; }

        public BaseInterfaceElement(Rectangle bounds, SpriteFont font, string? tooltipText = null)
        {
            this.Bounds = bounds;
            this._font = font;
            this.TooltipText = tooltipText;
            this.Tooltip = new ToolTip(tooltipText ?? string.Empty);
        }

        protected Vector2 RecalculateTextPosition(string text, Rectangle frame)
        {
            Vector2 size = _font.MeasureString(text);
            float x_axis = frame.X + frame.Width / 2 - size.X / 2;
            float y_axis = frame.Y + frame.Height / 2 - size.Y / 2;
            return new Vector2(x_axis, y_axis);
        }

        protected Vector2 RecalculateTextPosition(string text)
        {
            Vector2 size = _font.MeasureString(text);
            float x_axis = this.Bounds.X + Bounds.Width / 2 - size.X / 2;
            float y_axis = this.Bounds.Y + Bounds.Height / 2 - size.Y / 2;
            return new Vector2(x_axis, y_axis);
        }

        public void HideElement()
        {
            this.IsVisible = false;
            this.IsEnabled = false;
        }

        protected void UpdateHoveredState()
        {
            if (this.IsVisible && this.IsEnabled)
            {
                var mouse = Mouse.GetState();
                this.IsHovered = Bounds.Contains(mouse.Position);
                if (this.IsHovered && !string.IsNullOrEmpty(this.TooltipText))
                {
                    UpdateTooltip();
                }
            }
            else
            {
                this.IsHovered = false;
            }
        }

        protected void UpdateTooltip()
        {
            if (this.IsHovered && !string.IsNullOrEmpty(this.TooltipText))
            {
                this.Tooltip = new ToolTip(this.TooltipText);
                TryToFindPlaceForToolTip();
                Tooltip.Show();
            }
            else
            {
                Tooltip.Hide();
            }
        }

        protected void TryToFindPlaceForToolTip()
        {
            var mouse = Mouse.GetState();
            var tooltipSize = _font.MeasureString(this.TooltipText);
            var screenBounds = new Rectangle(0, 0, 1280, 800);
            var tooltipBounds = new Rectangle(mouse.X, mouse.Y, (int)tooltipSize.X + 10, (int)tooltipSize.Y + 10);
            if (!screenBounds.Contains(tooltipBounds))
            {
                if (mouse.X + tooltipBounds.Width > screenBounds.Right)
                {
                    tooltipBounds.X = screenBounds.Right - tooltipBounds.Width;
                }
                if (mouse.Y + tooltipBounds.Height > screenBounds.Bottom)
                {
                    tooltipBounds.Y = screenBounds.Bottom - tooltipBounds.Height;
                }
            }
            Tooltip.Bounds = tooltipBounds;
        }

        protected void ToggleVisibility()
        {
            this.IsVisible = !this.IsVisible;
            this.IsEnabled = this.IsVisible;
        }

        public void ShowElement()
        {
            this.IsVisible = true;
            this.IsEnabled = true;
        }

        public void SetVisible(bool visible)
        {
            this.IsVisible = visible;
        }

        public void SetEnabled(bool enabled)
        {
            this.IsEnabled = enabled;
        }
    }
}
