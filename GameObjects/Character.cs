using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.GameObjects
{
    public class Character
    {
        public string Name { get; set; }
        public CharacterRace Race { get; set; }
        public CharacterClass Class { get; set; }
        public CharacterGender Gender { get; set; }

        public Character()
        {
            Name = "New Character";
            Race = CharacterRace.Human;
            Class = CharacterClass.Warrior;
            Gender = CharacterGender.Male;
        }
    }

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
}

