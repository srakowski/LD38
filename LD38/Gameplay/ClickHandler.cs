using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD38.Gameplay
{
    public static class ClickHandler
    {
        static MouseState PreviousState = new MouseState();
        static MouseState CurrentState = new MouseState();
        static IClickable selected;

        public static GameModel HandleClick(this GameModel self, float delta, Func<Vector2, Vector2> screenToWorld)
        {
            PreviousState = CurrentState;
            CurrentState = Mouse.GetState();

            if (!Sizes.ScreenBounds.Contains(CurrentState.Position))
                return self;

            var leftClicked = PreviousState.LeftButton == ButtonState.Pressed && CurrentState.LeftButton == ButtonState.Released;
            var rightClicked = PreviousState.RightButton == ButtonState.Pressed && CurrentState.RightButton == ButtonState.Released;
            if (!leftClicked && !rightClicked)
                return self;

            var clickables = self.Players.SelectMany(p => p.Ships.Select(s => s as IClickable))
                .Concat(self.Places.Select(p => p as IClickable))
                .Concat(new[]
                {
                    Hud.MiniMap as IClickable
                });

            var worldClick = screenToWorld.Invoke(CurrentState.Position.ToVector2());

            if (leftClicked)
            {
                var clicked = clickables.FirstOrDefault(c => c.IsClickHit(worldClick));
                if (clicked == null)
                {
                    selected?.Action(delta, worldClick);
                }
                else
                {
                    var newSelected =clicked?.Select(worldClick, selected);
                    if (newSelected != selected) selected?.Unselect();
                    selected = newSelected;
                }
            }
            else if (rightClicked)
            {
                selected?.Unselect();
                selected = null;
            }


            return self;
        }
    }
}
