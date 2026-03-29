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
        public Rectangle ContentContainer;
        public Rectangle FooterContainer;
        public Rectangle LeftPanel;
        public Rectangle RightPanel;

        public double HeaderContainerWidth;
        public double HeaderContainerHeight = 0.1;

        public double CenterWidth;
        public double CenterHeight;

        public double ContentContainerWidth = 0.75;
        public double ContainedContentHeight = 0.8 ;

        public double FootContainerHeight = 0.10;

        public double LeftPanelWidth = 0.15;

        public double RightPanelWidth = 0.10;

        public MenuLayout(Viewport viewport)
        {
            CenterWidth = Screen.Width / 2;
            CenterHeight = Screen.Height / 2;
            Screen = new Rectangle(viewport.X, viewport.Y, viewport.Width, viewport.Height);
            HeaderContainer = new Rectangle(0, 0, Screen.Width, (int)(Screen.Height * HeaderContainerHeight));
            LeftPanel = new Rectangle(0, HeaderContainer.Height, (int)(Screen.Width * LeftPanelWidth), (int)(Screen.Height - FootContainerHeight));
            ContentContainer = new Rectangle(RightPanel.Width, HeaderContainer.Height, (int)(Screen.Width * ContentContainerWidth), (int)(Screen.Height * ContainedContentHeight));
            RightPanel = new Rectangle(Screen.Width - (int)(Screen.Width * RightPanelWidth), HeaderContainer.Height, (int)(Screen.Width * RightPanelWidth), (int)(Screen.Height * RightPanelWidth));
            FooterContainer = new Rectangle(0, Screen.Height - (int)(Screen.Height * FootContainerHeight), Screen.Width, (int)(Screen.Height * FootContainerHeight));

        }
    }
}
