using Abyss.MenuSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss.Menus
{
    public class ShipMenus
    {
        public static Menu ContextMenu(GameState gs, Ship s)
        {
            List<MenuOption> options = new List<MenuOption>();
            options.Add(new MenuOption("Colonize", mc =>
            {
                var colony = s.Sector.BeginColonization(gs, s.Position);
                mc.PushMenu(PlaceColonyMenu(gs, s, colony));
            }));

            var gobackop = Common.GoBackMenuOption;
            gobackop.Action += new Action<MenuControl>(mc => gs.Select(null));
            options.Add(gobackop);
            return new Menu($"Ship[{s.Name}]", options);
        }

        private static Menu PlaceColonyMenu(GameState gs, Ship s, Colony colony) =>
            new Menu("Colonize", new[] {
                new MenuOption("Place", mc => {
                    s.Sector.PlaceColony(gs, colony);
                    mc.PopMenu().SwapMenus(ColonyMenus.ContextMenu(gs, colony));
                    }),
                new MenuOption("Cancel", mc => {
                    s.Sector.CancelColony(gs, colony);
                    gs.Select(s);
                    mc.PopMenu();
                    })
                });
    }
}
