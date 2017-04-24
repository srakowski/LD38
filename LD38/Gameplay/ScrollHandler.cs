using LD38.Rendering;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD38.Gameplay
{
    public static class Scroller
    {
        public static GameModel HandleScroll(this GameModel self, float delta)
        {
            var mousePos = Mouse.GetState().Position;
            if (!Sizes.ScreenBounds.Contains(mousePos))
            {
                if (mousePos.X < Sizes.ScreenBounds.Left) Hud.Camera.ScrollLeft(delta);
                else if (mousePos.X > Sizes.ScreenBounds.Right) Hud.Camera.ScrollRight(delta);

                if (mousePos.Y < Sizes.ScreenBounds.Top) Hud.Camera.ScrollUp(delta);
                else if (mousePos.Y > Sizes.ScreenBounds.Bottom) Hud.Camera.ScrollDown(delta);
            }
            return self;
        }

        private const float speed = 0.3f;

        private static void ScrollDown(this Camera self, float delta) =>
            self.SetPosition(
                self.Transform.Position.ShiftY(speed * delta)
                .Clamp(Sizes.WorldBounds));

        private static void ScrollUp(this Camera self, float delta) =>
            self.SetPosition(
                self.Transform.Position.ShiftY(-speed * delta)
                .Clamp(Sizes.WorldBounds));

        private static void ScrollRight(this Camera self, float delta) =>
            self.SetPosition(
                self.Transform.Position.ShiftX(speed * delta)
                .Clamp(Sizes.WorldBounds));

        private static void ScrollLeft(this Camera self, float delta) =>
            self.SetPosition(
                self.Transform.Position.ShiftX(-speed * delta)
                .Clamp(Sizes.WorldBounds));
    }
}
