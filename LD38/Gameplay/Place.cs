using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace LD38.Gameplay
{
    public class Place : IClickable
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

        public bool IsClickHit(Vector2 clickPos) => false;

        public IClickable Select(Vector2 clickPos, IClickable prevSelected)
        {
            return this;
        }

        public void Unselect()
        {
        }

        public void Action(float delta, Vector2 clickPos)
        {
        }

        internal static Place FromMap(int posX, int posY, Texture2D texture2D)
        {
            var placePos = new Vector2(posX, posY);
            var landUnits = new List<LandUnit>();
            var data = new Color[texture2D.Width * texture2D.Height];
            texture2D.GetData(data);
            for (int y = 0; y < texture2D.Height; y++)
            for (int x = 0; x < texture2D.Width; x++)
                {
                    var color = data[(y * texture2D.Width) + x];
                    if (color.A == 0) continue;
                    if (color.R == 0 && color.G == 0 && color.B == 0) continue;

                    landUnits.Add(new LandUnit(
                        placePos + (new Vector2(x, y) * Sizes.LandUnitDims.ToVector2()),
                        color.A / 3));
                }
            return new Place(placePos, landUnits);
        }
    }
}