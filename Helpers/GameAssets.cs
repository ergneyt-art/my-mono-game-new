using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.Helpers
{
    public class GameAssets
    {
        public Dictionary<CharacterRace, Dictionary<CharacterGender, Texture2D>> Characters { get; private set; }

        public GameAssets(ContentManager manager) 
        {
            LoadAssets(manager);
        }

        public Texture2D GetCharacterTexture(CharacterRace race, CharacterGender gender)
        {
            if (Characters.ContainsKey(race))
            {
                if (Characters[race].ContainsKey(gender))
                {
                    return Characters[race][gender];
                }
                else
                {
                    throw new ArgumentException($"Texture {gender} for race {race} is not found");
                }
            }
            else
            {
                throw new ArgumentException($"Textures for {race} is not found");
            }
        }

        public Texture2D GetCharacterTexture(Character character)
        {
            if (character == null) return null;
            if (Characters.ContainsKey(character.Race))
            {
                if (Characters[character.Race].ContainsKey(character.Gender))
                {
                    return Characters[character.Race][character.Gender];
                }
                else
                {
                    throw new ArgumentException($"Texture {character.Gender} for race {character.Race} is not found");
                }
            }
            else
            {
                throw new ArgumentException($"Textures for {character.Race} is not found");
            }
        }

        private void LoadAssets(ContentManager manager)
        {
            LoadCharacteres(manager);
        }

        private void LoadCharacteres(ContentManager manager)
        {
            var characteres = new Dictionary<CharacterRace, Dictionary<CharacterGender, Texture2D>>();
            var races = Enum.GetValues(typeof(CharacterRace)).Cast<CharacterRace>().ToList();
            Characters = new Dictionary<CharacterRace, Dictionary<CharacterGender, Texture2D>>();
            foreach (var race in races)
            {
                Characters[race] = new Dictionary<CharacterGender, Texture2D>();
                Characters[race][CharacterGender.Male] = manager.Load<Texture2D>($"Characters/{race.ToString().ToLower()}-Male");
                Characters[race][CharacterGender.Female] = manager.Load<Texture2D>($"Characters/{race.ToString().ToLower()}-Female");
            }
        }
    }
}
