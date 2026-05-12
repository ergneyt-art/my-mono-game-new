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
    public abstract class BaseElement : IInterfaceElement
    {
        public Rectangle Bounds { get; set; }
        protected SpriteFont Font;
        protected bool IsVisible = true;

        protected BaseElement(Rectangle bounds, SpriteFont font) 
        { 
            this.Bounds = bounds;
            this.Font = font;
        }

        public virtual void Hide()
        {
            IsVisible = false;
        }

        public virtual void Show() 
        {
            IsVisible = true;
        }

        public abstract void Draw(SpriteBatch spriteBatch, Texture2D pixel);
    }

    public abstract class BaseActiveElement : BaseElement
    {
        protected bool IsEnabled = true;
        protected bool IsHovered = false;

        protected BaseActiveElement(Rectangle bounds, SpriteFont font) : base(bounds, font) 
        { 

        }

        protected virtual void UpdateHoveredState()
        {
            if (IsVisible && IsEnabled)
            {
                var mouse = Mouse.GetState();
                IsHovered = Bounds.Contains(mouse.Position);
            }
            else
            {
                IsHovered = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D pixel = null)
        {
            // Base drawing logic can be implemented here, or in derived classes
        }

        public void AllowInteraction()
        {
            IsEnabled = true;
        }

        public void DisallowInteraction()
        {
            IsEnabled = false;
        }

        public void ToggleInteraction()
        {
            IsEnabled = !IsEnabled;
        }

        public override void Hide()
        {
            base.Hide();
            IsEnabled = false;
        }

        public override void Show() 
        {
            base.Show();
            IsEnabled = true;
        }
    }

    public abstract class BaseElementWithTooltip : BaseActiveElement
    {
        public string? TooltipText;
        protected ToolTip Tooltip;
        protected BaseElementWithTooltip(Rectangle bounds, SpriteFont font, string? tooltipText = null) : base(bounds, font)
        {
            this.TooltipText = tooltipText;
            this.Tooltip = new ToolTip(tooltipText ?? string.Empty, font);
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D pixel = null)
        {
            base.Draw(spriteBatch, pixel);
            if (IsHovered && !string.IsNullOrEmpty(TooltipText))
            {
                this.Tooltip.Draw(spriteBatch, pixel);
            }
        }

        protected override void UpdateHoveredState()
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
            if (this.IsVisible && !string.IsNullOrEmpty(this.TooltipText))
            {
                this.Tooltip = new ToolTip(this.TooltipText, Font);
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
            var tooltipSize = Font.MeasureString(this.TooltipText);
            var screenBounds = new Rectangle(0, 0, 1280, 800); // TODO: Get actual screen size
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
    }
}
