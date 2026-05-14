using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGame.GameObjects;
using MyMonoGame.Helpers;
using MyMonoGame.InterfaceElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.MenuClasses
{
    public class MainMenuScreen : BaseMenu<ScreenAction>
    {
        public MainMenuScreen(string title, Rectangle frame, GameContext context) : base(title, frame, context)
        {
            AddButtonToCenterPanel("Start Game", ScreenAction.GoToPartyMenu);
            AddButtonToCenterPanel("Load Game", ScreenAction.GoToLoadGameMenu);
            AddButtonToCenterPanel("About Game", ScreenAction.GoToAboutGameMenu);
            AddButtonToCenterPanel("Settings", ScreenAction.GoToSettingsMenu);
            AddButtonToCenterPanel("Exit", ScreenAction.ExitGame);
            AddButtonToCenterPanel("Test Dialog", ScreenAction.Test);

            /*
            _centerPanelCursor.SetPosition(_menuLayout.ContentContainer.Center.X - _defaultButtonWidth / 2, _menuLayout.ContentContainer.Top + _defaultSpacing);
            _centerPanelButtons.Add(AddButton("Start Game", ScreenAction.GoToPartyMenu, _centerPanelCursor));
            _centerPanelButtons.Add(AddButton("Load Game", ScreenAction.GoToLoadGameMenu, _centerPanelCursor));
            _centerPanelButtons.Add(AddButton("About Game", ScreenAction.GoToAboutGameMenu, _centerPanelCursor));
            _centerPanelButtons.Add(AddButton("Settings", ScreenAction.GoToSettingsMenu, _centerPanelCursor));
            _centerPanelButtons.Add(AddButton("Exit", ScreenAction.ExitGame, _centerPanelCursor));
            _centerPanelButtons.Add(AddButton("Test Dialog", ScreenAction.Test, _centerPanelCursor));
            */
        }

        public override ScreenAction Update()
        {
            ButtonsEnabledManage();
            if (_infoDialog != null) 
            {
                var dialogResult = _infoDialog.Update();
                if (dialogResult != InfoDialogResult.None) 
                {
                    _infoDialog.Close();
                    _infoDialog = null;
                    return ScreenAction.None;
                }
            }

            foreach (var button in _buttons) 
            {
                button.Update();
                if (button.GetClickedStatus())
                {
                    if (button.Action == ScreenAction.Test)
                    {
                        _infoDialog = new InfoDialog(_menuLayout.ContentContainer, "Test", Context, "This is a test dialog. Random text here and here and here too. There is a enormous bunch of useless text. Yes!");
                        TurnOffAllButtons();
                        _infoDialog.Open();
                        return ScreenAction.None;
                    }
                    else
                    {
                        return button.Action;
                    }
                }
            }
            return ScreenAction.None;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
