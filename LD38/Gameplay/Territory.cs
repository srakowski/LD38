using System.Collections.Generic;

namespace LD38.Gameplay
{
    /// <summary>
    /// Aggregate of Player claims over a Place, central goods storage.
    /// </summary>
    public class Territory
    {
        public TerritoryGoods GoodsThisTerritory { get; private set; }
        public IEnumerable<Outpost> Outposts { get; private set; }
        public Territory(Outpost foundingOutpost)
        {
            GoodsThisTerritory = new TerritoryGoods();
            Outposts = new[] { foundingOutpost };
        }
    }
}
