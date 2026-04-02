using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            _spacing = 10;
            this.AddButtonToCenterPanel("Start Game", ScreenAction.GoToCharacterMenu, AddButtonMode.Bottom);
            this.AddButtonToCenterPanel("Load Game", ScreenAction.GoToLoadGameMenu, AddButtonMode.Bottom);
            this.AddButtonToCenterPanel("Settings", ScreenAction.GoToSettingsMenu, AddButtonMode.Bottom);
            this.AddButtonToCenterPanel("About Game", ScreenAction.GoToAboutGameMenu, AddButtonMode.Bottom);
            this.AddButtonToCenterPanel("Exit", ScreenAction.ExitGame, AddButtonMode.Bottom);

            this.AddButtonToCenterPanel("Test", ScreenAction.Test, AddButtonMode.Bottom);
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
            if (_infoDialog != null) 
            {
                _infoDialog.Draw(spriteBatch, _font, _pixel);
            }
            else
            {
                SetTitle(spriteBatch);
                foreach (var button in _buttons)
                {
                    button.Draw(spriteBatch, _font, _pixel);
                }
            }
        }
    }
}
