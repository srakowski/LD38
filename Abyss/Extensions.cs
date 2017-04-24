using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss
{
    public static class Extensions
    {
        public static Vector2 CenterOrigin(this Texture2D self) =>
            new Vector2(self.Width * 0.5f, self.Height * 0.5f);
    }
}
