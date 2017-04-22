using System;
using System.Collections.Generic;
using System.Text;

namespace LD38.Gameplay
{
    public abstract class Building
    {
        public Player Owner { get; }
        public Building(Player owner) =>
            Owner = owner;
    }
}
