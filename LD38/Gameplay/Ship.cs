using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace LD38.Gameplay
{
    public class Ship
    {
        // visual rep, position
        public Texture2D Texture { get; }
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }

        // storage
        public GoodsStorage[] CargoSlots { get; }

        private Ship(
            Texture2D texture,
            Vector2 position,
            int cargoSlots)
        {
            Texture = texture;
            Position = position;
            CargoSlots = new GoodsStorage[cargoSlots];
        }

        public GoodsStorage Load(GoodsStorage goods) =>
            CargoSlots.Load(goods);
    }
}
