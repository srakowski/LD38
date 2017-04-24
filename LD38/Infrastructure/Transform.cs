using Microsoft.Xna.Framework;

namespace LD38
{
    public class Transform
    {
        public Vector2 Position = Vector2.Zero;
        public float Rotation = 0f;
        public float Scale = 1f;

        public Transform() { }

        public Transform(Vector2 position, float rotation, float scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public static Transform Default => new Transform();
    }
}
