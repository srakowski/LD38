using System.Collections.Generic;

namespace LD38.Gameplay
{
    public class Outpost : Building
    {
        public IEnumerable<LandUnit> Claims { get; }
        public Outpost(Player owner, IEnumerable<LandUnit> claims)
            : base(owner)
        {
            Claims = claims;
        }
    }
}