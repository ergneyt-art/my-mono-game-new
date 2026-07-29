using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.InterfaceElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.Helpers
{
    /// <summary>
    /// Splits a screen or window rectangle into common menu regions.
    /// </summary>
    public class MenuLayout
    {
        /// <summary>
        /// Root rectangle used by this layout after applying ProcentFrame.
        /// </summary>
        public Rectangle Screen;

        /// <summary>
        /// Top region usually used for a title.
        /// </summary>
        public Rectangle HeaderContainer;

        /// <summary>
        /// Bottom region usually used for action buttons.
        /// </summary>
        public Rectangle FooterContainer;

        /// <summary>
        /// Middle region between header and footer.
        /// </summary>
        public Rectangle Body;

        /// <summary>
        /// Main region between left and right panels.
        /// </summary>
        public Rectangle ContentContainer;

        /// <summary>
        /// Left side panel inside the body.
        /// </summary>
        public Rectangle LeftPanel;

        /// <summary>
        /// Right side panel inside the body.
        /// </summary>
        public Rectangle RightPanel;

        private readonly MenuLayoutConfig _defaultConfig = new MenuLayoutConfig
        {
            ProcentFrame = 1,
            HeaderContainerHeight = 0.1,
            FootContainerHeight = 0.1,
            LeftPanelWidth = 0.15,
            RightPanelWidth = 0.15,
            ContentContainerWidth = 0.7
        };

        /// <summary>
        /// Configuration used to calculate this layout.
        /// </summary>
        public MenuLayoutConfig Config;

        /// <summary>
        /// Creates a layout for the given frame.
        /// </summary>
        public MenuLayout(Rectangle frame, MenuLayoutConfig config = default)
        {
            if (config == default) 
            {
                Config = _defaultConfig;
            }
            else
            {
                Config = config;
            }
            ApplyConfig(frame);
        }

        private void ApplyConfig(Rectangle frame)
        {
            var width = (int)(frame.Width * Config.ProcentFrame);
            var height = (int)(frame.Height * Config.ProcentFrame);

            Screen = new Rectangle(
                (int)(frame.Center.X - (width / 2)),
                (int)(frame.Center.Y - (height / 2)),
                width,
                height
            );

            var headerContainerHeight = (int)(Screen.Height * Config.HeaderContainerHeight);
            var footerContainerHeight = (int)(Screen.Height * Config.FootContainerHeight);

            HeaderContainer = new Rectangle(Screen.Left, Screen.Top, Screen.Width, headerContainerHeight);
            FooterContainer = new Rectangle(Screen.Left, Screen.Bottom - footerContainerHeight, Screen.Width, footerContainerHeight);

            var bodyHeight = Screen.Height - headerContainerHeight - footerContainerHeight;

            Body = new Rectangle(Screen.Left, HeaderContainer.Bottom, Screen.Width, bodyHeight);

            var leftPanelWidth = (int)(Body.Width * Config.LeftPanelWidth);
            var rightPanelWidth = (int)(Body.Width * Config.RightPanelWidth);

            LeftPanel = new Rectangle(Body.Left, Body.Top, leftPanelWidth, Body.Height);
            RightPanel = new Rectangle(Body.Right - rightPanelWidth, Body.Top, rightPanelWidth, Body.Height);

            ContentContainer = new Rectangle(LeftPanel.Right, Body.Top, RightPanel.Left - LeftPanel.Right, Body.Height);
        }

        public PanelCursor GetCursor(MenuLayoutArea area)
        {
            return area switch
            {
                MenuLayoutArea.Screen => new PanelCursor(Screen),
                MenuLayoutArea.Header => new PanelCursor(HeaderContainer),
                MenuLayoutArea.Body => new PanelCursor(Body),
                MenuLayoutArea.Content => new PanelCursor(ContentContainer),
                MenuLayoutArea.Footer => new PanelCursor(FooterContainer),
                MenuLayoutArea.LeftPanel => new PanelCursor(LeftPanel),
                MenuLayoutArea.RightPanel => new PanelCursor(RightPanel),
                _ => throw new ArgumentException("Invalid MenuLayoutArea")
            };
        }
    }

    /// <summary>
    /// Percentage-based configuration for MenuLayout regions.
    /// </summary>
    public class MenuLayoutConfig
    {
        /// <summary>
        /// Portion of the provided frame used by the layout.
        /// </summary>
        public double ProcentFrame;

        /// <summary>
        /// Header height as a fraction of the screen height.
        /// </summary>
        public double HeaderContainerHeight;

        /// <summary>
        /// Content width as a fraction of the body width.
        /// </summary>
        public double ContentContainerWidth;

        /// <summary>
        /// Footer height as a fraction of the screen height.
        /// </summary>
        public double FootContainerHeight;

        /// <summary>
        /// Left panel width as a fraction of the body width.
        /// </summary>
        public double LeftPanelWidth;

        /// <summary>
        /// Right panel width as a fraction of the body width.
        /// </summary>
        public double RightPanelWidth;

        /// <summary>
        /// Indicates whether a dialog using this config should create default buttons.
        /// </summary>
        public bool AddDefaultButton;
    }

}
