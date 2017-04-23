using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Abyss.Infrastructure;
using System.Collections;

namespace Abyss
{
    public enum ShipType
    {
        SmallTrading
    }

    public class Ship : INamed, IControllable
    {
        public ShipType Type { get; }

        public Vector2 RenderPosition => Position.ToVector2() * Config.CellSize;

        public Point Position = new Point(0, 0);

        public string Name { get; }

        public Sector Sector { get; private set; }

        public Ship(string name, ShipType shipType)
        {
            Name = name;
            Type = shipType;
        }

        public static Ship Starter(Faction faction) =>
            new Ship(RandomShipName, ShipType.SmallTrading);

        public Ship JumpToSector(Sector sector)
        {
            Sector?.RemoveShipe(this);
            Sector = sector;
            Sector?.AddShip(this);
            return this;
        }

        public void ActionUp() =>
            this.Position.Y--;

        public void ActionDown() =>
            this.Position.Y++;

        public void ActionLeft() =>
            this.Position.X--;

        public void ActionRight() =>
            this.Position.X++;

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
    }
}