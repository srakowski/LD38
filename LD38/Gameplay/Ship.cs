using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace LD38.Gameplay
{
    public enum ShipType
    {
        SmallTrading,
        LargeTrading
    }


    public class Ship : IClickable
    {
        private Coroutine _moveTo;
        private float speed = 0.1f;
        public ShipType Type { get; }
        public Player Owner { get; }
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public Color Color { get; private set; } = Color.White;
        public GoodsStorage[] CargoSlots { get; }
        public string Name { get; }

        private Ship(ShipType type,
            Player owner,
            Vector2 position,
            int cargoSlots)
        {
            Name = type.ToString();
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

        public bool IsClickHit(Vector2 clickPos) =>
            Vector2.Distance(clickPos, this.Position) < Sizes.ShipClickThreshold;

        public IClickable Select(Vector2 clickPos, IClickable prevSelected)
        {
            this.Color = Color.Lime;
            return this;
        }

        public void Action(float delta, Vector2 clickPos)
        {
            BeginMoveTo(clickPos);
        }

        public void Unselect()
        {
            this.Color = Color.White;
        }

        public void BeginMoveTo(Vector2 clickPos)
        {
            _moveTo?.Stop();
            _moveTo = new Coroutine(MoveTo(clickPos));
            Coroutines.Add(_moveTo);
        }

        private IEnumerator MoveTo(Vector2 target)
        {
            while (Vector2.Distance(Position, target) > 3f)
            {
                var dir = (target - Position);
                dir.Normalize();
                this.Position += (dir * speed * Global.Delta);
                yield return null;
            }
            this.Position = target;
            _moveTo = null;
        }
    }
}
