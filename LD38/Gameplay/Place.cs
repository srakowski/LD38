using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LD38.Gameplay
{
    public class Place
    {
        public Vector2 Position { get; }

        public IEnumerable<LandUnit> LandUnits { get; }

        public Place(
            Vector2 position,
            IEnumerable<LandUnit> landUnits)
        {
            Position = position;
            LandUnits = landUnits;
        }
    }
}