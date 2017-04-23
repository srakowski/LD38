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
                new MenuOption("Sectors", PushMenuFromEnumerable(gs, "Sectors", gs.Sectors, SelectSector, shouldJustText: s => !s.IsExplored)),
                new MenuOption("Quit", Common.Confirm("Quit?", Pop), isCancel: true)
            });

        private static Action<MenuControl> SelectShip(GameState gs, Ship ship) =>
            mc => 
            {
                gs.Select(ship);
                gs.JumpToSector(ship.Sector.Number);
                gs.Camera.MoveToLocation(ship.RenderPosition);
                mc.SwapMenus(ShipMenus.ContextMenu(gs, ship));
            };   

        private static void Pop(MenuControl mc) => mc.PopMenu();

        private static Action<MenuControl> SelectColony(GameState gs, Colony colony) =>
            mc => mc.PopMenu();

        private static Action<MenuControl> PushMenuFromEnumerable<T>(GameState gs, string title, IEnumerable<T> t, Func<GameState, T, Action<MenuControl>> onSelect, Func<T, bool> shouldJustText = null) where T : INamed =>
            mc => mc.PushMenu(MenuFromEnumerable(gs, title, t, onSelect, shouldJustText: shouldJustText));

        private static Menu MenuFromEnumerable<T>(GameState gs, string title, IEnumerable<T> t, Func<GameState, T, Action<MenuControl>> onSelect, Func<T, bool> shouldJustText = null) where T : INamed =>
            new Menu(title,
                (t.Any() ? t.Select(c => new MenuOption(c.Name, onSelect(gs, c), justText: shouldJustText?.Invoke(c) ?? false)) : new[] { Common.EmptyMenuOption })
                .Concat(new[] { Common.GoBackMenuOption }));

        private static Action<MenuControl> SelectSector(GameState gs, Sector sector) =>
            mc => mc.PopMenu();
    }
}
