using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyMonoGame.InterfaceElements
{
    public class InfoDialog
    {
        public string Message { get; private set; }
        public string Title { get; private set; }
        public bool IsOpen { get; private set; }
        public MenuLayout _layout { get; private set; }
        public TextBlock TitleArea { get; private set; }
        public TextBlock MessageArea { get; private set; }
        public SpriteFont Font { get; private set; }
        public PanelCursor _titleCursor { get; private set; }
        public PanelCursor _messageCursor { get; private set; }
        public PanelCursor _buttonsCursor { get; private set; }
        public List<Button<InfoDialogResult>> Buttons { get; private set; }
        private const int _defaultButtonWidth = 60;
        private const int _defaultButtonHeight = 30;

        private readonly MenuLayoutConfig _defaultConfig = new MenuLayoutConfig
        {
            ProcentFrame = 0.6,
            HeaderContainerHeight = 0.2,
            LeftPanelWidth = 0.2,
            RightPanelWidth = 0.2,
            ContentContainerWidth = 0.6,
            FootContainerHeight = 0.2,
            AddDefaultButton = true,
        };

        public InfoDialog(Rectangle frame, string title, SpriteFont font, string message, MenuLayoutConfig windowConfig = default) //  SpriteFont font, Rectangle frame
        {
            
            var config = windowConfig == default ? _defaultConfig : windowConfig;
            _layout = new MenuLayout(frame, config);
            _titleCursor = new PanelCursor(_layout.HeaderContainer);
            _messageCursor = new PanelCursor(_layout.ContentContainer);
            _buttonsCursor = new PanelCursor(_layout.FooterContainer);
            Font = font;
            Title = title;
            Message = message;

            _titleCursor.SetPosition(_titleCursor.CurrentArea.Center.X - TextHelper.GetTextWidth(title, font) / 2, _titleCursor.CurrentArea.Top + 5);
            var titleArea = _titleCursor.GetNextRect(Direction.Right, TextHelper.GetTextWidth(title, font), TextHelper.GetTextWidth(title, font));
            TitleArea = new TextBlock(titleArea, title, font);
            MessageArea = new TextBlock(_layout.ContentContainer, message, font);
            /*
            TextHelper.SplitText(message, font, _layout.ContentContainer.Width - 10).ForEach(line =>
            {
                _messageCursor.GetNextRect(Direction.Down, TextHelper.GetTextWidth(line, font), TextHelper.GetTextHeight(line, font));
            });
            */
            _buttonsCursor.SetPosition(_buttonsCursor.CurrentArea.Left + 10, _buttonsCursor.CurrentArea.Top + 5);
            Buttons = new List<Button<InfoDialogResult>>();

            if (config.AddDefaultButton)
            {
                SetDefaultButtons();
            }
        }

        private void SetDefaultButtons()
        {
            this.AddButton(InfoDialogResult.Ok, "OK");
            this.AddButton(InfoDialogResult.Cancel, "Cancel");
        }

        public void AddButton(InfoDialogResult action, string text, int width = _defaultButtonWidth, int height = _defaultButtonHeight)
        {
            var rect = _buttonsCursor.GetNextRect(Direction.Right, width, height);
            Buttons.Add(new Button<InfoDialogResult>(rect, action, text, Font));
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
            spriteBatch.Draw(pixel, _layout.Screen, Color.Black * 0.8f);

            TitleArea.Draw(spriteBatch);
            MessageArea.Draw(spriteBatch);
            /*
            // Draw title
            Vector2 titleSize = font.MeasureString(Title);
            Vector2 titlePosition = new Vector2(_layout.ContentContainer.Center.X - titleSize.X / 2, _layout.ContentContainer.Top + 20);
            spriteBatch.DrawString(font, Title, titlePosition, Color.White);
            // Draw message
            Vector2 messageSize = font.MeasureString(Message);
            Vector2 messagePosition = new Vector2(_layout.ContentContainer.Center.X - messageSize.X / 2, _layout.ContentContainer.Center.Y - messageSize.Y / 2);
            spriteBatch.DrawString(font, Message, messagePosition, Color.White);
            */
            // Draw buttons
            foreach (var button in Buttons)
            {
                button.Draw(spriteBatch, pixel);
            }
        }

    }
}
