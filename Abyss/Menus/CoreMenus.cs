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
                new MenuOption("My Ships", Common.PushMenuFromEnumerable(gs, "My Ships", gs.PlayerShips, SelectShip)),
                new MenuOption("My Colonies", Common.PushMenuFromEnumerable(gs, "My Colonies", gs.PlayerColonies, SelectColony)),
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
            mc =>
            {
                gs.Select(colony);
                gs.JumpToSector(colony.Planet.Sector.Number);
                gs.Camera.MoveToLocation(colony.RenderPosition);
                mc.SwapMenus(Common.Todo());
            };
    }
}
