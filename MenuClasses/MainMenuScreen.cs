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
        public MainMenuScreen(string title, Rectangle frame, SpriteFont font, Texture2D pixel) : 
            base(title, frame, font, pixel)
        {
            _centerPanelCursor.SetPosition(_menuLayout.ContentContainer.Center.X - _defaultButtonWidth / 2, _menuLayout.ContentContainer.Top + _defaultSpacing);
            _centerPanelButtons.Add(AddButton("Start Game", ScreenAction.GoToPartyMenu, _centerPanelCursor));
            _centerPanelButtons.Add(AddButton("Load Game", ScreenAction.GoToLoadGameMenu, _centerPanelCursor));
            _centerPanelButtons.Add(AddButton("About Game", ScreenAction.GoToAboutGameMenu, _centerPanelCursor));
            _centerPanelButtons.Add(AddButton("Settings", ScreenAction.GoToSettingsMenu, _centerPanelCursor));
            _centerPanelButtons.Add(AddButton("Exit", ScreenAction.ExitGame, _centerPanelCursor));
            _centerPanelButtons.Add(AddButton("Test Dialog", ScreenAction.Test, _centerPanelCursor));
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
                if (button.IsClicked && button.Action == ScreenAction.Test) 
                {
                    _infoDialog = new InfoDialog("Test", _font, "This is a test dialog.", _menuLayout.ContentContainer);
                    TurnOffAllButtons();
                    _infoDialog.AddButton(InfoDialogResult.Ok, "OK", _font);
                    _infoDialog.AddButton(InfoDialogResult.Cancel, "Cancel", _font);
                    _infoDialog.Open();
                    return ScreenAction.None;
                }
                if (button.IsClicked) 
                { 
                    return button.Action; 
                }
            }
            return ScreenAction.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SetTitle(spriteBatch);
            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch, _pixel);
            }

            if (_infoDialog != null)
            {
                _infoDialog.Draw(spriteBatch, _font, _pixel);
            }
        }
    }
}
