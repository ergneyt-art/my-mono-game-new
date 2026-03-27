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

namespace MyMonoGame
{
    public class CharacterMenuScreen : BaseMenu
    {
        private Dictionary<string, CharacterSlotUI> _charSlots;
        private Dictionary<string, Button> _otherButtons;

        private int buttonWidth = 100;
        private int buttonHeight = 50;
        private int startX = 5;
        private int startY = 5;
        private int spacing = 10;

        public CharacterMenuScreen(string title, int windowHeight, int windowWeight, SpriteFont font, Texture2D pixel) : base(title, windowHeight, windowWeight, font, pixel)
        {
            _otherButtons = new Dictionary<string, Button>();
            _charSlots = new Dictionary<string, CharacterSlotUI>();

            _otherButtons.Add("Back", new Button(new Rectangle(startX, startY, buttonWidth, buttonHeight), ScreenAction.GoToMainMenu, "Back", _font));
            startY = windowHeight - spacing;
            _otherButtons.Add("StartGame", new Button(new Rectangle(startX, startY, buttonWidth, buttonHeight), ScreenAction.StartGame, "StartGame", _font));
           
            for (int i = 1; i <= 4; i++)
            {
                startX = startX + spacing + buttonWidth;
                var chairSlot = new CharacterSlotUI()
                {
                    CreateButton = new Button(new Rectangle(startX, startY - buttonHeight, buttonWidth, buttonHeight), ScreenAction.AddCharacter, "Add", _font),
                    ChangeButton = new Button(new Rectangle(startX, startY - buttonHeight, buttonWidth, buttonHeight), ScreenAction.EditCharacter, "Edit", _font),
                    DeleteButton = new Button(new Rectangle(startX, startY - (buttonHeight * 2 + spacing), buttonWidth, buttonHeight), ScreenAction.DeleteCharacter, "Delete", _font),
                };
                _charSlots.Add($"Char{i}", chairSlot);
            }

            ManageButtons();
        }

        private void ManageButtons()
        {
            if (_charSlots is not null && _charSlots.Any(x => x.Value.Character is not null))
            {
                _otherButtons["StartGame"].SetEnabled(true);
                foreach (var slot in _charSlots)
                {
                    if (slot.Value.Character is null)
                    {
                        slot.Value.CreateButton.ShowButton();
                        slot.Value.ChangeButton.HideButton();
                        slot.Value.DeleteButton.HideButton();
                    }
                    else
                    {
                        slot.Value.CreateButton.HideButton();
                        slot.Value.ChangeButton.ShowButton();
                        slot.Value.DeleteButton.ShowButton();
                    }
                }
            }
            else
            {
                _otherButtons["StartGame"].SetEnabled(false);
            }
        }

        public override ScreenAction Update()
        {
            ManageButtons();
            foreach (var button in _otherButtons)
            {
                if (button.Value.Update() != ScreenAction.None) 
                { 
                    return button.Value.Action;
                }
            }

            foreach (var slot in _charSlots)
            {
                if (slot.Value.CreateButton.Update() != ScreenAction.None) { return slot.Value.CreateButton.Action; }
                else if (slot.Value.ChangeButton.Update() != ScreenAction.None) { return slot.Value.ChangeButton.Action; }
                else if (slot.Value.DeleteButton.Update() != ScreenAction.None) { return slot.Value.DeleteButton.Action; }
            }
            return ScreenAction.None;
        }

        public override void Draw(SpriteBatch spriteBatch) 
        {
            SetTitle(spriteBatch);
            foreach (var button in _otherButtons) 
            {
                button.Value.Draw(spriteBatch, _font, _pixel);
            }
            foreach(var slot in _charSlots)
            {
                slot.Value.CreateButton.Draw(spriteBatch, _font, _pixel);
                slot.Value.ChangeButton.Draw(spriteBatch, _font, _pixel);
                slot.Value.DeleteButton.Draw(spriteBatch, _font, _pixel);
            }
        }
    }
}
