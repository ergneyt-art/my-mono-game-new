using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.GameObjects
{
    public static class Descriptions
    {
        public static Dictionary<CharacterClass, string> CharacterClassDescriptions = new Dictionary<CharacterClass, string>
        {
            { CharacterClass.Warrior, "Warrior: A strong and resilient fighter, excelling in melee combat and defense." },
            { CharacterClass.Mage, "Mage: A master of arcane arts, capable of casting powerful spells to damage enemies or support allies." },
            { CharacterClass.Rogue, "Rogue: A stealthy and agile character, skilled in sneaking, lockpicking, and dealing high damage with critical strikes." },
            { CharacterClass.Archer, "Archer description" }
        };

        public static Dictionary<CharacterRace, string> CharacterRaceDescriptions = new Dictionary<CharacterRace, string>
        {
            { CharacterRace.Human, "Human: Versatile and adaptable, humans can excel in any class and are known for their resilience." },
            { CharacterRace.Elf, "Elf: Graceful and agile, elves have a natural affinity for magic and are skilled archers." },
            { CharacterRace.Dwarf, "Dwarf: Sturdy and strong, dwarves are excellent warriors and craftsmen, with a deep connection to the earth." },
            { CharacterRace.Orc, "Orc: Fierce and powerful, orcs are formidable fighters with a strong sense of honor and loyalty." }
        };

        public static Dictionary<CharacterGender, string> CharacterGenderDescriptions = new Dictionary<CharacterGender, string>
        {
            { CharacterGender.Male, "Male" },
            { CharacterGender.Female, "Female" }
        };
    }
}
