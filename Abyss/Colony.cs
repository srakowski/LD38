using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Linq;
using Abyss.Infrastructure;
using System.Collections;

namespace Abyss
{
    public class Colony : INamed, IControllable
    {
        public Int64 Credits = 0;

        public int WaterLevel => Resources[(int)ResourceType.Water].Quantity;
        public int OrganicsLevel => Resources[(int)ResourceType.Organics].Quantity;
        public int MetalsLevel => Resources[(int)ResourceType.Metals].Quantity;
        public int UraniumLevel => Resources[(int)ResourceType.Uranium].Quantity;

        private bool Happy { get; set; }

        public string Name { get; }

        public Vector2 RenderPosition => Position.ToVector2() * Config.CellSize;

        public Point Position => Planet.Position;

        public Planet Planet { get; }

        public int Population { get; set; } = 2;

        public int TaxRate { get; set; } = 1;

        //public int OperatingCost => Planet.Stats.CostToOperate +
        //    Structures.Sum(GetOperatingCostForStructure);

        private int GetOperatingCostForStructure(Structure arg) =>
            arg == Structure.Settlement ? 0 :
            arg == Structure.WaterFacility ? 20 :
            arg == Structure.Farm ? 20 :
            arg == Structure.MiningFacility ? 30 :
            arg == Structure.EnrichmentFacility ? 40 :
            arg == Structure.ShipYard ? 10 : 0;

        private int GetBuildingCostForStructure(Structure arg) =>
            arg == Structure.Settlement ? 100 :
            arg == Structure.WaterFacility ? 200 :
            arg == Structure.Farm ? 200 :
            arg == Structure.MiningFacility ? 300 :
            arg == Structure.EnrichmentFacility ? 1000 :
            arg == Structure.ShipYard ? 2000 : 0;

        public CargoBay[] Resources { get; } = new CargoBay[5]
        {
            new CargoBay(ResourceType.Empty, detype: false),
            new CargoBay(ResourceType.Water, detype: false),
            new CargoBay(ResourceType.Metals, detype: false),
            new CargoBay(ResourceType.Uranium, detype: false),
            new CargoBay(ResourceType.Organics, detype: false),
        };

        internal void Step()
        {
            Credits += 1;

            //if (Planet.Stats.Water != ResourceAvailability.NA)
            //{
            //    var waterFacs = Structures.Count(s => s == Structure.WaterFacility);
            //    if (waterFacs > 0)
            //    {
            //        if (Planet.Stats.Water == ResourceAvailability.Abundant)
            //        {
            //            Resources[(int)ResourceType.Water].Quantity += (2 * waterFacs);
            //        }
            //        else
            //        {
            //            Resources[(int)ResourceType.Water].Quantity += (waterFacs);
            //        }
            //    }
            //}

            //if (Planet.Stats.Organics != ResourceAvailability.NA)
            //{
            //    var farms = Structures.Count(s => s == Structure.Farm);
            //    if (farms > 0 && Resources[(int)ResourceType.Water].Quantity > 2)
            //    {
            //        Resources[(int)ResourceType.Water].Quantity -= 2;
            //        if (Planet.Stats.Organics == ResourceAvailability.Abundant)
            //        {
            //            Resources[(int)ResourceType.Organics].Quantity += (2 * farms);
            //        }
            //        else
            //        {
            //            Resources[(int)ResourceType.Organics].Quantity += (farms);
            //        }
            //    }
            //}

            //if (Planet.Stats.Metals != ResourceAvailability.NA)
            //{
            //    var strs = Structures.Count(s => s == Structure.MiningFacility);
            //    if (strs > 0)
            //    {
            //        if (Planet.Stats.Metals == ResourceAvailability.Abundant)
            //        {
            //            Resources[(int)ResourceType.Metals].Quantity += (2 * strs);
            //        }
            //        else
            //        {
            //            Resources[(int)ResourceType.Metals].Quantity += (strs);
            //        }
            //    }
            //}

            //if (Planet.Stats.Uranium != ResourceAvailability.NA)
            //{
            //    var strs = Structures.Count(s => s == Structure.EnrichmentFacility);
            //    if (strs > 0 && Resources[(int)ResourceType.Metals].Quantity > 2)
            //    {
            //        Resources[(int)ResourceType.Metals].Quantity -= 2;
            //        if (Planet.Stats.Uranium == ResourceAvailability.Abundant)
            //        {
            //            Resources[(int)ResourceType.Uranium].Quantity += (2 * strs);
            //        }
            //        else
            //        {
            //            Resources[(int)ResourceType.Uranium].Quantity += (strs);
            //        }
            //    }
            //}

            //var haveWater = true;
            //// people use food and water
            //var unitsNeeded = Population / 4;
            //if (WaterLevel < unitsNeeded)
            //{
            //    haveWater = false;
            //}
            //var unitsToConsume = MathHelper.Clamp(unitsNeeded, 0, WaterLevel);
            //Resources[(int)ResourceType.Water].Quantity -= unitsToConsume;

            //var haveFood = true;
            //var foodUnitsNeeded = Population / 8;
            //if (OrganicsLevel < foodUnitsNeeded)
            //{
            //    haveFood = false;
            //}
            //unitsToConsume = MathHelper.Clamp(foodUnitsNeeded, 0, OrganicsLevel);
            //Resources[(int)ResourceType.Organics].Quantity -= unitsToConsume;

            //this.Happy = haveFood && haveWater && TaxRate < 3;

            //var minPop = 2 + (this.Structures.Count(c => c != Structure.Settlement) * 2);
            //var settlementCount = Structures.Count(s => s == Structure.Settlement);
            //int maxPop = (settlementCount * 8) + minPop;

            //if (TaxRate > 6 || (!haveFood && !haveWater))
            //{
            //    this.Population -= 1;
            //}
            //else if (Happy)
            //{
            //    this.Population += 1;
            //}

            //this.Population = MathHelper.Clamp(Population, minPop, maxPop);
        }

