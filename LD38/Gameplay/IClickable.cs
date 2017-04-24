using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace LD38.Gameplay
{
    public interface IClickable
    {
        bool IsClickHit(Vector2 worldClick);
        IClickable Select(Vector2 clickPos, IClickable prevSelected);
        void Action(float delta, Vector2 clickPos);
        void Unselect();
        
    }
}
