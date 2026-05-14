using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGame.GameObjects;
using MyMonoGame.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGame.InterfaceElements
{
    public class CharacterStatusWindow : BaseElement
    {
        public ActiveIcon Icon { get; private set; }
        public ValueBar HPbar { get; private set; }
        public ValueBar MPbar { get; private set; }
        public ValueBar StaminaBar { get; private set; }
        public EffectsBar CurrentEffects { get; private set; }

        private readonly int barSpacing = 5;
        private readonly int effectBarHeight = 20;
        private readonly int ParamsBarHeight = 15;

        public CharacterStatusWindow(Rectangle bounds, GameContext context) : base(bounds, context)
        {
            Icon = new ActiveIcon(new Rectangle(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height - (effectBarHeight + ParamsBarHeight * 3)), Context);
            HPbar = new ValueBar(new Rectangle(Bounds.Left, Icon.Bounds.Bottom, Bounds.Width, ParamsBarHeight), Context) { Color = Color.Red };
            MPbar = new ValueBar(new Rectangle(Bounds.Left, HPbar.Bounds.Bottom, Bounds.Width, ParamsBarHeight), Context) { Color = Color.Blue };
            StaminaBar = new ValueBar(new Rectangle(Bounds.Left, MPbar.Bounds.Bottom, Bounds.Width, ParamsBarHeight), Context) { Color = Color.Green };
            CurrentEffects = new EffectsBar(new Rectangle(Bounds.Left, StaminaBar.Bounds.Bottom, Bounds.Width, effectBarHeight), Context);
        }

        public void UpdateStatus(Character character)
        {
            Icon.SetTexture(Context.Assets.GetCharacterPortraitTexture(character));
            HPbar.SetValue(character.Health, character.MaxHealth);
            MPbar.SetValue(character.Mana, character.MaxMana);
            StaminaBar.SetValue(character.Stamina, character.MaxStamina);
            CurrentEffects.SetEffects(character.CharacterEffects);
        }




        public override void Draw()
        {
            Icon.Draw();
            HPbar.Draw();
            MPbar.Draw();
            StaminaBar.Draw();
            CurrentEffects.Draw();
        }
    }
}
