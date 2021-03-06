﻿// MIT License - Copyright (C) Shawn Rakowski
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework;

namespace LD38
{
    public static class Extensions
    {
        public static Vector2 ShiftX(this Vector2 self, float x) =>
            self + new Vector2(x, 0);

        public static Vector2 ShiftY(this Vector2 self, float y) =>
            self + new Vector2(0, y);

        public static Vector2 Shift(this Vector2 self, float x, float y) =>
            self + new Vector2(x, y);

        public static Vector2 Clamp(this Vector2 self, float minX, float maxX, float minY, float maxY) =>
            new Vector2(MathHelper.Clamp(self.X, minX, maxX), MathHelper.Clamp(self.Y, minY, maxY));

        public static Vector2 Clamp(this Vector2 self, Rectangle bounds) =>
            self.Clamp(bounds.Left, bounds.Right, bounds.Top, bounds.Bottom);
    }
}
