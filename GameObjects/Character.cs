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

        public int Health { get; set; } = 0;
        
        public int MaxHealth { get; set; } = 0;

        public int Stamina { get; set; } = 0;

        public int MaxStamina { get; set; } = 0;

        public int Mana { get; set; } = 0;

        public int MaxMana { get; set; } = 0;

        public int Strength { get; set; } = 0;
        public int Agility { get; set; } = 0;
        public int Intelligence { get; set; } = 0;

        public int Experience { get; set; } = 0;

        public int Level { get; set; } = 1;

        public List<CharacterStatus> CharacterEffects { get; private set; } = new List<CharacterStatus>();

        public Character()
        {
            Name = "New Character";
            Race = CharacterRace.Human;
            Class = CharacterClass.Warrior;
            Gender = CharacterGender.Male;
        }

        public void SetStartingParams() 
        {
            switch (Class)
            {
                case CharacterClass.Warrior:
                    Health = MaxHealth = 150;
                    Stamina = MaxStamina = 100;
                    Mana = MaxMana = 50;
                    Strength = 10;
                    Agility = 5;
                    Intelligence = 3;
                    break;
                case CharacterClass.Mage:
                    Health = MaxHealth = 100;
                    Stamina = MaxStamina = 50;
                    Mana = MaxMana = 150;
                    Strength = 3;
                    Agility = 5;
                    Intelligence = 10;
                    break;
                case CharacterClass.Archer:
                    Health = MaxHealth = 120;
                    Stamina = MaxStamina = 120;
                    Mana = MaxMana = 30;
                    Strength = 7;
                    Agility = 10;
                    Intelligence = 5;
                    break;
                case CharacterClass.Rogue:
                    Health = MaxHealth = 110;
                    Stamina = MaxStamina = 150;
                    Mana = MaxMana = 20;
                    Strength = 5;
                    Agility = 10;
                    Intelligence = 7;
                    break;
            }
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

