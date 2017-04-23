using System;
using System.Collections.Generic;
using System.Text;
using Abyss.MenuSystem;

namespace Abyss.Menus
{
    class ColonyMenus
    {
        internal static Menu MainMenu(GameState gs, Colony c, MenuOption customReturn = null)
        {
            return new Menu("Menu",
                new[]
                {
                    customReturn ?? Common.GoBackMenuOption()
                });
        }
    }
}
