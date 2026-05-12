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
        public Dictionary<CharacterRace, Dictionary<CharacterGender, Texture2D>> CharactersFullTexture { get; private set; }
        public Dictionary<CharacterRace, Dictionary<CharacterGender, Texture2D>> CharactersPortraitTexture { get; private set; }
        public Dictionary<CharacterStatus, Texture2D> StatusTextures { get; private set; }

        public GameAssets(ContentManager manager) 
        {
            LoadAssets(manager);
        }

        public Texture2D GetStatusTexture(CharacterStatus status)
        {
            if (StatusTextures.ContainsKey(status))
            {
                return StatusTextures[status];
            }
            else
            {
                throw new ArgumentException($"Texture for status {status} is not found");
            }
        }

        public Texture2D GetCharacterTexture(CharacterRace race, CharacterGender gender)
        {
            if (CharactersFullTexture.ContainsKey(race))
            {
                if (CharactersFullTexture[race].ContainsKey(gender))
                {
                    return CharactersFullTexture[race][gender];
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

        public Texture2D GetCharacterPortraitTexture(Character character)
        {
            if (character == null) return null;
            if (CharactersPortraitTexture.ContainsKey(character.Race))
            {
                if (CharactersPortraitTexture[character.Race].ContainsKey(character.Gender))
                {
                    return CharactersPortraitTexture[character.Race][character.Gender];
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

        public Texture2D GetCharacterTexture(Character character)
        {
            if (character == null) return null;
            if (CharactersFullTexture.ContainsKey(character.Race))
            {
                if (CharactersFullTexture[character.Race].ContainsKey(character.Gender))
                {
                    return CharactersFullTexture[character.Race][character.Gender];
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
            LoadStatuses(manager);
        }

        private void LoadStatuses(ContentManager manager)
        {
            StatusTextures = new Dictionary<CharacterStatus, Texture2D>();
            var statuses = Enum.GetValues(typeof(CharacterStatus)).Cast<CharacterStatus>().ToList();
            foreach (var status in statuses)
            {
                StatusTextures[status] = manager.Load<Texture2D>($"Statuses/{status.ToString().ToLower()}");
            }
        }

        private void LoadCharacteres(ContentManager manager)
        {
            var characteres = new Dictionary<CharacterRace, Dictionary<CharacterGender, Texture2D>>();
            var races = Enum.GetValues(typeof(CharacterRace)).Cast<CharacterRace>().ToList();
            CharactersFullTexture = new Dictionary<CharacterRace, Dictionary<CharacterGender, Texture2D>>();
            CharactersPortraitTexture = new Dictionary<CharacterRace, Dictionary<CharacterGender, Texture2D>>();

            foreach (var race in races)
            {
                CharactersFullTexture[race] = new Dictionary<CharacterGender, Texture2D>();
                CharactersFullTexture[race][CharacterGender.Male] = manager.Load<Texture2D>($"Characters/{race.ToString().ToLower()}-male-full");
                CharactersFullTexture[race][CharacterGender.Female] = manager.Load<Texture2D>($"Characters/{race.ToString().ToLower()}-female-full");
                CharactersPortraitTexture[race] = new Dictionary<CharacterGender, Texture2D>();
                CharactersPortraitTexture[race][CharacterGender.Male] = manager.Load<Texture2D>($"Characters/{race.ToString().ToLower()}-male-portrait");
                CharactersPortraitTexture[race][CharacterGender.Female] = manager.Load<Texture2D>($"Characters/{race.ToString().ToLower()}-female-portrait");
            }
        }
    }
}
