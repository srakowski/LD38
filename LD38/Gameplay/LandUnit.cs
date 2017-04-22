using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace LD38.Gameplay
{
    public class LandUnit
    {
        public Vector2 Position { get; }
        public Color Color { get; set; }
        public List<Outpost> Claims { get; } = new List<Outpost>();
        public LandUnit(Vector2 position, int alpha)
        {
            Position = position;
            Color = new Color(GameColors.EmptyLandUnitColor, alpha);
        }
    }
}