using System;
using System.Collections;
using System.Collections.Generic;

namespace Abyss
{
    public class GameState
    {
        public Faction PlayerFaction { get; private set; }

        private List<Ship> _playerShips = new List<Ship>();
        public IEnumerable<Ship> PlayerShips => _playerShips;

        private List<Colony> _playerColonies = new List<Colony>();
        public IEnumerable<Colony> PlayerColonies => _playerColonies;

        private List<Planet> _planets = new List<Planet>();
        public IEnumerable<Planet> Planets => _planets;


        public GameState Initialize(Faction faction)
        {
            PlayerFaction = faction;
            _playerShips.Add(Ship.Starter(faction));
            return this;
        }
    }
}
