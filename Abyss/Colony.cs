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
    }
}
