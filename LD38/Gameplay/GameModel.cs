using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LD38.Gameplay
{
    public struct GameModel
    {
        public Eye Eye { get; }
        public IEnumerable<Player> Players { get; }
        public IEnumerable<Place> Places { get; }
        public GameModel(IEnumerable<Player> players, IEnumerable<Place> places)
        {
            Eye = new Eye();
            Players = players;
            Places = places;
        }

        public static GameModel Dev()
        {
            var player = new Player(GameColors.PlayerRed);
            var startingShip = Ship.SmallTrading(player, Vector2.Zero);
            player.AddShip(startingShip);

            return new GameModel(new[] {
                player
            }, new[] 
            {
                new Place(new Vector2(600, 400), new [] {
                    new LandUnit(new Vector2(600, 400))
                })
            });

        }
    }
}
