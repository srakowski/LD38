using System;
using System.Collections.Generic;
using System.Text;
using Abyss.MenuSystem;

namespace Abyss.Menus
{
    public static class ColonyMenus
    {
        internal static Menu ContextMenu(GameState gs, Colony colony) =>
            new Menu("Colony", new[]
            {
                Common.GoBackMenuOption
            });
    }
}
