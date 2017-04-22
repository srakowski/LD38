using System.Collections.Generic;

namespace LD38.Gameplay
{
    public struct World
    {
        public IEnumerable<Player> Players { get; }
        public IEnumerable<Place> Places { get; }
    }
}
