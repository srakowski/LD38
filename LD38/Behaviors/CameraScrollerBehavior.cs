using System;
using Coldsteel.Scripting;
using Coldsteel.Extensions;

namespace LD38.Behaviors
{
    internal class CameraScrollerBehavior : Behavior
    {
        private float speed = 1f;

        internal void ScrollDown(float delta) =>
            this.Entity.SetPosition(
                Transform.Position.ShiftY(speed * delta)
                .Clamp(-1200, 1200, -1800, 1800));

        internal void ScrollUp(float delta) =>
            this.Entity.SetPosition(
                Transform.Position.ShiftY(-speed * delta)
                .Clamp(-1200, 1200, -1800, 1800));

        internal void ScrollRight(float delta) =>
            this.Entity.SetPosition(
                Transform.Position.ShiftX(speed * delta)
                .Clamp(-1200, 1200, -1800, 1800));

        internal void ScrollLeft(float delta) =>
            this.Entity.SetPosition(
                Transform.Position.ShiftX(-speed * delta)
                .Clamp(-1200, 1200, -1800, 1800));
    }
}