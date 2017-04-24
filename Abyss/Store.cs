using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Abyss
{
    public class Store : IControllable
    {
        public long[] costs = new long[]
        {
            1,
            100,
            500,
            1000,
            5000,
            13000,
            24000,
            1000000
        };

        public Point Position { get; }
        public Sector Sector { get; }
        public Vector2 RenderPosition => Position.ToVector2() * Config.CellSize;

        public Store (Point pos) => Position = pos;

        public bool Buy(GameState gs, int i)
        {
            if (gs.Credits >= costs[i])
            {
                gs.Credits -= costs[i];
                costs[i] = costs[i] * 2;
                return true;
            }
            return false; 
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

        public void ActionUp() {
        }
    }
}
