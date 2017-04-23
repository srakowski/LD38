using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss
{
    public class InputState
    {
        public KeyboardState PrevKBState { get; private set; } = new KeyboardState();
        public KeyboardState CurrKBState { get; private set; } = new KeyboardState();
        public void Update()
        {
            PrevKBState = CurrKBState;
            CurrKBState = Keyboard.GetState();
        }
    }
}
