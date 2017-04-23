using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Abyss
{
    public class Sector : INamed
    {
        public int Number { get; }

        private string _name = "Some Sector";

        public string Name => IsExplored ? _name : "Unexplored";

        public bool IsExplored { get; set; } = false;

        public Cell[,] Cells { get; }

        private List<Ship> _shipsInThisSector = new List<Ship>();

        public IEnumerable<Ship> ShipsInSector => _shipsInThisSector;

        private List<Colony> _coloniesThisSector = new List<Colony>();

        public IEnumerable<Colony> ColoniesThisSector => _coloniesThisSector;

        public Sector(int number)
        {
            Number = number;
            _name = $"{_sectorFirstNames.First()} {_sectorLastNames.First()}";
            _sectorFirstNames.Remove(_sectorFirstNames.First());
            _sectorLastNames.Remove(_sectorLastNames.First());
            Cells = new Cell[Config.SectorWidth, Config.SectorHeight];
            for (int y = 0; y < Config.SectorHeight; y++)
                for (int x = 0; x < Config.SectorWidth; x++)
                {
                    if (y == Config.SectorHeight / 2 && x == Config.SectorWidth / 2)
                        continue;

                    var spawnPlanet = Config.R.Next(0, 100) == 50;
                    if (!spawnPlanet) continue;
                    Cells[x, y] = new Cell(Planet.Random(new Point(x, y), this));
                }
        }

        internal void AddShip(Ship ship) =>
            _shipsInThisSector.Add(ship);

        internal void RemoveShipe(Ship ship) =>
            _shipsInThisSector.Remove(ship);

        static Sector()
        {
            _sectorFirstNames = new List<string>(_sectorFirstNames.OrderBy(g => Guid.NewGuid()));
            _sectorLastNames = new List<string>(_sectorLastNames.OrderBy(g => Guid.NewGuid()));
        }

        private static List<string> _sectorFirstNames = new List<string>
        {
            "red",
            "green",
            "blue",
            "yellow",
            "orange",
            "purple",
            "cyan",
            "black",
            "white",
            "peachpuff",
            "magenta",
            "dark",
            "light",
            "rogue",
            "Kasprzak",
            "quill",
            "Pixel",
            "singularity",
            "cherry"
        };

        private static List<string> _sectorLastNames = new List<string>
        {
            "alpha",
            "beta",
            "gamma",
            "delta",
            "epsilon",
            "zeta",
            "eta",
            "theta",
            "iota",
            "kappa",
            "lambda",
            "mu",
            "nu",
            "xi",
            "omicron",
            "pi",
            "rho",
            "sigma",
            "tau",
            "upsilon",
            "phi",
            "chi",
            "psi",
            "omega"
        };
    }
}
