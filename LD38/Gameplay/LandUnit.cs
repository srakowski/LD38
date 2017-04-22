using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace LD38.Gameplay
{
    public class LandUnit
    {
        public Vector2 Position { get; }
        public List<Outpost> Claims { get; } = new List<Outpost>();
        public LandUnit(Vector2 position)
        {
            Position = position;
        }
    }
}