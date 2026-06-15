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
    /// <summary>
    /// Base class for visible interface elements with bounds and shared context.
    /// </summary>
    public abstract class BaseElement
    {
        /// <summary>
        /// Area occupied by this element.
        /// </summary>
        public Rectangle Bounds { get; set; }
        protected bool IsVisible = true;
        protected GameContext Context;

        protected BaseElement(Rectangle bounds, GameContext context) 
        { 
            this.Bounds = bounds;
            this.Context = context;
        }

        /// <summary>
        /// Hides the element from rendering.
        /// </summary>
        public virtual void Hide()
        {
            IsVisible = false;
        }

        /// <summary>
        /// Shows the element for rendering.
        /// </summary>
        public virtual void Show() 
        {
            IsVisible = true;
        }

        /// <summary>
        /// Draws the element using resources from GameContext.
        /// </summary>
        public abstract void Draw();
    }

    /// <summary>
    /// Base class for interface elements that can react to mouse interaction.
    /// </summary>
    public abstract class BaseActiveElement : BaseElement
    {
        protected bool IsEnabled = true;
        protected bool IsHovered = false;

        protected BaseActiveElement(Rectangle bounds, GameContext context) : base(bounds, context) 
        { 

        }

        /// <summary>
        /// Updates IsHovered based on the current mouse position.
        /// </summary>
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

        public override void Draw()
        {
            // Base drawing logic can be implemented here, or in derived classes
        }

        /// <summary>
        /// Allows this element to react to user input.
        /// </summary>
        public void AllowInteraction()
        {
            IsEnabled = true;
        }

        /// <summary>
        /// Prevents this element from reacting to user input.
        /// </summary>
        public void DisallowInteraction()
        {
            IsEnabled = false;
        }

        /// <summary>
        /// Toggles whether this element can react to user input.
        /// </summary>
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

    /// <summary>
    /// Base class for active elements that can display a tooltip while hovered.
    /// </summary>
    public abstract class BaseElementWithTooltip : BaseActiveElement
    {
        /// <summary>
        /// Text shown in the tooltip when the element is hovered.
        /// </summary>
        public string? TooltipText;
        protected ToolTip Tooltip;
        protected BaseElementWithTooltip(Rectangle bounds, GameContext context, string? tooltipText = null) : base(bounds, context)
        {
            this.TooltipText = tooltipText;
            this.Tooltip = new ToolTip(tooltipText ?? string.Empty, Context);
        }

        public override void Draw()
        {
            base.Draw();
            if (IsHovered && !string.IsNullOrEmpty(TooltipText))
            {
                this.Tooltip.Draw();
            }
        }

        /// <summary>
        /// Updates hover and tooltip state.
        /// </summary>
        public virtual void Update()
        {
            this.UpdateHoveredState();
        }

        protected override void UpdateHoveredState()
        {
            if (this.IsVisible && this.IsEnabled)
            {
                var mouse = Mouse.GetState();
                this.IsHovered = Bounds.Contains(mouse.Position);
                UpdateTooltip();
            }
            else
            {
                this.IsHovered = false;
            }
        }

        protected void UpdateTooltip()
        {
            if (!string.IsNullOrEmpty(this.TooltipText))
            {
                if (Tooltip == null)
                {
                    Tooltip = new ToolTip(this.TooltipText, Context);
                }

                if (IsHovered)
                {
                    TryToFindPlaceForToolTip();
                    Tooltip.Show();
                }
                else
                {
                    Tooltip.Hide();
                }    
            }
        }

        protected void TryToFindPlaceForToolTip()
        {
            var mouse = Mouse.GetState();
            var tooltipSize = Context.Font.MeasureString(this.TooltipText);
            var screenBounds = new Rectangle(0, 0, Context.ScreenWidth, Context.ScreenHeight); // TODO: Get actual screen size
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
