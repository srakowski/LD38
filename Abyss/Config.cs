using Microsoft.Xna.Framework;
using System;

namespace Abyss
{
    public static class Config
    {
        public static Random R { get; } = new Random();
        public const int ViewportWidth = 1280;
        public const int ViewportHeight = 720;
        public const int MaxMenuOptions = 10;
        public const int SectorWidth = 200;
        public const int SectorHeight = 200;
        public static int NumSectors = 9;
        public static Vector2 CellSize { get; } = new Vector2(20, 20);
    }
}
