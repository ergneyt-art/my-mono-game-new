using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.MenuClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyMonoGame
{
    public class CharacterSlotUI
    {
        public Character? Character;
        public Rectangle Slot;
        public Button<PartyMenuActions> CreateButton;
        public Button<PartyMenuActions> ChangeButton;
        public Button<PartyMenuActions> DeleteButton;
        private const int _defaultButtonWidth = 80;
        private const int _defaultButtonHeight = 40;
        private const int _defaultButtonSpacing = 5;

        public CharacterSlotUI(Rectangle frame, SpriteFont font, int buttonWidth = _defaultButtonWidth, int buttonHeight = _defaultButtonHeight, int buttonSpacing = _defaultButtonSpacing)
        {
            Slot = frame;
            DeleteButton = new Button<PartyMenuActions>(new Rectangle(Slot.X + buttonSpacing, Slot.Bottom - (buttonSpacing + buttonHeight), buttonWidth, buttonHeight), PartyMenuActions.DeleteCharacter, "Delete", font);
            CreateButton = new Button<PartyMenuActions>(new Rectangle(Slot.X + buttonSpacing, Slot.Bottom - (buttonSpacing + buttonHeight) * 2, buttonWidth, buttonHeight), PartyMenuActions.AddCharacter, "Add", font);
            ChangeButton = new Button<PartyMenuActions>(new Rectangle(Slot.X + buttonSpacing, Slot.Bottom - (buttonSpacing + buttonHeight) * 2, buttonWidth, buttonHeight), PartyMenuActions.EditCharacter, "Edit", font);
            Character = null;
        }
    }
}
