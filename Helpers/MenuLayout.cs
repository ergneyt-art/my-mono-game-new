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
    public class MenuLayout
    {
        public Rectangle Screen;
        public Rectangle HeaderContainer;
        public Rectangle FooterContainer;
        public Rectangle Body;
        public Rectangle ContentContainer;
        public Rectangle LeftPanel;
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

        public MenuLayoutConfig Config;

        public int LeftPanelCurrentX = 0;
        public int LeftPanelCurrentY = 0;

        public int RightPanelCurrentX = 0;
        public int RightPanelCurrentY = 0;

        public int ContentContainerCurrentX = 0;
        public int ContentContainerCurrentY = 0;

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

            LeftPanelCurrentY = LeftPanel.Top;
            RightPanelCurrentY = RightPanel.Top;
            ContentContainerCurrentX = ContentContainer.Left;
            ContentContainerCurrentY = ContentContainer.Top;
        }
    }

    public class MenuLayoutConfig
    {
        public double ProcentFrame;
        public double HeaderContainerHeight;
        public double ContentContainerWidth;
        public double FootContainerHeight;
        public double LeftPanelWidth;
        public double RightPanelWidth;
        public bool AddDefaultButton;
    }

}
