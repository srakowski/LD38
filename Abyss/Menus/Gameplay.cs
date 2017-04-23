using System;
using Abyss.MenuSystem;
using System.Linq;

namespace Abyss.Menus
{
    public static class Gameplay
    {
        public static Menu Root(GameState gs) =>
            new Menu($"{gs.PlayerFaction} Abyss", new[]
            {
                new MenuOption("&Ships", mc => mc.PushMenu(ShipsMenu(gs))),
                new MenuOption("&Colonies", mc => mc.PushMenu(ColoniesMenu(gs))),
                new MenuOption("&Quit", Confirm("Quit?", Pop))
            });

        private static Menu ShipsMenu(GameState gs) =>
            new Menu("Ships", gs.PlayerShips.Select(s => new MenuOption(s.Name, SelectShip(s))));

        private static Menu ColoniesMenu(GameState gs)
        {
            throw new NotImplementedException();
        }

        private static Action<MenuControl> SelectShip(Ship ship)
        {
            throw new NotImplementedException();
        }

        private static Action<MenuControl> Confirm(string prompt, Action<MenuControl> ifYes) =>
            mc => mc.PushMenu(new Menu(prompt, new[]
            {
                new MenuOption("&No", _ => mc.PopMenu()),
                new MenuOption("&Yes", _ => ifYes(mc.PopMenu())),
            }));

        private static void Pop(MenuControl mc) => mc.PopMenu();
    }
}
