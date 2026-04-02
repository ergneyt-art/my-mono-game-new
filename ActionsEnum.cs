using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame
{
    public enum ScreenAction
    {
        None,
        GoToCharacterMenu,
        GoToMainMenu,
        GoToLoadGameMenu,
        GoToSettingsMenu,
        GoToAboutGameMenu,
        StartGame,
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
