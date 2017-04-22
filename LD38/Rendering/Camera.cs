
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD38.Rendering
{
    internal class Camera
    {
        public Transform Transform { get; set; } = Transform.Default;

        private GraphicsDevice GraphicsDevice { get; }

        public Camera(GraphicsDevice gd) =>
            GraphicsDevice = gd;

        internal Matrix TransformationMatrix =>
            Matrix.Identity *
            Matrix.CreateRotationZ(Transform.Rotation) *
            Matrix.CreateScale(Transform.Scale) *
            Matrix.CreateTranslation(-Transform.Position.X, -Transform.Position.Y, 0f) *
            Matrix.CreateTranslation(
                (GraphicsDevice.Viewport.Width * 0.5f),
                (GraphicsDevice.Viewport.Height * 0.5f),
                0f);

        public Vector2 ToWorldCoords(Vector2 coords) =>
            Vector2.Transform(coords, Matrix.Invert(this.TransformationMatrix));
    }
}