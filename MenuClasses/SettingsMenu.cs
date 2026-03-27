using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.MenuClasses
{
    public class SettingsMenu : BaseMenu
    {
        private int buttonWidth = 100;
        private int buttonHeight = 50;
        private int startX;
        private int startY;
        private int spacing = 10;

        public SettingsMenu(string title, int windowHeight, int windowWidth, SpriteFont font, Texture2D pixel) : base(title, windowHeight, windowWidth, font, pixel)
        {
            _buttons.Add(new Button(new Rectangle(spacing, spacing, buttonWidth, buttonHeight), ScreenAction.GoToMainMenu, "Back", _font));
        }

        public override ScreenAction Update()
        {
            foreach (var button in _buttons)
            {
                if (button.Update() != ScreenAction.None) return button.Action;
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
        }
    }
}
