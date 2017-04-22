using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LD38.Gameplay
{
    public class Player
    {
        public Color Color { get; }

        private List<Ship> _ships = new List<Ship>();
        public IEnumerable<Ship> Ships => _ships;

        public Player(Color color)
        {
            this.Color = color;
        }

        internal void AddShip(Ship ship) =>
            _ships.Add(ship);
    }
}