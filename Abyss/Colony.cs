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

        public bool Placed { get; internal set; }

        public Point Position = new Point(0, 0);

        public Colony(string name) => Name = name;

        public void ActionUp()
        {
            if (Placed) return;
            this.Position.Y--;
        }

        public void ActionDown()
        {
            if (Placed) return;
            this.Position.Y++;
        }

        public void ActionLeft()
        {
            if (Placed) return;
            this.Position.X--;
        }

        public void ActionRight()
        {
            if (Placed) return;
            this.Position.X++;
        }
    }
}
