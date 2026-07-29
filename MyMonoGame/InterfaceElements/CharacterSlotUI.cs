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
    public class CharacterSlotUI : BaseElement
    {
        public Character? Character;
        public Rectangle CharInfoArea;
        public Rectangle CharImageArea;
        public Rectangle ButtonsArea;
        public Texture2D CharTexture;
        public Button<PartyMenuActions> CreateButton;
        public Button<PartyMenuActions> ChangeButton;
        public Button<PartyMenuActions> DeleteButton;
        private const int _defaultButtonWidth = 80;
        private const int _defaultButtonHeight = 40;
        private const int _defaultButtonSpacing = 5;

        public CharacterSlotUI(Rectangle frame, GameContext context, int buttonWidth = _defaultButtonWidth, int buttonHeight = _defaultButtonHeight, int buttonSpacing = _defaultButtonSpacing) : base(frame, context)
        {
            
            var infoAreaHeight = (int)(frame.Height * 0.3f);
            var imageAreaHeight = (int)(frame.Height * 0.5f);
            var buttonsAreaHeight = frame.Height - infoAreaHeight - imageAreaHeight;

            CharInfoArea = new Rectangle(frame.X, frame.Y, frame.Width, infoAreaHeight);
            CharImageArea = new Rectangle(frame.X, CharInfoArea.Bottom, frame.Width, imageAreaHeight);
            ButtonsArea = new Rectangle(frame.X, CharImageArea.Bottom, frame.Width, buttonsAreaHeight);

            DeleteButton = new Button<PartyMenuActions>(new Rectangle(ButtonsArea.X, ButtonsArea.Y, ButtonsArea.Width, ButtonsArea.Height / 2), PartyMenuActions.DeleteCharacter, "Delete", Context);
            CreateButton = new Button<PartyMenuActions>(new Rectangle(ButtonsArea.X, ButtonsArea.Y, ButtonsArea.Width, ButtonsArea.Height / 2), PartyMenuActions.AddCharacter, "Add", Context);
            ChangeButton = new Button<PartyMenuActions>(new Rectangle(ButtonsArea.X, ButtonsArea.Y + (ButtonsArea.Height / 2), ButtonsArea.Width, ButtonsArea.Height / 2), PartyMenuActions.EditCharacter, "Edit", Context);
            Character = null;
        }

        public void Update()
        {
            CreateButton.Update();
            ChangeButton.Update();
            DeleteButton.Update();
        }

        public override void Draw()
        {
            // Draw slot background
            Context.SpriteBatch.Draw(Context.Pixel, Bounds, Color.Gray * 0.5f);
            if (Character is not null)
            {
                // Draw character info
                Vector2 namePosition = new Vector2(CharInfoArea.X + 10, CharInfoArea.Y + 10);
                Context.SpriteBatch.DrawString(Context.Font, Character.Name, namePosition, Color.White);
                Vector2 classPosition = new Vector2(CharInfoArea.X + 10, CharInfoArea.Y + 40);
                Context.SpriteBatch.DrawString(Context.Font, Character.Class.ToString(), classPosition, Color.White);
                Vector2 racePosition = new Vector2(CharInfoArea.X + 10, CharInfoArea.Y + 70);
                Context.SpriteBatch.DrawString(Context.Font, Character.Race.ToString(), racePosition, Color.White);
                Vector2 genderPosition = new Vector2(CharInfoArea.X + 10, CharInfoArea.Y + 100);
                Context.SpriteBatch.DrawString(Context.Font, Character.Gender.ToString(), genderPosition, Color.White);
                // Here you can draw more character info like level, class, etc.
                // Draw character image (placeholder)
                Context.SpriteBatch.Draw(Context.Assets.GetCharacterTexture(Character), CharImageArea, Color.White);
            }
            else
            {
                Context.SpriteBatch.Draw(Context.Pixel, CharImageArea, Color.Blue * 0.5f);
            }

            CreateButton.Draw();
            ChangeButton.Draw();
            DeleteButton.Draw();
        }
    }
}
