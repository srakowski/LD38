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
        public const int SectorWidth = 30;
        public const int SectorHeight = 30;
        public static int NumSectors = 9;
        public static Vector2 CellSize { get; } = new Vector2(72, 72);
        public static Point ShipEntryPoint = new Point(Config.SectorWidth / 2, Config.SectorHeight / 2);

        public static int NumDataSectionLines = 100;

        public static float CameraFollowThreshold = 0f;
        public const int InputRepeatDelay = 300;
        public const int PlanetTextureCount = 4;
    }
}
