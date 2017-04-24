using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Abyss.Infrastructure;
using System.Collections;
using System.Collections.Generic;

namespace Abyss
{
    public enum ShipType
    {
        SmallTrading
    }

    public class Ship : INamed, IControllable
    {
        public ShipType Type { get; }

        public string TypeName => Type == ShipType.SmallTrading ? "Small trading" : "";

        public Vector2 RenderPosition => Position.ToVector2() * Config.CellSize;

        public Point Position = Config.ShipEntryPoint;

        public string Name { get; }

        public Sector Sector { get; private set; }

        public object Selected { get; private set; }

        public GameState GameState { get; }

        public CargoBay[] CargoBays { get; }

        public List<int> Inventory { get; } = new List<int>();

        public Ship(string name, ShipType shipType, GameState gs, int cargoBays)
        {
            Name = name;
            Type = shipType;
            GameState = gs;
            CargoBays = new CargoBay[cargoBays];
            for (int i = 0; i < cargoBays; i++)
                CargoBays[i] = new CargoBay();
        }

        public static Ship Starter(Faction faction, GameState gs)
        {
            var s = new Ship(RandomShipName, ShipType.SmallTrading, gs, 4);

            s.CargoBays[0].ResourceType = ResourceType.Metals;
            s.CargoBays[0].Quantity = 50;

            s.CargoBays[1].ResourceType = ResourceType.Organics;
            s.CargoBays[1].Quantity = 50;

            s.CargoBays[2].ResourceType = ResourceType.Water;
            s.CargoBays[2].Quantity = 50;

            s.CargoBays[3].ResourceType = ResourceType.Uranium;
            s.CargoBays[3].Quantity = 50;

            return s;
        }

        public Ship JumpToSector(Sector sector)
        {
            Sector?.RemoveShipe(this);
            Sector = sector;
            Sector?.AddShip(this);
            return this;
        }

        internal void Deselect()
        {
            Selected = null;
        }

        public void ActionUp()
        {
            GameState.Credits++;
            var newPos = Position;
            newPos.Y--;
            TryMove(newPos);
        }

        public void ActionDown()
        {
            GameState.Credits++;
            var newPos = Position;
            newPos.Y++;
            TryMove(newPos);
        }

        public void ActionLeft()
        {
            GameState.Credits++;
            var newPos = Position;
            newPos.X--;
            TryMove(newPos);
        }

        public void ActionRight()
        {
            GameState.Credits++;
            var newPos = Position;
            newPos.X++;
            TryMove(newPos);
        }

        private void TryMove(Point newPos)
        {
            Selected = null;
            newPos = new Point(
                MathHelper.Clamp(newPos.X, 0, Config.SectorWidth - 1),
                MathHelper.Clamp(newPos.Y, 0, Config.SectorHeight - 1));
            if (Sector.Cells[newPos.X, newPos.Y].Occupant != null)
            {
                Selected = Sector.Cells[newPos.X, newPos.Y].Occupant;
                GameState.Select(Selected as IControllable);
                return;
            }
            this.Position = newPos;
        }

        internal void AddInventory(int v)
        {
            if (Inventory.Count < 10)
                Inventory.Add(v);
        }

        public static string RandomShipName => $"the {_firsts[Config.R.Next(0, _firsts.Length)]} {lasts[Config.R.Next(0, lasts.Length)]}";
        private static readonly string[] _firsts = new[]
        {
"slippery",
"boaty",
"gastly",
"night",
"tender",
"bearded",
"deep",
"tiresome",
"ashamed",
"vulgar",
"picayune",
"homeless",
"tricky",
"scarce",
"waggish",
"capable",
"selective",
"didactic",
"witty",
"puffy",
"cloudy",
"milky",
"nimble",
"spotty",
"wary",
"small",
"barbarous",
"astonishing",
"watery",
"dependent",
"stimulating",
"overrated",
"vivacious",
"wry",
"enchanted",
"steady",
"amused",
"illustrious",
"rampant",
"deafening",
"grandiose",
"laughable",
"mellow",
"scientific",
"incredible",
"flawless",
"feeble",
"dysfunctional",
"understood",
"coherent",
"truthful",
"alike",
"cut",
"uninterested",
"unarmed",
"first",
"hesitant",
"sad",
"pushy",
"left",
"synonymous",
"irate",
"violent",
"spiteful",
"past",
"hilarious",
"possible",
"volatile",
"relieved",
"foregoing",
"thin",
"abiding",
"amazing",
"spiritual",
"phobic",
"swanky",
"afraid",
"like",
"cluttered",
"innate",
"dynamic",
"marked",
"eager",
"adorable",
"disgusted",
"quizzical",
"giddy",
"incompetent",
"supreme",
"unused",
"materialistic",
"bouncy",
"warm",
"optimal",
"thoughtful",
"big",
"craven",
"used",
"thirsty",
"purring",
"ubiquitous",
"closed",
"abounding",
"well-groomed",
"third",
"harsh",
};



        private static readonly string[] lasts = new[]
        {
"boatface",
"trotter",
"treader",
"mckfunkypants",
"art",
"sugar",
"pest",
"jewel",
"town",
"representative",
"dress",
"account",
"vessel",
"stretch",
"cobweb",
"rainstorm",
"jump",
"umbrella",
"crown",
"snake",
"deer",
"women",
"color",
"jeans",
"flock",
"anger",
"produce",
"grain",
"thought",
"calculator",
"pen",
"start",
"territory",
"meal",
"elbow",
"bed",
"jelly",
"touch",
"house",
"leg",
"push",
"needle",
"crack",
"morning",
"cows",
"ink",
"amount",
"hand",
"yard",
"trees",
"bear",
"property",
"linen",
"river",
"drawer",
"cast",
"popcorn",
"sofa",
"horse",
"egg",
"earthquake",
"cream",
"hair",
"guitar",
"bike",
"shape",
"cherry",
"range",
"grandfather",
"writer",
"tree",
"page",
"sink",
"record",
"plough",
"north",
"trip",
"mailbox",
"ladybug",
"hat",
"quiver",
"humor",
"bells",
"vacation",
"fly",
"carpenter",
"rake",
"voice",
"straw",
"talk",
"event",
"crayon",
"corn",
"digestion",
"kitty",
"steam",
"teeth",
"songs",
"spring",
"parcel",
"cars",
"spade",
"act",
"temper",
};

        internal void JumpToSector(object sector)
        {
            throw new NotImplementedException();
        }
    }
}