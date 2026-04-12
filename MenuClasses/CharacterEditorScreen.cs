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
    public class CharacterEditorScreen : BaseMenu<ScreenAction>
    {
        public Character CurrentCharacter { get; private set; }
        private SwitchField<CharacterClass> _characterClass;
        private SwitchField<CharacterRace> _characterRace;
        private SwitchField<CharacterGender> _characterGender;
        private TextBlock _classInfo;
        private TextBlock _raceInfo;
        private TextBlock _genderInfo;
        private InputField _characterName;
        private GameAssets _assets;
        private Texture2D _charTexture;

        public CharacterEditorScreen(string title, Rectangle frame, SpriteFont font, Texture2D pixel) :
            base(title, frame, font, pixel)
        {
            _leftPanelCursor.SetPosition(_menuLayout.LeftPanel.Center.X - _defaultButtonWidth / 2, _menuLayout.LeftPanel.Top + _defaultSpacing);
            _leftPanelButtons.Add(AddButton("Back", ScreenAction.GoToPartyMenu, _leftPanelCursor));
            _leftPanelButtons.Add(AddButton("Save", ScreenAction.SaveCharacter, _leftPanelCursor));
            _rightPanelCursor.SetPosition(_menuLayout.RightPanel.Center.X - _defaultButtonWidth / 2, _menuLayout.RightPanel.Top + _defaultSpacing);
            _characterClass = new SwitchField<CharacterClass>(_rightPanelCursor.GetNextRect(Direction.Down, 120, 100), "Class", CharacterClass.Warrior, _font);
            _classInfo = new TextBlock(_rightPanelCursor.GetNextRect(Direction.Down, 120, 80), "Class Info", _font);
            _characterRace = new SwitchField<CharacterRace>(_rightPanelCursor.GetNextRect(Direction.Down, 120, 100), "Race", CharacterRace.Human, _font);
            _raceInfo = new TextBlock(_rightPanelCursor.GetNextRect(Direction.Down, 120, 80), "Class Info", _font);
            _characterGender = new SwitchField<CharacterGender>(_rightPanelCursor.GetNextRect(Direction.Down, 120, 100), "Gender", CharacterGender.Male, _font);
            _genderInfo = new TextBlock(_rightPanelCursor.GetNextRect(Direction.Down, 120, 80), "Class Info", _font);
            _centerPanelCursor.SetPosition(_menuLayout.ContentContainer.Center.X - 75, _menuLayout.ContentContainer.Bottom - _defaultSpacing);
            var rect = _centerPanelCursor.GetNextRect(Direction.Up, 150, 50);
            _characterName = new InputField(rect, _font);
        }

        public void SetCharacterTexture(GameAssets assets)
        {
            _assets = assets;
        }

        public void LoadCharacter(Character character)
        {
            CurrentCharacter = character;
            _characterClass.Value = character.Class;
            _characterRace.Value = character.Race;
            _characterGender.Value = character.Gender;
            _characterName.Text = character.Name;
            _classInfo.Text = Descriptions.CharacterClassDescriptions[CurrentCharacter.Class];
            _raceInfo.Text = Descriptions.CharacterRaceDescriptions[CurrentCharacter.Race];
            _genderInfo.Text = Descriptions.CharacterGenderDescriptions[CurrentCharacter.Gender];
        }

        private void UpdateInfoBlocks()
        {
            _classInfo.Text = Descriptions.CharacterClassDescriptions[_characterClass.Value];
            _raceInfo.Text = Descriptions.CharacterRaceDescriptions[_characterRace.Value];
            _genderInfo.Text = Descriptions.CharacterGenderDescriptions[_characterGender.Value];
        }


        public override ScreenAction Update()
        {
            ButtonsEnabledManage();
            _characterClass.Update();
            _characterRace.Update();
            _characterGender.Update();
            _characterName.Update();
            UpdateInfoBlocks();
            foreach (var button in _buttons)
            {
                button.Update();
                if (button.IsClicked)
                {
                    if (button.Action == ScreenAction.SaveCharacter)
                    {
                        CurrentCharacter.Class = _characterClass.Value;
                        CurrentCharacter.Race = _characterRace.Value;
                        CurrentCharacter.Gender = _characterGender.Value;
                        CurrentCharacter.Name = _characterName.Text;
                    }
                    return button.Action;
                }
            }
            SetCharacterTexture();
            return ScreenAction.None;
        }

        private void SetCharacterTexture()
        {
            if (_assets is not null)
            {
                _charTexture = _assets.GetCharacterTexture(_characterRace.Value, _characterGender.Value);
            }
        }
                

        public override void Draw(SpriteBatch spriteBatch)
        {
            SetTitle(spriteBatch);
            _characterClass.Draw(spriteBatch, _pixel);
            _characterRace.Draw(spriteBatch, _pixel);
            _characterGender.Draw(spriteBatch, _pixel);
            _characterName.Draw(spriteBatch, _pixel);
            _classInfo.Draw(spriteBatch);
            _raceInfo.Draw(spriteBatch);
            _genderInfo.Draw(spriteBatch);

            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch, _pixel);
            }
            if (_charTexture is not null)
            {
                spriteBatch.Draw(_charTexture, new Rectangle(
                (int)(_menuLayout.ContentContainer.Center.X - (_menuLayout.ContentContainer.Width * 0.3)),
                (int)(_menuLayout.ContentContainer.Center.Y - (_menuLayout.ContentContainer.Height * 0.3)),
                (int)(_menuLayout.ContentContainer.Width * 0.6),
                (int)(_menuLayout.ContentContainer.Height * 0.6)), Color.White);
            }
        }
    }
}
