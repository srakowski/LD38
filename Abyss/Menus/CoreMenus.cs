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
            new Menu($"Menu", new[]
            {
                new MenuOption("My Ships", Common.PushMenuFromEnumerable(gs, "My Ships", gs.PlayerShips, SelectShip)),
                new MenuOption("My Colonies", Common.PushMenuFromEnumerable(gs, "My Colonies", gs.PlayerColonies, SelectColony)),
                new MenuOption("Quit", Common.Confirm("Quit?", Pop), isCancel: true)
            }, mc => mc.SwapMenus(Root(gs)))
            {
                //DataSectionTitle = "Global Stats",
                //DataSectionText = new []
                //{
                //    Common.FormatValue("population", gs.Stats.Population.ToString()),
                //    Common.FormatValue("credits", $"${gs.Stats.BankedCredits}"),
                //    Common.FormatValue("colonies", gs.Stats.ColonyCount.ToString()),
                //    Common.FormatValue("ships", gs.Stats.ShipCount.ToString()),
                //    "",
                //    "Balance Sheet",
                //    Common.FormatValue("tax revenue", $"${gs.Stats.TaxRevenue}"),
                //    Common.FormatValue("operating cost", $"[${gs.Stats.OperatingCost}]"),
                //    Common.FormatValue("balance", gs.Stats.Balance < 0 ? $"[${gs.Stats.Balance}]" : $"${gs.Stats.Balance}")
                //}
            };

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
                mc.SwapMenus(ColonyMenus.MainMenu(gs, colony));
            };
    }
}
