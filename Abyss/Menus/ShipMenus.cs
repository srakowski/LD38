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
            var title = $"Ship[{s.Name}]";
            List<MenuOption> options = new List<MenuOption>();
            if (s.Selected != null && s.Selected is Planet p)
            {
                title = "Planet";
                if (!p.IsExplored)
                {
                    options.Add(new MenuOption("Explore Planet",
                        mc => p.Explore()));
                }
                else if (!p.IsColonized)
                {
                    title = "Explored planet";
                    options.Add(new MenuOption("Colonize Planet",
                        mc => 
                        {
                            s.Deselect();
                            p.Colonize(gs, () =>
                            {
                                mc.PopMenu();
                            });
                        }));
                }
                options.Add(new MenuOption("Back to ship", mc =>
                {
                    s.Deselect();
                    gs.Select(s);
                }, isCancel: true));
            }
            else if (s.Selected != null && s.Selected is Colony c)
            {

            }
            else
            {
                options.Add(new MenuOption("Jump to sector", Common.PushMenuFromEnumerable(gs, "Sectors", gs.Sectors, (_, sec) => JumpToSector(gs, sec, s))));
                options.Add(Common.GoBackMenuOption());
            }
            return new Menu(title, options, mc => mc.SwapMenus(ContextMenu(gs, s)));
        }

        private static Action<MenuControl> JumpToSector(GameState gs, Sector sector, Ship s) =>
            mc =>
            {
                mc.PopMenu();
                gs.JumpToSector(sector.Number);
                s.JumpToSector(sector);
                s.Position = Config.ShipEntryPoint;
            };
    }
}
