using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGame.GameObjects;
using MyMonoGame.Helpers;
using MyMonoGame.InterfaceElements;
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
        public Character CurrentChar { get; private set; }

        public PartyMenuScreen(string title, Rectangle frame, SpriteFont font, Texture2D pixel) : 
            base(title, frame, font, pixel)
        {
            _charSlots = new List<CharacterSlotUI>();
            _leftPanelCursor.SetPosition(_menuLayout.LeftPanel.Center.X - _defaultButtonWidth / 2, _menuLayout.LeftPanel.Top + _defaultSpacing);
            _leftPanelButtons.Add(AddButton("Back", ScreenAction.GoToMainMenu, _leftPanelCursor));
            _rightPanelCursor.SetPosition(_menuLayout.RightPanel.Center.X - _defaultButtonWidth / 2, _menuLayout.RightPanel.Top + _defaultSpacing);
            _rightPanelButtons.Add(AddButton("Start Game", ScreenAction.StartGame, _rightPanelCursor));
            var slotSize = this._menuLayout.ContentContainer.Width / 4;

            for (int i = 0; i < 4; i++)
            {
                Rectangle characterFrame = default;
                if (i == 0)
                {
                    characterFrame = new Rectangle(this._menuLayout.ContentContainer.Left, _menuLayout.ContentContainer.Top, slotSize, _menuLayout.ContentContainer.Height);
                }
                else
                {
                    var previousSlot = _charSlots[i - 1];
                    characterFrame = new Rectangle(previousSlot.Slot.Right, _menuLayout.ContentContainer.Top, slotSize, _menuLayout.ContentContainer.Height);

                }
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

                if (slot.CreateButton.IsClicked) 
                {
                    CurrentChar = new Character();
                    return ScreenAction.GoToCharacterMenu;
                }
                else if (slot.CreateButton.IsClicked)                {
                    CurrentChar = slot.Character;
                    return ScreenAction.GoToCharacterMenu;
                }
                else if (slot.DeleteButton.IsClicked)
                {
                    slot.Character = null;
                }
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
