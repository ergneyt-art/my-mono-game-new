using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MyMonoGame
{
    public class CharacterMenuScreen
    {
        private string Title;

        public bool IsCurrentMenu;

        private SpriteFont _font;
        private Texture2D _pixel;

        private Character CharacterSlotOne;
        private Character CharacterSlotTwo;
        private Character CharacterSlotThree;
        private Character CharacterSlotFour;

        private Button CreateCharOnSlotOne;
        private Button CreateCharOnSlotTwo;
        private Button CreateCharOnSlotThree;
        private Button CreateCharOnSlotFour;

        private Button ChangeCharOnSlotOne;
        private Button ChangeCharOnSlotTwo;
        private Button ChangeCharOnSlotThree;
        private Button ChangeCharOnSlotFour;

        private Button BackButton;
        private Button StartButton;

        private int ButtonWidthMarginPercent = 5;
        private int ButtonHeightMarginPercent = 5;
        private int ButtonWidthPercent = 16;
        private int ButtonHeightPercent = 6;

        public CharacterMenuScreen(SpriteFont font, Texture2D pixel, int windowHeight, int windowWeight)
        {
            Title = "Character menu";
            _font = font;
            _pixel = pixel;

            var buttonWidth = (windowWeight / 100) * ButtonWidthPercent;
            var buttonHeight = (windowWeight / 100) * ButtonHeightPercent;

            var buttonMarginWidth = (windowWeight / 100) * ButtonWidthMarginPercent;
            var buttonMarginHeight = (windowWeight / 100) * ButtonHeightMarginPercent;

            var buttonsX_axis = buttonMarginWidth;
            var buttonsY_axis = buttonMarginHeight;

            BackButton = new Button(
                new Rectangle(
                buttonsX_axis,
                buttonsY_axis,
                buttonWidth,
                buttonHeight), "Back", _font);

            buttonsY_axis = windowHeight - buttonMarginHeight - buttonHeight;

            StartButton = new Button(
                new Rectangle(
                    (windowWeight / 2) - (buttonWidth / 2), 
                    buttonsY_axis,
                    buttonWidth,
                    buttonHeight), "Start", _font);

            buttonsY_axis = buttonsY_axis - (buttonMarginHeight + buttonHeight);

            CreateCharOnSlotOne = new Button(new Rectangle(buttonsX_axis, buttonsY_axis, buttonWidth, buttonHeight), "Create character", _font);
            ChangeCharOnSlotOne = new Button(new Rectangle(buttonsX_axis, buttonsY_axis, buttonWidth, buttonHeight), "Change character", _font);

            buttonsX_axis += buttonMarginWidth + buttonWidth;

            CreateCharOnSlotTwo = new Button(new Rectangle(buttonsX_axis, buttonsY_axis, buttonWidth, buttonHeight), "Create character", _font);
            ChangeCharOnSlotTwo = new Button(new Rectangle(buttonsX_axis, buttonsY_axis, buttonWidth, buttonHeight), "Change character", _font);

            buttonsX_axis += buttonMarginWidth + buttonWidth;

            CreateCharOnSlotThree = new Button(new Rectangle(buttonsX_axis, buttonsY_axis, buttonWidth, buttonHeight), "Create character", _font);
            ChangeCharOnSlotThree = new Button(new Rectangle(buttonsX_axis, buttonsY_axis, buttonWidth, buttonHeight), "Change character", _font);

            buttonsX_axis += buttonMarginWidth + buttonWidth;

            CreateCharOnSlotFour = new Button(new Rectangle(buttonsX_axis, buttonsY_axis, buttonWidth, buttonHeight), "Create character", _font);
            ChangeCharOnSlotFour = new Button(new Rectangle(buttonsX_axis, buttonsY_axis, buttonWidth, buttonHeight), "Change character", _font);

            ManageButtons();
        }

        private void ManageButtons()
        {
            if (CharacterSlotOne is null && CharacterSlotTwo is null && CharacterSlotThree is null && CharacterSlotFour is null)
            {
                StartButton.SetEnabled(false);
            }
            else
            {
                StartButton.SetEnabled(true);
            }

            if (CharacterSlotOne != null) 
            {
                this.CreateCharOnSlotOne.HideButton();
                this.ChangeCharOnSlotOne.ShowButton();
            }
            else
            {
                this.CreateCharOnSlotOne.ShowButton();
                this.ChangeCharOnSlotTwo.HideButton();
            }

            if (CharacterSlotTwo != null)
            {
                this.CreateCharOnSlotTwo.HideButton();
                this.ChangeCharOnSlotTwo.ShowButton();
            }
            else
            {
                this.CreateCharOnSlotTwo.ShowButton();
                this.ChangeCharOnSlotTwo.HideButton();
            }

            if (this.CharacterSlotThree != null)
            {
                this.CreateCharOnSlotThree.HideButton();
                this.ChangeCharOnSlotThree.ShowButton();
            }
            else
            {
                this.CreateCharOnSlotThree.ShowButton();
                this.ChangeCharOnSlotThree.HideButton();
            }

            if (this.ChangeCharOnSlotFour != null)
            {
                this.CreateCharOnSlotFour.HideButton();
                this.ChangeCharOnSlotFour.ShowButton();
            }
            else
            {
                this.CreateCharOnSlotFour.ShowButton();
                this.ChangeCharOnSlotFour.HideButton();
            }
        }

        public string Update()
        {
            ManageButtons();
            StartButton.Update();
            BackButton.Update();
            CreateCharOnSlotOne.Update();
            ChangeCharOnSlotOne.Update();
            CreateCharOnSlotTwo.Update();
            ChangeCharOnSlotTwo.Update();
            CreateCharOnSlotThree.Update();
            ChangeCharOnSlotThree.Update();
            CreateCharOnSlotFour.Update();
            ChangeCharOnSlotFour.Update();

            if (BackButton.IsClicked)
            {
                IsCurrentMenu = false;
                return "GoToMainMenu";
            }

            return string.Empty;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            StartButton.Draw(spriteBatch, _font, _pixel);
            BackButton.Draw(spriteBatch, _font, _pixel);
            CreateCharOnSlotOne.Draw(spriteBatch, _font, _pixel);
            ChangeCharOnSlotOne.Draw(spriteBatch, _font, _pixel);
            CreateCharOnSlotTwo.Draw(spriteBatch, _font, _pixel);
            ChangeCharOnSlotTwo.Draw(spriteBatch, _font, _pixel);
            CreateCharOnSlotThree.Draw(spriteBatch, _font, _pixel);
            ChangeCharOnSlotThree.Draw(spriteBatch, _font, _pixel);
            CreateCharOnSlotFour.Draw(spriteBatch, _font, _pixel);
            ChangeCharOnSlotFour.Draw(spriteBatch, _font, _pixel);
        }
    }
}
