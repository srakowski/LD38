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
        private static Texture2D Default;
        private static Dictionary<string, Texture2D> Textures { get; } = new Dictionary<string, Texture2D>();

        internal static void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Default = content.Load<Texture2D>("sprites/icon");
            Textures["sprites/sidebar"] = content.Load<Texture2D>("sprites/sidebar");
            Textures["sprites/posbox"] = content.Load<Texture2D>("sprites/posbox");
        }

        internal static void Render(GameModel model, SpriteBatch sb)
        {
            sb.Begin(blendState: BlendState.NonPremultiplied, transformMatrix: Hud.Camera.TransformationMatrix);
            foreach (var landUnit in model.Places.SelectMany(p => p.LandUnits))
            {
                sb.Draw(
                    Default,
                    landUnit.Position,
                    null,
                    landUnit.Color,
                    0f,
                    Sizes.TileOrigin,
                    1f,
                    SpriteEffects.None,
                    0f);
            }
            sb.End();

            sb.Begin(transformMatrix: Hud.Camera.TransformationMatrix);
            foreach (var ship in model.Players.SelectMany(p => p.Ships))
                sb.Draw(
                    ship.Type == ShipType.LargeTrading
                        ? Default
                        : Default,
                    ship.Position,
                    null,
                    ship.Color,
                    0f,
                    Sizes.TileOrigin,
                    1f,
                    SpriteEffects.None,
                    0f);
            sb.End();

            sb.Begin(blendState: BlendState.NonPremultiplied,
                samplerState: SamplerState.PointClamp);

            sb.Draw(Textures[Hud.Underlay.TextureKey],
                Hud.Underlay.Position,
                Hud.Underlay.Color);

            var minimapPos = Hud.MiniMap.Position;

            sb.Draw(
                Default,
                new Rectangle(minimapPos.ToPoint(), Sizes.MiniMapDims),
                GameColors.Background);

            sb.Draw(
                Hud.MiniMap.Texture,
                minimapPos,
                Color.White);

            sb.Draw(
                Textures["sprites/posbox"],
                minimapPos + MiniMap.WorldToMiniMapCoords(Hud.Camera.Transform.Position - (Sizes.WorldViewportDim / 2)),
                Color.White);

            foreach (var ship in model.Players.SelectMany(p => p.Ships))
                sb.Draw(Default,
                    new Rectangle(
                        minimapPos.ToPoint() + (MiniMap.WorldToMiniMapCoords(ship.Position) - new Vector2(2, 2)).ToPoint(),
                        new Point(4, 4)),
                    ship.Owner.Color);

            Hud.Title.Draw(sb);

            sb.End();
        }
    }
}
