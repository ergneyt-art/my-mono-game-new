using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.GameObjects;
using MyMonoGame.Helpers;
using MyMonoGame.MenuClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyMonoGame.InterfaceElements
{
    public class CharacterSlotUI : BaseInterfaceElement
    {
        public Character? Character;
        public Rectangle CharInfoArea;
        public Rectangle CharImageArea;
        public Rectangle ButtonsArea;
        public Button<PartyMenuActions> CreateButton;
        public Button<PartyMenuActions> ChangeButton;
        public Button<PartyMenuActions> DeleteButton;
        private const int _defaultButtonWidth = 80;
        private const int _defaultButtonHeight = 40;
        private const int _defaultButtonSpacing = 5;

        public CharacterSlotUI(Rectangle frame, SpriteFont font, int buttonWidth = _defaultButtonWidth, int buttonHeight = _defaultButtonHeight, int buttonSpacing = _defaultButtonSpacing) : base(frame, font)
        {
            var infoAreaHeight = (int)(frame.Height * 0.3f);
            var imageAreaHeight = (int)(frame.Height * 0.5f);
            var buttonsAreaHeight = frame.Height - infoAreaHeight - imageAreaHeight;

            CharInfoArea = new Rectangle(frame.X, frame.Y, frame.Width, infoAreaHeight);
            CharImageArea = new Rectangle(frame.X, CharInfoArea.Bottom, frame.Width, imageAreaHeight);
            ButtonsArea = new Rectangle(frame.X, CharImageArea.Bottom, frame.Width, buttonsAreaHeight);

            DeleteButton = new Button<PartyMenuActions>(new Rectangle(ButtonsArea.X, ButtonsArea.Y, ButtonsArea.Width, ButtonsArea.Height / 2), PartyMenuActions.DeleteCharacter, "Delete", _font);
            CreateButton = new Button<PartyMenuActions>(new Rectangle(ButtonsArea.X, ButtonsArea.Y, ButtonsArea.Width, ButtonsArea.Height / 2), PartyMenuActions.AddCharacter, "Add", _font);
            ChangeButton = new Button<PartyMenuActions>(new Rectangle(ButtonsArea.X, ButtonsArea.Y + (ButtonsArea.Height / 2), ButtonsArea.Width, ButtonsArea.Height / 2), PartyMenuActions.EditCharacter, "Edit", font);
            Character = null;
        }

        public void Update()
        {
            CreateButton.Update();
            ChangeButton.Update();
            DeleteButton.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D charTexture, Texture2D pixel)
        {
            // Draw slot background
            spriteBatch.Draw(pixel, Bounds, Color.Gray * 0.5f);
            if (Character is not null)
            {
                // Draw character info
                Vector2 namePosition = new Vector2(CharInfoArea.X + 10, CharInfoArea.Y + 10);
                spriteBatch.DrawString(_font, Character.Name, namePosition, Color.White);
                Vector2 classPosition = new Vector2(CharInfoArea.X + 10, CharInfoArea.Y + 40);
                spriteBatch.DrawString(_font, Character.Class.ToString(), classPosition, Color.White);
                Vector2 racePosition = new Vector2(CharInfoArea.X + 10, CharInfoArea.Y + 70);
                spriteBatch.DrawString(_font, Character.Race.ToString(), racePosition, Color.White);
                Vector2 genderPosition = new Vector2(CharInfoArea.X + 10, CharInfoArea.Y + 100);
                spriteBatch.DrawString(_font, Character.Gender.ToString(), genderPosition, Color.White);
                // Here you can draw more character info like level, class, etc.
                // Draw character image (placeholder)
                spriteBatch.Draw(charTexture, CharImageArea, Color.White);
            }
            else
            {
                spriteBatch.Draw(pixel, CharImageArea, Color.Blue * 0.5f);
            }

            CreateButton.Draw(spriteBatch, pixel);
            ChangeButton.Draw(spriteBatch, pixel);
            DeleteButton.Draw(spriteBatch, pixel);
        }
    }
}
