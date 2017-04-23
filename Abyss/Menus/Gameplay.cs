using System;
using Abyss.MenuSystem;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Abyss.Menus
{
    public static class Gameplay
    {
        public static Menu Root(GameState gs) =>
            new Menu($"{gs.PlayerFaction} Abyss", new[]
            {
                new MenuOption("Ships", PushMenuFromEnumerable(gs, "Ships", gs.PlayerShips, SelectShip)),
                new MenuOption("Colonies", PushMenuFromEnumerable(gs, "Colonies", gs.PlayerColonies, SelectColony)),
                new MenuOption("Planets", PushMenuFromEnumerable(gs, "Planets", gs.Planets, SelectPlanet)),
                new MenuOption("Quit", Common.Confirm("Quit?", Pop), isCancel: true)
            });

        private static Action<MenuControl> SelectShip(GameState gs, Ship ship) =>
            mc => mc.PopMenu();

        private static void Pop(MenuControl mc) => mc.PopMenu();

        private static Action<MenuControl> SelectColony(GameState gs, Colony colony) =>
            mc => mc.PopMenu();

        private static Action<MenuControl> PushMenuFromEnumerable<T>(GameState gs, string title, IEnumerable<T> t, Func<GameState, T, Action<MenuControl>> onSelect) where T : INamed =>
            mc => mc.PushMenu(MenuFromEnumerable(gs, title, t, onSelect));

        private static Menu MenuFromEnumerable<T>(GameState gs, string title, IEnumerable<T> t, Func<GameState, T, Action<MenuControl>> onSelect) where T : INamed =>
            new Menu(title,
                (t.Any() ? t.Select(c => new MenuOption(c.Name, onSelect(gs, c))) : new[] { Common.EmptyMenuOption })
                .Concat(new[] { Common.GoBackMenuOption }));

        private static Action<MenuControl> SelectPlanet(GameState gs, Planet arg2) =>
            mc => mc.PopMenu();
    }
}
