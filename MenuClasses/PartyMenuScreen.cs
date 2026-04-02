using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MyMonoGame.MenuClasses
{
    public class PartyMenuScreen : BaseMenu<ScreenAction>
    {
        private List<CharacterSlotUI> _charSlots;

        public PartyMenuScreen(string title, Rectangle frame, SpriteFont font, Texture2D pixel) : 
            base(title, frame, font, pixel)
        {
            _charSlots = new List<CharacterSlotUI>();
            AddButtonToLeftPanel("Back", ScreenAction.GoToMainMenu);
            AddButtonToRightPanel("Start Game", ScreenAction.StartGame);
            var slotSize = this._menuLayout.ContentContainer.Width / 4;

            for (int i = 0; i < 4; i++)
            {
                var characterFrame = new Rectangle(this._menuLayout.ContentContainer.Left + (slotSize * i), _menuLayout.ContentContainer.Top, slotSize, _menuLayout.ContentContainer.Height);
                _charSlots.Add(new CharacterSlotUI(characterFrame, _font));
            }

            ButtonsEnabledManage();
        }

        private void ManageButtons()
        {
            if (_charSlots.Any(x => x.Character is not null))
            {
                _buttons.FirstOrDefault(x => x.Action == ScreenAction.StartGame).SetEnabled(true);
            }
            else
            {
                _buttons.FirstOrDefault(x => x.Action == ScreenAction.StartGame).SetEnabled(false);
            }

            foreach (var slot in _charSlots)
            {
                if (slot.Character is null)
                {
                    slot.CreateButton.ShowButton();
                    slot.ChangeButton.HideButton();
                    slot.DeleteButton.HideButton();
                }
                else
                {
                    slot.CreateButton.HideButton();
                    slot.ChangeButton.ShowButton();
                    slot.DeleteButton.ShowButton();
                }
            }
        }

        public override ScreenAction Update()
        {
            ButtonsEnabledManage();
            foreach (var button in _buttons)
            {
                button.Update();
                if (button.IsClicked) 
                { 
                    return button.Action;
                }
            }

            foreach (var slot in _charSlots)
            {
                slot.CreateButton.Update();
                slot.ChangeButton.Update();
                slot.DeleteButton.Update();
            }
            return ScreenAction.None;
        }

        public override void Draw(SpriteBatch spriteBatch) 
        {
            SetTitle(spriteBatch);
            foreach (var button in _buttons) 
            {
                button.Draw(spriteBatch, _font, _pixel);
            }
            foreach(var slot in _charSlots)
            {
                slot.CreateButton.Draw(spriteBatch, _font, _pixel);
                slot.ChangeButton.Draw(spriteBatch, _font, _pixel);
                slot.DeleteButton.Draw(spriteBatch, _font, _pixel);
            }
        }

        protected override void ButtonsEnabledManage() 
        {
            base.ButtonsEnabledManage();
            ManageButtons();
        }
    }
}
