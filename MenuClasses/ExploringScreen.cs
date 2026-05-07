using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    public class ExploringScreen : BaseMenu<ScreenAction>
    {
        private Character characterOne;
        private Character characterTwo;
        private Character characterThree;
        private Character characterFour;

        private ActiveIcon CharOnePoster;
        private ActiveIcon CharTwoPoster;
        private ActiveIcon CharThreePoster;
        private ActiveIcon CharFourPoster;

        private GameAssets _assets;

        private int _defaultCharPosterHeight = 200;



        public ExploringScreen(string title, Rectangle frame, SpriteFont font, Texture2D pixel) : base(title, frame, font, pixel)
        {
            _leftPanelCursor.SetPosition(_menuLayout.LeftPanel.Center.X - _defaultButtonWidth / 2, _menuLayout.LeftPanel.Top + _defaultSpacing);
            _leftPanelButtons.Add(AddButton("Back", ScreenAction.GoToMainMenu, _leftPanelCursor));

            var charsPorterWidth = ((_menuLayout.ContentContainer.Right - 10) - (_menuLayout.ContentContainer.Left + 10)) / 4;
            var charPoserTop = _menuLayout.ContentContainer.Bottom - _defaultCharPosterHeight;

            CharOnePoster = new ActiveIcon(new Rectangle(_menuLayout.ContentContainer.Left + 5, charPoserTop, charsPorterWidth, _defaultCharPosterHeight), _font);
            CharTwoPoster = new ActiveIcon(new Rectangle(CharOnePoster.Bounds.Right + 5, charPoserTop, charsPorterWidth, _defaultCharPosterHeight), _font);
            CharThreePoster = new ActiveIcon(new Rectangle(CharTwoPoster.Bounds.Right + 5, charPoserTop, charsPorterWidth, _defaultCharPosterHeight), _font);
            CharFourPoster = new ActiveIcon(new Rectangle(CharThreePoster.Bounds.Right + 5, charPoserTop, charsPorterWidth, _defaultCharPosterHeight), _font);
        }

        public void LoadContent(GameAssets assets)
        {
            _assets = assets;
        }

        public void SetParty(List<Character> party)
        {
            if (party.Count > 0)
            { 
                characterOne = party[0];
                CharOnePoster.SetTexture(_assets.GetCharacterPortraitTexture(characterOne));
            }
            if (party.Count > 1) 
            {
                characterTwo = party[1];
                CharTwoPoster.SetTexture(_assets.GetCharacterPortraitTexture(characterTwo));
            }
            if (party.Count > 2)
            {
                characterThree = party[2];
                CharThreePoster.SetTexture(_assets.GetCharacterPortraitTexture(characterThree));
            }
            if (party.Count > 3)
            {
                characterFour = party[3];
                CharFourPoster.SetTexture(_assets.GetCharacterPortraitTexture(characterFour));
            }
        }

        public override ScreenAction Update()
        {
            ButtonsEnabledManage();
            foreach (var button in _buttons)
            {
                button.Update();
                if (button.IsClicked) return button.Action;
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

            if (characterOne is not null && CharOnePoster.Texture is not null)
            {
                spriteBatch.Draw(CharOnePoster.Texture, CharOnePoster.Bounds, Color.White);
            }
            if (characterTwo is not null && CharTwoPoster.Texture is not null)
            {
                spriteBatch.Draw(CharTwoPoster.Texture, CharTwoPoster.Bounds, Color.White);
            }
            if (characterThree is not null && CharThreePoster.Texture is not null)
            {
                spriteBatch.Draw(CharThreePoster.Texture, CharThreePoster.Bounds, Color.White);
            }
            if (characterFour is not null && CharFourPoster.Texture is not null)
            {
                spriteBatch.Draw(CharFourPoster.Texture, CharFourPoster.Bounds, Color.White);
            }
        }
    }
}
