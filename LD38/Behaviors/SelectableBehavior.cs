using System;
using Coldsteel.Scripting;
using Microsoft.Xna.Framework;
using System.Linq;
using Coldsteel.Rendering;

namespace LD38.Behaviors
{
    public class SelectableBehavior : Behavior
    {
        private SpriteRenderer spriteRenderer;
        private Color preSelectColor;
        public Action<Vector2> OnSelect { get; set; }
        public Action<Vector2> OnDoAction { get; set; }

        public SelectableBehavior(
            Action<Vector2> onSelect = null,
            Action<Vector2> doAction = null)
        {
            this.OnSelect = onSelect;
            this.OnDoAction = doAction;
        }

        public override void Activate()
        {
            base.Activate();
            spriteRenderer = Entity.Components.OfType<SpriteRenderer>()
                .FirstOrDefault();
        }

        public bool IsHit(Vector2 pointerPos)
        {
            return Vector2.Distance(this.Entity.Transform.Position,
                pointerPos) < 24;
        }

        internal void Select(Vector2 pointPos)
        {
            preSelectColor = spriteRenderer.Color;
            spriteRenderer.Color = Color.Green;
            OnSelect?.Invoke(pointPos);
        }

        internal void Unselect()
        {
            spriteRenderer.Color = preSelectColor;
        }

        internal void DoAction(Vector2 pointPos)
        {
            OnDoAction?.Invoke(pointPos);
        }
    }
}
