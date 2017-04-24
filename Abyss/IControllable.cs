using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss
{
    public interface IControllable
    {
        Vector2 RenderPosition { get; }
        void ActionUp();
        void ActionDown();
        void ActionLeft();
        void ActionRight();
    }
}
