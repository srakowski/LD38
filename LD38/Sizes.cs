using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD38
{
    public static class Sizes
    {
        public static readonly Vector2 TileOrigin = new Vector2(7, 7);
        public static readonly Rectangle WorldBounds = new Rectangle(-ushort.MaxValue, -ushort.MaxValue, ushort.MaxValue * 2, ushort.MaxValue * 2);
        public static readonly Rectangle ScreenBounds = new Rectangle(0, 0, 1280, 720);
    }
}
