using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss
{
    public struct Cell
    {
        public object Occupant { get; }
        public Cell(object occupant) => Occupant = occupant;
    }
}
