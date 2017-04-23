using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss
{
    public class Planet : IControllable
    {
        public int TextureIdx { get; }
        public bool IsExplored { get; internal set; }
        public bool IsColonized { get; internal set; }
        public Point Position { get; }
        public Sector Sector { get; }

        public Vector2 RenderPosition => Position.ToVector2() * Config.CellSize;

        public Planet(int textureIdx, Point pos, Sector sector)
        {
            Position = pos;
            TextureIdx = textureIdx;
            Sector = sector;
        }

        internal static Planet Random(Point forPoint, Sector sector)
        {
            return new Planet(Config.R.Next(Config.PlanetTextureCount), forPoint, sector);
        }

        internal void Explore()
        {
            if (IsExplored) return;

            IsExplored = true;
        }

        internal void Colonize(GameState gs, Action ifSuccess = null)
        {
            if (IsColonized) return;
            IsColonized = true;
            var c = new Colony("My Colony", this);
            gs.AddPlayerColony(c);
            gs.Select(c);
            ifSuccess?.Invoke();
        }

        public void ActionUp()
        {
            
        }

        public void ActionDown()
        {

        }

        public void ActionLeft()
        {

        }

        public void ActionRight()
        {

        }
    }
}
