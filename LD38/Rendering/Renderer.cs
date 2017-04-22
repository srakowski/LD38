using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using LD38.Gameplay;
using System.Linq;
using Microsoft.Xna.Framework;

namespace LD38.Rendering
{
    public static class Renderer
    {
        private static Camera Camera;
        private static Texture2D Default;
        private static Texture2D EmptyLandUnit;
        private static Texture2D SmallTradingShipTexture;
        private static Texture2D LargeTradingShipTexture;

        internal static void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Camera = new Camera(graphicsDevice);
            Default = content.Load<Texture2D>("sprites/icon");
            EmptyLandUnit = Default;
            SmallTradingShipTexture = Default;
            LargeTradingShipTexture = Default;
        }

        internal static void Render(GameModel model, SpriteBatch sb)
        {
            Camera.Transform = model.Eye.Transform;

            sb.Begin(blendState: BlendState.NonPremultiplied, transformMatrix: Camera.TransformationMatrix);
            foreach (var landUnit in model.Places.SelectMany(p => p.LandUnits))
            {
                sb.Draw(
                    EmptyLandUnit,
                    landUnit.Position,
                    null,
                    GameColors.EmptyLandUnitColor,
                    0f,
                    Sizes.TileOrigin,
                    1f,
                    SpriteEffects.None,
                    0f);
            }
            sb.End();

            sb.Begin(transformMatrix: Camera.TransformationMatrix);
            foreach (var ship in model.Players.SelectMany(p => p.Ships))
                sb.Draw(
                    ship.Type == ShipType.LargeTrading
                        ? LargeTradingShipTexture
                        : SmallTradingShipTexture,
                    ship.Position,
                    null,
                    ship.Owner.Color,
                    0f,
                    Sizes.TileOrigin,
                    1f,
                    SpriteEffects.None,
                    0f);
            sb.End();
        }
    }
}
