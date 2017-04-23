using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss
{
    public struct Stats
    {
        public int BankedCredits { get; }
        public int TaxRevenue { get; }
        public int OperatingCost { get; }
        public int Balance => TaxRevenue - OperatingCost;
        public int Population { get; }
        public int ColonyCount { get; }
        public int ShipCount { get; }
        public Stats(int credits,
            int taxRevenue,
            int operatingCost,
            int population,
            int colonyCount,
            int shipCount)
        {
            BankedCredits = credits;
            TaxRevenue = taxRevenue;
            OperatingCost = operatingCost;
            Population = population;
            ColonyCount = colonyCount;
            ShipCount = shipCount;
        }
    }
}
