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
    public class EffectsBar : BaseElementWithTooltip
    {
        public Dictionary<CharacterStatus, ActiveIcon> Effects { get; private set; }
        private PanelCursor Cursor;
        private GameAssets Assets;

        public EffectsBar(Rectangle bounds, GameContext context) : base(bounds, context)
        {
            Effects = new Dictionary<CharacterStatus, ActiveIcon>();
            Cursor = new PanelCursor(Bounds);
        }

        public void SetEffects(List<CharacterStatus> activeEffects)
        {
            Cursor.SetPosition(Bounds.Left, Bounds.Top);
            Effects.Clear();
            foreach (var effect in activeEffects)
            {
                AddEffect(effect);
            }
        }

        public void AddEffect(CharacterStatus effect)
        {
            if (!this.Effects.ContainsKey(effect))
            {
                var iconRect = Cursor.GetNextRect(Direction.Right, 20, 20, 2); // TO DO: This is a bit hacky, we should probably have a better way to manage the cursor position for the icons
                var effectIcon = new ActiveIcon(iconRect, Context);
                effectIcon.SetTexture(Assets.GetStatusTexture(effect));
                this.Effects[effect] = effectIcon;
            }
        }


        public void RemoveEffect(CharacterStatus effect)
        {
            if (this.Effects.ContainsKey(effect))
            {
                this.Effects.Remove(effect);
            }
        }

        public override void Draw()
        {
            if (IsVisible)
            {
                base.Draw();
                foreach (var icon in Effects)
                {
                    icon.Value.Draw();
                }
            }
        }
    }
}

