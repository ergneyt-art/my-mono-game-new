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
    public class InfoDialog : BaseElement
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

        public InfoDialog(Rectangle bound, string title, GameContext context, string message, MenuLayoutConfig windowConfig = default) : base(bound, context) //  SpriteFont font, Rectangle frame
        {
            
            var config = windowConfig == default ? _defaultConfig : windowConfig;
            _layout = new MenuLayout(bound, config);
            _titleCursor = new PanelCursor(_layout.HeaderContainer);
            _messageCursor = new PanelCursor(_layout.ContentContainer);
            _buttonsCursor = new PanelCursor(_layout.FooterContainer);
            Title = title;
            Message = message;

            _titleCursor.SetPosition(_titleCursor.CurrentArea.Center.X - TextHelper.GetTextWidth(title, Context.Font) / 2, _titleCursor.CurrentArea.Top + 5);
            var titleArea = _titleCursor.GetNextRect(Direction.Right, TextHelper.GetTextWidth(title, Context.Font), TextHelper.GetTextHeight(title, Context.Font));
            TitleArea = new TextBlock(titleArea, title, Context);
            MessageArea = new TextBlock(_layout.ContentContainer, message, Context);
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
            Buttons.Add(new Button<InfoDialogResult>(rect, action, text, Context));
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
                    if (button.GetClickedStatus())
                    {
                        // IsOpen = false;
                        return button.Action;
                    }
                }
            }
            return InfoDialogResult.None;
        }

        public override void Draw()
        {
            // Draw background
            Context.SpriteBatch.Draw(Context.Pixel, _layout.Screen, Color.Black * 0.8f);

            TitleArea.Draw();
            MessageArea.Draw();
            foreach (var button in Buttons)
            {
                button.Draw();
            }
        }

    }
}
