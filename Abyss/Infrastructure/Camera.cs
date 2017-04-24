using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace Abyss.Infrastructure
{
    public class Camera
    {
        public Vector2 Position { get; set; } = new Vector2(0, 3000);
        public float Rotation { get; set; } = 0f;
        public float Scale { get; set; } = 1f;

        private GraphicsDevice GraphicsDevice { get; }

        public Camera(GraphicsDevice gd) =>
            GraphicsDevice = gd;

        internal Matrix TransformationMatrix =>
            Matrix.Identity *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(Scale) *
            Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) *
            Matrix.CreateTranslation(
                (GraphicsDevice.Viewport.Width * 0.5f),
                (GraphicsDevice.Viewport.Height * 0.5f),
                0f);

        public Vector2 ToWorldCoords(Vector2 coords) =>
            Vector2.Transform(coords, Matrix.Invert(this.TransformationMatrix));

        public Vector2 ToScreenCoords(Vector2 coords) =>
            Vector2.Transform(coords, this.TransformationMatrix);

        private Coroutine _moveTo = null;

        internal void MoveToLocation(Vector2 pos) =>
            BeginMoveTo(pos);

        private void BeginMoveTo(Vector2 pos)
        {
            Position = pos + new Vector2(160, 0);
            //_moveTo?.Stop();
            //_moveTo = new Coroutine(MoveTo(pos));
            //Coroutines.Start(_moveTo);
        }

        public const float MoveSpeed = 0.006f;

        private IEnumerator MoveTo(Vector2 target)
        {
            while (Vector2.Distance(Position, target) > 1f)
            {
                Position = Vector2.SmoothStep(Position, target, Delta.Value * MoveSpeed);
                yield return null;
            }
            this.Position = target;
            _moveTo = null;
        }
    }
}