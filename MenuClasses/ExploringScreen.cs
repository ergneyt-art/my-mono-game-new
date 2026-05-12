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
        // private List<Character> _party;
        private List<CharacterStatusWindow> _characterStatusWindows;
        private Dictionary<Character, CharacterStatusWindow> partyMapping;

        private GameAssets _assets;

        private int _defaultCharPosterHeight = 200;



        public ExploringScreen(string title, Rectangle frame, SpriteFont font, Texture2D pixel, GameAssets assets) : base(title, frame, font, pixel)
        {
            _leftPanelCursor.SetPosition(_menuLayout.LeftPanel.Center.X - _defaultButtonWidth / 2, _menuLayout.LeftPanel.Top + _defaultSpacing);
            _leftPanelButtons.Add(AddButton("Back", ScreenAction.GoToMainMenu, _leftPanelCursor));
            _assets = assets;
            var charsPorterWidth = ((_menuLayout.ContentContainer.Right - 10) - (_menuLayout.ContentContainer.Left + 10)) / 4;
            var charPoserTop = _menuLayout.ContentContainer.Bottom - _defaultCharPosterHeight;
            _characterStatusWindows = new List<CharacterStatusWindow>();
            for (int i = 0; i < 4; i++)
            {
                var left = _characterStatusWindows.Count == 0 ? _menuLayout.ContentContainer.Left + 5 : _characterStatusWindows.Last().Bounds.Right + 5;
                var charStatusWindow = new CharacterStatusWindow(new Rectangle(left, charPoserTop, charsPorterWidth, _defaultCharPosterHeight), _font, _assets);
                _characterStatusWindows.Add(charStatusWindow);
            }
        }


        public void SetParty(List<Character> party)
        {
            partyMapping = new Dictionary<Character, CharacterStatusWindow>();
            for (int i = 0;i < party.Count;i++)
            {
                partyMapping[party[i]] = _characterStatusWindows[i];
            }
        }

        public override ScreenAction Update()
        {
            ButtonsEnabledManage();
            foreach (var item in partyMapping)
            {
                item.Value.UpdateStatus(item.Key);
            }

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

            foreach (var item in partyMapping)
            {
                item.Value.Draw(spriteBatch, _pixel);
            }
        }
    }
}
