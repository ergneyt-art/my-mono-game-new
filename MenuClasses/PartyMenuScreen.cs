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
    public class PartyMenuScreen : BaseMenu
    {
        private List<CharacterSlotUI> _charSlots;

        private int buttonWidth = 100;
        private int buttonHeight = 50;
        private int startX;
        private int startY;

        public PartyMenuScreen(string title, Viewport viewport, SpriteFont font, Texture2D pixel) : 
            base(title, viewport, font, pixel)
        {
            _spacing = 10;
            _charSlots = new List<CharacterSlotUI>();

            _buttons.Add(new Button(new Rectangle(_spacing, _spacing, buttonWidth, buttonHeight), ScreenAction.GoToMainMenu, "Back", _font));

            _buttons.Add(new Button(new Rectangle(_menuLayout.Screen.Width - (_spacing + buttonWidth), _spacing, buttonWidth, buttonHeight), ScreenAction.StartGame, "StartGame", _font));
            startX = _spacing;
            for (int i = 1; i <= 4; i++)
            {
                
                var chairSlot = new CharacterSlotUI()
                {
                    CreateButton = new Button(new Rectangle(startX, _menuLayout.Screen.Height - buttonHeight - _spacing, buttonWidth, buttonHeight), ScreenAction.AddCharacter, "Add", _font),
                    ChangeButton = new Button(new Rectangle(startX, _menuLayout.Screen.Height - buttonHeight - _spacing, buttonWidth, buttonHeight), ScreenAction.EditCharacter, "Edit", _font),
                    DeleteButton = new Button(new Rectangle(startX, _menuLayout.Screen.Height - (buttonHeight * 2 + _spacing), buttonWidth, buttonHeight), ScreenAction.DeleteCharacter, "Delete", _font),
                };
                _charSlots.Add(chairSlot);
                startX = startX + _spacing + buttonWidth;
            }

            ManageButtons();
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
            ManageButtons();
            foreach (var button in _buttons)
            {
                if (button.Update() != ScreenAction.None) 
                { 
                    return button.Action;
                }
            }

            foreach (var slot in _charSlots)
            {
                if (slot.CreateButton.Update() != ScreenAction.None) { return slot.CreateButton.Action; }
                else if (slot.ChangeButton.Update() != ScreenAction.None) { return slot.ChangeButton.Action; }
                else if (slot.DeleteButton.Update() != ScreenAction.None) { return slot.DeleteButton.Action; }
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
    }
}
