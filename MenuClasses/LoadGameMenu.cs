using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.Helpers;
using MyMonoGame.InterfaceElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.MenuClasses
{
    
    public class LoadGameMenu : BaseMenu<ScreenAction>
    {
        protected ActiveGrid Grids;
        public LoadGameMenu(string title, Rectangle frame, GameContext context) : base(title, frame, context)
        {
            _leftPanelCursor.SetPosition(_menuLayout.LeftPanel.Center.X - _defaultButtonWidth / 2, _menuLayout.LeftPanel.Top + _defaultSpacing);
            _leftPanelButtons.Add(AddButton("Back", ScreenAction.GoToMainMenu, _leftPanelCursor));
            Grids = new ActiveGrid(_menuLayout.ContentContainer, Context);
            // _leftPanelButtons[0].TooltipText = "test tip";
        }

        public override ScreenAction Update()
        {
            ButtonsEnabledManage();
            Grids.Update();

            foreach (var button in _buttons)
            {
                button.Update();
                if (button.GetClickedStatus()) return button.Action;
            }
            return ScreenAction.None;
        }

        public override void Draw()
        {
            base.Draw();
            if (Grids != null)
            {
                Grids.Draw();
            }
        }
    }
}
