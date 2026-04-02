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
    public class InfoDialog
    {
        public string Message { get; private set; }
        public string Title { get; private set; }

        public bool IsOpen { get; private set; }

        public MenuLayout _layout { get; private set; }

        public List<Button<InfoDialogResult>> Buttons { get; private set; }

        private const int _defaultWindowWidth = 400;
        private const int _defaultWindowHeight = 250;

        public InfoDialog(string title, SpriteFont font, string message, Rectangle frame, int windowWidth = _defaultWindowWidth, int windowHeight = _defaultWindowHeight) //  SpriteFont font, Rectangle frame
        {
            Title = title;
            Message = message;
            Buttons = new List<Button<InfoDialogResult>>();
            var config = new MenuLayoutConfig
            {
                ProcentFrame = 0.6,
                HeaderContainerHeight = 0.2,
                LeftPanelWidth = 0.2,
                RightPanelWidth = 0.2,
                ContentContainerWidth = 0.6,
                FootContainerHeight = 0.2
            };
            _layout = new MenuLayout(frame, config);
            var testButton = new Rectangle(_layout.FooterContainer.Center.X, _layout.FooterContainer.Center.Y, 60, 30);
            Buttons.Add(new Button<InfoDialogResult>(testButton, InfoDialogResult.Yes, "OK", font));
        }

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }

        public InfoDialogResult Update()
        {
            if (this.IsOpen)
            {
                foreach (var button in Buttons)
                {
                    button.Update();
                    if (button.IsClicked)
                    {
                        // IsOpen = false;
                        return button.Action;
                    }
                }
            }
            return InfoDialogResult.None;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D pixel)
        {
            // Draw background
            spriteBatch.Draw(pixel, _layout.ContentContainer, Color.Black * 0.8f);
            // Draw title
            Vector2 titleSize = font.MeasureString(Title);
            Vector2 titlePosition = new Vector2(_layout.ContentContainer.Center.X - titleSize.X / 2, _layout.ContentContainer.Top + 20);
            spriteBatch.DrawString(font, Title, titlePosition, Color.White);
            // Draw message
            Vector2 messageSize = font.MeasureString(Message);
            Vector2 messagePosition = new Vector2(_layout.ContentContainer.Center.X - messageSize.X / 2, _layout.ContentContainer.Center.Y - messageSize.Y / 2);
            spriteBatch.DrawString(font, Message, messagePosition, Color.White);
            // Draw buttons
            foreach (var button in Buttons)
            {
                button.Draw(spriteBatch, font, pixel);
            }
        }

    }
}
