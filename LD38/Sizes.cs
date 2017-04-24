using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD38
{
    public static class Sizes
    {
        public static readonly Vector2 WorldViewportDim = new Vector2(960, 720);
        public static readonly Vector2 TileOrigin = new Vector2(9, 9);
        public static readonly Point LandUnitDims = new Point(20, 20);
        public static readonly Point MiniMapDims = new Point(300, 300);
        public static readonly Point HalfWorldDim = new Point((MiniMapDims.X * LandUnitDims.X) / 2, (MiniMapDims.Y * LandUnitDims.Y) / 2);
        public static readonly Rectangle WorldBounds = new Rectangle(-(MiniMapDims.X * LandUnitDims.X)/2, -(MiniMapDims.Y * LandUnitDims.Y)/2, (MiniMapDims.X * LandUnitDims.X), (MiniMapDims.Y * LandUnitDims.Y));
        public static readonly Rectangle ScreenBounds = new Rectangle(0, 0, 1280, 720);
        public static readonly float ShipClickThreshold = 24;
    }
}
