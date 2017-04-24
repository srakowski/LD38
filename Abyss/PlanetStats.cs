using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss
{
    public enum ResourceType
    {
        Empty,
        Water,
        Metals,
        Uranium,
        Organics
    }

    public enum ResourceAvailability
    {
        NA,
        Abundant,
        Scarce
    }



    public struct PlanetStats
    {
        public int CostToColonize => Config.CostToColonize;// 100 + (IsHabitable ? 0 : 20) + (Water == ResourceAvailability.NA ? 100 : 0);
        //public int CostToOperate =>// 10 + (IsHabitable ? 0 : 5) + (Water == ResourceAvailability.NA ? 10 : 0);

        public ResourceAvailability Water { get; }
        public ResourceAvailability Metals { get; }
        public ResourceAvailability Uranium { get; }
        public ResourceAvailability Organics { get; }
        public bool IsHabitable { get; }
        public PlanetStats(
            ResourceAvailability water,
            ResourceAvailability metals,
            ResourceAvailability uranium,
            ResourceAvailability organics,
            bool habitable)
        {
            Water = water;
            Metals = metals;
            Uranium = uranium;
            Organics = organics;
            IsHabitable = habitable;
        }

        public static PlanetStats Random()
        {
            ResourceAvailability water = (ResourceAvailability)Config.R.Next(3);
            ResourceAvailability metals = (ResourceAvailability)Config.R.Next(3);
            ResourceAvailability uranium = (ResourceAvailability)Config.R.Next(3);
            ResourceAvailability organics = ResourceAvailability.NA;
            bool habitable = false;
            if (water != ResourceAvailability.NA)
            {
                organics = (ResourceAvailability)Config.R.Next(3);
                if (organics != ResourceAvailability.NA)
                    habitable = true;
            }
            return new PlanetStats(water, metals, uranium, organics, habitable);
        }
    }
}