        internal void ApplyMultiplier(int item)
        {
            Coroutines.Start(new Coroutine(Derp(item, this)));
        }

        private static IEnumerator Derp(int i, Colony c)
        {
            while (true)
            {
                c.Credits += (i + 1);
                yield return WaitYieldInstruction.Create(1000 - ((i + 1) * 100));
            }
        }

        //public int Balance => TaxRevenue - OperatingCost;

        public int TaxRevenue => (int)(Population * TaxRate);

        public Sector Sector => Planet?.Sector;

        public List<Structure> Structures { get; } = new List<Structure>();

        public Colony(string name, Planet planet)
        {
            Name = name;
            Planet = planet;
            Planet.Sector.Cells[Planet.Position.X, planet.Position.Y] = new Cell(this);
        }

        public void Build(GameState gs, Structure structure)
        {
            var cost = GetBuildingCostForStructure(structure);
            if (gs.Credits >= cost)
            {
                gs.Credits -= cost;
                Structures.Add(structure);
            }
        }

        public void ActionUp()
        {
            return;
        }

        public void ActionDown()
        {
            return;
        }

        public void ActionLeft()
        {
            return;
        }

        public void ActionRight()
        {
            return;
        }

        internal Point GetOpenAdjacentPos()
        {
            var pos = new Point(
                MathHelper.Clamp(this.Position.X - 1, 0, Config.SectorWidth), 
                MathHelper.Clamp(this.Position.Y, 0, Config.SectorHeight));
            if (Sector.Cells[pos.X, pos.Y].Occupant == null) return pos;
            pos = new Point(
                MathHelper.Clamp(this.Position.X + 1, 0, Config.SectorWidth),
                MathHelper.Clamp(this.Position.Y, 0, Config.SectorHeight));
            if (Sector.Cells[pos.X, pos.Y].Occupant == null) return pos;

            pos = new Point(
                MathHelper.Clamp(this.Position.X, 0, Config.SectorWidth),
                MathHelper.Clamp(this.Position.Y - 1, 0, Config.SectorHeight));
            if (Sector.Cells[pos.X, pos.Y].Occupant == null) return pos;

            pos = new Point(
                MathHelper.Clamp(this.Position.X, 0, Config.SectorWidth),
                MathHelper.Clamp(this.Position.Y + 1, 0, Config.SectorHeight));
            if (Sector.Cells[pos.X, pos.Y].Occupant == null) return pos;

            return Config.ShipEntryPoint;
        }

        internal void LowerTaxRate()
        {
            this.TaxRate -= 1;
            this.TaxRate = MathHelper.Clamp(this.TaxRate, 0, 10);
        }

        internal void RaiseTaxRate()
        {
            this.TaxRate += 1;
            this.TaxRate = MathHelper.Clamp(this.TaxRate, 0, 10);
        }
    }
}
