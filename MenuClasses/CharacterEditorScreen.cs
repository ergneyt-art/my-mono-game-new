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
        private Texture2D _charTexture;


        public CharacterEditorScreen(string title, Rectangle frame, GameContext context) :
            base(title, ScreenConfigs.GetCharacterEditorScreenConfig(), frame, context)
        {
            _leftPanelCursor.SetPosition(_menuLayout.LeftPanel.Center.X - _defaultButtonWidth / 2, _menuLayout.LeftPanel.Top + _defaultSpacing);
            _leftPanelButtons.Add(AddButton("Back", ScreenAction.GoToPartyMenu, _leftPanelCursor));
            _leftPanelButtons.Add(AddButton("Save", ScreenAction.SaveCharacter, _leftPanelCursor));
            _rightPanelCursor.SetPosition(_menuLayout.RightPanel.Left, _menuLayout.RightPanel.Top + _defaultSpacing);
            _characterClass = new SwitchField<CharacterClass>(_rightPanelCursor.GetNextRect(Direction.Down, _menuLayout.RightPanel.Width, 100), "Class", CharacterClass.Warrior, Context);
            _classInfo = new TextBlock(_rightPanelCursor.GetNextRect(Direction.Down, _menuLayout.RightPanel.Width, 80), "Class Info", Context);
            _characterRace = new SwitchField<CharacterRace>(_rightPanelCursor.GetNextRect(Direction.Down, _menuLayout.RightPanel.Width  , 100), "Race", CharacterRace.Human, Context);
            _raceInfo = new TextBlock(_rightPanelCursor.GetNextRect(Direction.Down, _menuLayout.RightPanel.Width, 80), "Class Info", Context);
            _characterGender = new SwitchField<CharacterGender>(_rightPanelCursor.GetNextRect(Direction.Down, _menuLayout.RightPanel.Width, 100), "Gender", CharacterGender.Male, Context);
            _genderInfo = new TextBlock(_rightPanelCursor.GetNextRect(Direction.Down, _menuLayout.RightPanel.Width, 80), "Class Info", Context);
            _centerPanelCursor.SetPosition(_menuLayout.ContentContainer.Center.X - 75, _menuLayout.ContentContainer.Bottom - _defaultSpacing);
            var rect = _centerPanelCursor.GetNextRect(Direction.Up, 150, 50);
            _characterName = new InputField(rect, Context);
        }

        public void LoadCharacter(Character character)
        {
            if (character == null)
            {
                _characterClass.Value = (CharacterClass)typeof(CharacterClass).GetEnumValues().GetValue(0);
                _characterRace.Value = (CharacterRace)typeof(CharacterRace).GetEnumValues().GetValue(0);
                _characterGender.Value = (CharacterGender)typeof(CharacterGender).GetEnumValues().GetValue(0);
                _characterName.Text = "";
            }
            else
            {
                CurrentCharacter = character;
                _characterClass.Value = character.Class;
                _characterRace.Value = character.Race;
                _characterGender.Value = character.Gender;
                _characterName.Text = character.Name;
            }
            _classInfo.Text = Descriptions.CharacterClassDescriptions[_characterClass.Value];
            _raceInfo.Text = Descriptions.CharacterRaceDescriptions[_characterRace.Value];
            _genderInfo.Text = Descriptions.CharacterGenderDescriptions[_characterGender.Value];
        }

        private void UpdateInfoBlocks()
        {
            _classInfo.Text = Descriptions.CharacterClassDescriptions[_characterClass.Value];
            _raceInfo.Text = Descriptions.CharacterRaceDescriptions[_characterRace.Value];
            _genderInfo.Text = Descriptions.CharacterGenderDescriptions[_characterGender.Value];
        }


        public override ScreenAction Update()
        {
            if (_infoDialog != null)
            {
                var dialogResult = _infoDialog.Update();
                if (dialogResult != InfoDialogResult.None)
                {
                    _infoDialog.Close();
                    _infoDialog = null;
                    return ScreenAction.None;
                }
            }
            else
            {
                ButtonsEnabledManage();
                _characterClass.Update();
                _characterRace.Update();
                _characterGender.Update();
                _characterName.Update();
                UpdateInfoBlocks();
                SetCharacterTexture();
                foreach (var button in _buttons)
                {
                    button.Update();
                    if (button.GetClickedStatus())
                    {
                        if (button.Action == ScreenAction.SaveCharacter)
                        {
                            if (ValidateCharacterParams())
                            {
                                CharUpdate();
                                return button.Action;
                            }
                            else
                            {
                                _infoDialog = new InfoDialog(_menuLayout.ContentContainer, "Validation Error", Context, "Please ensure all character parameters are valid before saving.");
                                TurnOffAllButtons();
                                _infoDialog.Open();
                                // Handle validation failure (e.g., show an error message)
                                return ScreenAction.None;
                            }
                        }
                        else
                        {
                            return button.Action;
                        }
                    }
                }
            }
            return ScreenAction.None;
        }

        private void CharUpdate()
        {
            if (CurrentCharacter == null)
            {
                CurrentCharacter = new Character()
                {
                    Class = _characterClass.Value,
                    Race = _characterRace.Value,
                    Gender = _characterGender.Value,
                    Name = _characterName.Text
                };
            }
            else
            {
                CurrentCharacter.Class = _characterClass.Value;
                CurrentCharacter.Race = _characterRace.Value;
                CurrentCharacter.Gender = _characterGender.Value;
                CurrentCharacter.Name = _characterName.Text;
            }
            CurrentCharacter.SetStartingParams();
        }


        private bool ValidateCharacterParams()
        {
            // Implement any validation logic for the character here
            // For example, you could check if the name is not empty, or if certain combinations of class
            if (string.IsNullOrWhiteSpace(_characterName.Text))
            {
                return false;
            }
            return true;
        }

        private void SetCharacterTexture()
        {
            if (Context.Assets is not null)
            {
                _charTexture = Context.Assets.GetCharacterTexture(_characterRace.Value, _characterGender.Value);
            }
        }
                

        public override void Draw()
        {
            
            _characterClass.Draw();
            _characterRace.Draw();
            _characterGender.Draw();
            _characterName.Draw();
            _classInfo.Draw();
            _raceInfo.Draw();
            _genderInfo.Draw();
            if (_charTexture is not null)
            {
                Context.SpriteBatch.Draw(_charTexture, new Rectangle(
                (int)(_menuLayout.ContentContainer.Center.X - (_menuLayout.ContentContainer.Width * 0.3)),
                (int)(_menuLayout.ContentContainer.Center.Y - (_menuLayout.ContentContainer.Height * 0.3)),
                (int)(_menuLayout.ContentContainer.Width * 0.6),
                (int)(_menuLayout.ContentContainer.Height * 0.6)), Color.White);
            }
            base.Draw();
        }

        internal void CleanChar()
        {
            CurrentCharacter = null;
        }
    }
}
