using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;

namespace LD38.Gameplay
{
    public class MiniMap : IClickable
    {
        public Rectangle Bounds { get; private set; }

        public Vector2 Position { get; private set; }

        public Texture2D Texture { get; private set; }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("sprites/map");
        }

        public void Initialize(GameModel gameModel)
        {
            var data = new Color[Texture.Height * Texture.Width];
            Texture.GetData(data);
            var landUnits = gameModel.Places.SelectMany(p => p.LandUnits);
            foreach (var landUnit in landUnits)
            {
                var pos = landUnit.Position;
                var coords = WorldToMiniMapCoords(pos);
                data[MathHelper.Clamp((int)((coords.Y * Texture.Width) + coords.X), 0, data.Length) - 1] = landUnit.Color;
            }
            Texture.SetData(data);
            Position = Hud.Underlay.Position + new Vector2(10, 16);
            Bounds = new Rectangle(Position.ToPoint(), Sizes.MiniMapDims);
        }

        public static Vector2 WorldToMiniMapCoords(Vector2 pos) =>
            (pos + Sizes.HalfWorldDim.ToVector2()) / Sizes.LandUnitDims.ToVector2();

        public static Vector2 MiniMapToWorldCoords(Vector2 pos) =>
            (pos * Sizes.LandUnitDims.ToVector2()) - Sizes.HalfWorldDim.ToVector2();

        public bool IsClickHit(Vector2 worldClick)
        {
            var click = Hud.Camera.ToScreenCoords(worldClick);
            return this.Bounds.Contains(click);
        }

        public IClickable Select(Vector2 clickPos, IClickable prevSelected)
        {
            var click = Hud.Camera.ToScreenCoords(clickPos);
            click -= Position;
            var worldCoords = MiniMapToWorldCoords(click);
            if (prevSelected is Ship)
            {
                (prevSelected as Ship).BeginMoveTo(worldCoords);
            }
            else
            {
                Hud.Camera.Transform.Position = worldCoords;
            }
            return prevSelected;
        }

        public void Action(float delta, Vector2 clickPos) { }

        public void Unselect() { }

        internal static Vector2 WorldToMiniMapCoords(object p)
        {
            throw new NotImplementedException();
        }
    }
}
