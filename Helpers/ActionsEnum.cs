using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.Helpers
{
    public enum SwitchFieldAction
    {
        None,
        SwitchToNextValue,
        SwitchToPreviousValue
    }

    public enum ScreenAction
    {
        None,
        GoToCharacterMenu,
        GoToMainMenu,
        GoToLoadGameMenu,
        GoToSettingsMenu,
        GoToAboutGameMenu,
        GoToPartyMenu,
        StartGame,
        SaveCharacter,
        Test,
        ExitGame
    }

    public enum PartyMenuActions
    {
        AddCharacter,
        EditCharacter,
        DeleteCharacter
    }

    public enum AddButtonMode
    {
        Top,
        Left,
        Right,
        Bottom,
        Center,
    }

    public enum InfoDialogResult
    {
        None,
        Ok,
        Yes,
        No,
        Cancel
    }
}
