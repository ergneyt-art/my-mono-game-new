using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.MenuClasses
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
            Screen = new Rectangle(
                (int)(frame.Center.X - ((frame.Width * Config.ProcentFrame) / 2)),
                (int)(frame.Center.Y - ((frame.Height * Config.ProcentFrame) / 2)),
                (int)(frame.Width * Config.ProcentFrame),
                (int)(frame.Height * Config.ProcentFrame)
            );
            HeaderContainer = new Rectangle(Screen.Left, Screen.Top, Screen.Width, (int)(Screen.Height * Config.HeaderContainerHeight));
            FooterContainer = new Rectangle(Screen.Left, Screen.Height - (int)(Screen.Height * Config.FootContainerHeight), Screen.Width, (int)(Screen.Height * Config.FootContainerHeight));
            Body = new Rectangle(Screen.Left, Screen.Height - HeaderContainer.Height, Screen.Width, Screen.Height - HeaderContainer.Height - FooterContainer.Height);
            LeftPanel = new Rectangle(Screen.Left, HeaderContainer.Height, (int)(Body.Width * Config.LeftPanelWidth), Body.Height);
            ContentContainer = new Rectangle(LeftPanel.Width, HeaderContainer.Height, (int)(Body.Width * Config.ContentContainerWidth), Body.Height);
            RightPanel = new Rectangle(LeftPanel.Width + ContentContainer.Width, HeaderContainer.Height, (int)(Body.Width * Config.RightPanelWidth), Body.Height);
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

        /*
        public MenuLayoutConfig(double procentFrame, double headerContainerHeight, double contentContainerWidth, double footContainerHeight, double leftPanelWidth, double rightPanelWidth)
        {
            ProcentFrame = procentFrame;
            HeaderContainerHeight = headerContainerHeight;
            ContentContainerWidth = contentContainerWidth;
            FootContainerHeight = footContainerHeight;
            LeftPanelWidth = leftPanelWidth;
            RightPanelWidth = rightPanelWidth;
        }
        */
    }

}
