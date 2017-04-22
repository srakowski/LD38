using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace LD38.Gameplay
{
    public struct GameModel
    {
        public IEnumerable<Player> Players { get; }
        public IEnumerable<Place> Places { get; }
        public GameModel(IEnumerable<Player> players, IEnumerable<Place> places)
        {
            Players = players;
            Places = places;
        }

        public static GameModel Dev(IEnumerable<Texture2D> placeMaps)
        {
            var player = new Player(GameColors.PlayerRed);
            var startingShip = Ship.SmallTrading(player, Vector2.Zero);
            player.AddShip(startingShip);




            return new GameModel(new[] {
                player
            }, new[] 
            {
                Place.FromMap(600, 400, placeMaps.First()),
                Place.FromMap(-2000, -200, placeMaps.Skip(1).First())
            });

        }
    }
}
