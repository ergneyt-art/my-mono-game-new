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

        public int LeftPanelCurrentX = 0;
        public int LeftPanelCurrentY = 0;

        public int RightPanelCurrentX = 0;
        public int RightPanelCurrentY = 0;

        public int ContentContainerCurrentX = 0;
        public int ContentContainerCurrentY = 0;


        public double HeaderContainerHeight = 0.1;

        public double ContentContainerWidth = 0.75;

        public double FootContainerHeight = 0.10;

        public double LeftPanelWidth = 0.15;

        public double RightPanelWidth = 0.10;

        public MenuLayout(Viewport viewport)
        {
            Screen = new Rectangle(viewport.X, viewport.Y, viewport.Width, viewport.Height);
            HeaderContainer = new Rectangle(0, 0, Screen.Width, (int)(Screen.Height * HeaderContainerHeight));
            FooterContainer = new Rectangle(0, Screen.Height - (int)(Screen.Height * FootContainerHeight), Screen.Width, (int)(Screen.Height * FootContainerHeight));
            Body = new Rectangle(0, Screen.Height - HeaderContainer.Height, Screen.Width, Screen.Height - HeaderContainer.Height - FooterContainer.Height);
            LeftPanel = new Rectangle(0, HeaderContainer.Height, (int)(Body.Width * LeftPanelWidth), Body.Height);
            ContentContainer = new Rectangle(LeftPanel.Width, HeaderContainer.Height, (int)(Body.Width * ContentContainerWidth), Body.Height);
            RightPanel = new Rectangle(LeftPanel.Width + ContentContainer.Width, HeaderContainer.Height, (int)(Body.Width * RightPanelWidth), Body.Height);
            LeftPanelCurrentY = LeftPanel.Top;
            RightPanelCurrentY = RightPanel.Top;
            ContentContainerCurrentX = ContentContainer.Left;
        }
    }
}
