using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.GameObjects
{


    public enum CharacterClass
    {
        Warrior,
        Mage,
        Archer,
        Rogue
    }

    public enum CharacterRace
    {
        Human,
        Elf,
        Dwarf,
        Orc
    }

    public enum CharacterGender
    {
        Male,
        Female
    };

    public enum CharacterStatus
    {
        Poison,
        Burn,
        Freeze,
        Blessing,
        Fear,
        Heal,
        Shield,
    }
}
