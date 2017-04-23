using System;
using System.Collections.Generic;
using System.Text;
using Abyss.MenuSystem;

namespace Abyss.Menus
{
    public static class Common
    {
        public static MenuOption GoBackMenuOption => new MenuOption("Go Back", mc => mc.PopMenu(), isCancel: true);

        public static MenuOption EmptyMenuOption => new MenuOption("Empty", mc => mc.PopMenu(), justText: true);

        public static Action<MenuControl> Confirm(string prompt, Action<MenuControl> ifYes) =>
            mc => mc.PushMenu(new Menu(prompt, new[]
            {
                        new MenuOption("Yes", _ => ifYes(mc.PopMenu())),
                        new MenuOption("No", _ => mc.PopMenu()),
            }));

    }
}
