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
                if (mousePos.X < Sizes.ScreenBounds.Left) self.Eye.ScrollLeft(delta);
                else if (mousePos.X > Sizes.ScreenBounds.Right) self.Eye.ScrollRight(delta);

                if (mousePos.Y < Sizes.ScreenBounds.Top) self.Eye.ScrollUp(delta);
                else if (mousePos.Y > Sizes.ScreenBounds.Bottom) self.Eye.ScrollDown(delta);
            }
            return self;
        }

        private const float speed = 1f;

        private static void ScrollDown(this Eye self, float delta) =>
            self.SetPosition(
                self.Transform.Position.ShiftY(speed * delta)
                .Clamp(Sizes.WorldBounds));

        private static void ScrollUp(this Eye self, float delta) =>
            self.SetPosition(
                self.Transform.Position.ShiftY(-speed * delta)
                .Clamp(Sizes.WorldBounds));

        private static void ScrollRight(this Eye self, float delta) =>
            self.SetPosition(
                self.Transform.Position.ShiftX(speed * delta)
                .Clamp(Sizes.WorldBounds));

        private static void ScrollLeft(this Eye self, float delta) =>
            self.SetPosition(
                self.Transform.Position.ShiftX(-speed * delta)
                .Clamp(Sizes.WorldBounds));
    }
}
