using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Abyss
{
    public class Colony : INamed, IControllable
    {
        public string Name { get; }

        public Vector2 RenderPosition => Position.ToVector2() * Config.CellSize;

        public Point Position => Planet.Position;

        public Planet Planet { get; }

        public int Population { get; set; } = 2;

        public int OperatingCost => Planet.Stats.CostToOperate;

        public int TaxRevenue => Population * 1;

        public Sector Sector => Planet?.Sector;

        public Colony(string name, Planet planet)
        {
            Name = name;
            Planet = planet;
            Planet.Sector.Cells[Planet.Position.X, planet.Position.Y] = new Cell(this);
        }

        public void ActionUp()
        {
            return;
        }

        public void ActionDown()
        {
            return;
        }

        public void ActionLeft()
        {
            return;
        }

        public void ActionRight()
        {
            return;
        }

        internal Point GetOpenAdjacentPos()
        {
            var pos = new Point(
                MathHelper.Clamp(this.Position.X - 1, 0, Config.SectorWidth), 
                MathHelper.Clamp(this.Position.Y, 0, Config.SectorHeight));
            if (Sector.Cells[pos.X, pos.Y].Occupant == null) return pos;
            pos = new Point(
                MathHelper.Clamp(this.Position.X + 1, 0, Config.SectorWidth),
                MathHelper.Clamp(this.Position.Y, 0, Config.SectorHeight));
            if (Sector.Cells[pos.X, pos.Y].Occupant == null) return pos;

            pos = new Point(
                MathHelper.Clamp(this.Position.X, 0, Config.SectorWidth),
                MathHelper.Clamp(this.Position.Y - 1, 0, Config.SectorHeight));
            if (Sector.Cells[pos.X, pos.Y].Occupant == null) return pos;

            pos = new Point(
                MathHelper.Clamp(this.Position.X, 0, Config.SectorWidth),
                MathHelper.Clamp(this.Position.Y + 1, 0, Config.SectorHeight));
            if (Sector.Cells[pos.X, pos.Y].Occupant == null) return pos;

            return Config.ShipEntryPoint;
        }
    }
}
