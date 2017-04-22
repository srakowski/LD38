using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD38.Gameplay
{
    public enum ShipType
    {
        SmallTrading,
        LargeTrading
    }


    public class Ship
    {
        public ShipType Type { get; }
        public Player Owner { get; }
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public GoodsStorage[] CargoSlots { get; }

        private Ship(ShipType type,
            Player owner,
            Vector2 position,
            int cargoSlots)
        {
            Type = type;
            Owner = owner;
            Position = position;
            CargoSlots = new GoodsStorage[cargoSlots];
        }

        public GoodsStorage Load(GoodsStorage goods) =>
            CargoSlots.Load(goods);

        public static Ship SmallTrading(Player owner, Vector2 position) =>
            new Ship(ShipType.SmallTrading, owner, position, 4);

        public static Ship LargeTrading(Player owner, Vector2 position) =>
            new Ship(ShipType.LargeTrading, owner, position, 8);
    }
}
