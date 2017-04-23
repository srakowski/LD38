using System;
using System.Collections;
using System.Collections.Generic;

namespace Abyss
{
    public class GameState
    {
        private List<Ship> _playerShips = new List<Ship>();

        public Faction PlayerFaction { get; private set; }

        public IEnumerable<Ship> PlayerShips => _playerShips;

        public GameState Initialize(Faction faction)
        {
            PlayerFaction = faction;
            return this;
        }
    }
}
