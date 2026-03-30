using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.MenuClasses
{
    public class AboutGameMenu : BaseMenu
    {
        private int buttonWidth = 100;
        private int buttonHeight = 50;
        private int startX;
        private int startY;

        public AboutGameMenu(string title, Viewport viewport, SpriteFont font, Texture2D pixel) : base(title, viewport, font, pixel)
        {
            _spacing = 10;
            _buttons.Add(new Button(new Rectangle(_spacing, _spacing, buttonWidth, buttonHeight), ScreenAction.GoToMainMenu, "Back", _font));
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
