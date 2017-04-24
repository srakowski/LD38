using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abyss
{
    public class Planet : IControllable
    {
        public int TextureIdx { get; }
        public bool IsExplored { get; internal set; }
        public bool IsColonized { get; internal set; }
        public Point Position { get; }
        public Sector Sector { get; }
        public PlanetStats Stats { get; }

        public Vector2 RenderPosition => Position.ToVector2() * Config.CellSize;

        public Planet(int textureIdx, Point pos, Sector sector)
        {
            Position = pos;
            TextureIdx = textureIdx;
            Sector = sector;
            Stats = PlanetStats.Random();
        }

        internal static Planet Random(Point forPoint, Sector sector)
        {
            return new Planet(Config.R.Next(Config.PlanetTextureCount), forPoint, sector);
        }

        internal void Explore(GameState gs)
        {
            if (IsExplored) return;
            if (gs.Credits > Config.CostToSurvey)
            {
                gs.Credits -= Config.CostToSurvey;
                Config.CostToSurvey *= 2;
                IsExplored = true;
            }
        }

        internal void Colonize(GameState gs, Ship s,  Action<Colony> ifSuccess = null, Action ifFail = null)
        {
            if (IsColonized) return;
            if (gs.Credits > this.Stats.CostToColonize) // &&
                //s.CargoBays.Any(cb => cb.ResourceType == ResourceType.Metals && cb.Quantity >= 10) &&
                //s.CargoBays.Any(cb => cb.ResourceType == ResourceType.Organics && cb.Quantity >= 10) &&
                //s.CargoBays.Any(cb => cb.ResourceType == ResourceType.Uranium && cb.Quantity >= 10) &&
                //s.CargoBays.Any(cb => cb.ResourceType == ResourceType.Water && cb.Quantity >= 10))
            {
                gs.Credits -= this.Stats.CostToColonize;
                Config.CostToColonize *= 2;
                IsColonized = true;

                //s.CargoBays.First(cb => cb.ResourceType == ResourceType.Metals && cb.Quantity >= 10).Quantity -= 10;
                //s.CargoBays.First(cb => cb.ResourceType == ResourceType.Organics && cb.Quantity >= 10).Quantity -= 10;
                //s.CargoBays.First(cb => cb.ResourceType == ResourceType.Uranium && cb.Quantity >= 10).Quantity -= 10;
                //s.CargoBays.First(cb => cb.ResourceType == ResourceType.Water && cb.Quantity >= 10).Quantity -= 10;

                Sector.ColoniesThisSector++;
                var c = new Colony(Sector.Name + "-" + names[Sector.ColoniesThisSector % names.Length], this);
                gs.AddPlayerColony(c);
                gs.Select(c);
                ifSuccess?.Invoke(c);
            }
            else
            {
                ifFail?.Invoke();
            }
        }

        public void ActionUp()
        {
            
        }

        public void ActionDown()
        {

        }

        public void ActionLeft()
        {

        }

        public void ActionRight()
        {

        }

        public string[] names = new string[]
        {
            "One", "two", "three", "four", "five", "six", "seven", "eight", "nine",
            "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen",
            "twenty"
        };
    }
}
