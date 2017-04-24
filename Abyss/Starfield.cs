using Abyss.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss
{
    public class Starfield
    {
        private Texture2D StarTexture { get; }

        private Camera Camera { get; }

        private Vector2 PreviousCameraPos { get; set; }

        private struct Star
        {
            public Vector2 Position { get; }
            public Color Color { get; }
            public Star(Vector2 pos, Color color)
            {
                Position = pos;
                Color = color;
            }
        }

        private const int NumStars = 100;

        private Star[] Stars { get; }

        public Starfield(Texture2D starTexture, Camera camera)
        {
            Camera = camera;
            PreviousCameraPos = camera.Position;
            StarTexture = starTexture;
            Stars = new Star[NumStars];
            var r = new Random();
            for (int i = 0; i < Stars.Length; i++)
            {
                var c = r.Next(50, 256);
                Stars[i] = new Star(
                    new Vector2(r.Next(0, Config.ViewportWidth), r.Next(0, Config.ViewportHeight)),
                    new Color(c, c, c, r.Next(50, 256)));
            }
        }

        public void Update(GameTime gameTime)
        {
            if (PreviousCameraPos != Camera.Position)
            {
                var direction = PreviousCameraPos - Camera.Position;
                direction.Normalize(); ;
                for (int i = 0; i < Stars.Length; i++)
                {
                    var oldPos = Stars[i].Position;
                    var newPos = oldPos + (direction * (Delta.Value * (Stars[i].Color.A / 256f) ));
                    if (newPos.Y < 0) newPos.Y += Config.ViewportHeight;
                    else if (newPos.Y > Config.ViewportHeight) newPos.Y -= Config.ViewportHeight;

                    if (newPos.X < 0) newPos.X += Config.ViewportWidth;
                    else if (newPos.X > Config.ViewportWidth) newPos.X -= Config.ViewportWidth;

                    Stars[i] = new Star(newPos, Stars[i].Color);
                }
            }

            PreviousCameraPos = Camera.Position;
        }

        public void Render(SpriteBatch sb)
        {
            sb.Begin(blendState: BlendState.NonPremultiplied);
            foreach (var star in Stars) sb.Draw(StarTexture, star.Position, star.Color);
            sb.End();
        }

    }
}
