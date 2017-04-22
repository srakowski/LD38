
using System;
using Microsoft.Xna.Framework;

namespace LD38.Gameplay
{
    public class Eye
    {
        public Transform Transform { get; private set; } = Transform.Default;

        public Eye SetPosition(Vector2 position)
        {
            this.Transform.Position = position;
            return this;
        }
    }
}
