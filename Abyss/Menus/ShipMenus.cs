using Abyss.MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abyss.Menus
{
    public class ShipMenus
    {
        public static Menu ContextMenu(GameState gs, Ship s)
        {
            string dataSectionTitle = null;
            var dataSectionText = Enumerable.Empty<string>();
            var title = $"MENU";
            List<MenuOption> options = new List<MenuOption>();
            if (s.Selected != null && s.Selected is Planet p)
            {
                title = "Menu";
                if (!p.IsExplored)
                {
                    options.Add(new MenuOption("Survey Planet",
                        mc => p.Explore(gs)));

                    dataSectionTitle = "More information";
                    var report = new[]
                    {
                        Common.FormatValue("cost to survey", "$20"),
                    };
                    dataSectionText = report;
                }
                else if (!p.IsColonized)
                {
                    title = "Menu";
                    options.Add(new MenuOption("Colonize Planet",
                        mc =>
                        {
                            p.Colonize(gs, s, c =>
                            {
                                mc.SwapMenus(ColonyMenus.MainMenu(gs, c, 
                                    new MenuOption("Back to ship", _ =>
                                {
                                    s.Deselect();
                                    gs.Select(s);
                                    mc.SwapMenus(ContextMenu(gs, s));
                                }, isCancel: true)));
                            }, 
                            () =>
                            {
                                mc.SwapMenus(ContextMenu(gs, s));
                            });
                        }));

                    dataSectionTitle = "Survey Report";
                    var report = new List<string>
                    {
                        "General",
                        Common.FormatValue("sector", p.Sector.Name),
                        Common.FormatValue("colonization cost", $"${p.Stats.CostToColonize}"),
                        Common.FormatValue("operating cost", $"${p.Stats.CostToOperate}"),
                        "",
                        "Resources",
                        Common.FormatValue("habitable", p.Stats.IsHabitable ? "yes" : "no"),
                        Common.FormatValue("water", p.Stats.Water.ToString()),
                        Common.FormatValue("organics", p.Stats.Organics.ToString()),
                        Common.FormatValue("metals", p.Stats.Metals.ToString()),
                        Common.FormatValue("uranium", p.Stats.Uranium.ToString()),
                        "",
                        "More information",
                        "10 of each resource are required to",
                        "colonize a planet. These resources",
                        "must be available in the cargo bays",
                        "of the colonizing ship",
                        "",
                        "Cargo Bays"
                    };

                    foreach (var cb in s.CargoBays)
                        report.Add(Common.FormatValue(cb.ResourceType.ToString(), cb.Quantity.ToString()));

                    dataSectionText = report;
                }
                options.Add(new MenuOption("Back to ship", mc =>
                {
                    s.Deselect();
                    gs.Select(s);
                }, isCancel: true));
            }
            else if (s.Selected != null && s.Selected is Colony c)
            {
                return ColonyMenus.MainMenu(gs, c,
                    new MenuOption("Back to ship", mc =>
                {
                    s.Deselect();
                    gs.Select(s);
                    mc.SwapMenus(ContextMenu(gs, s));
                }, isCancel: true));
            }
            else
            {
                options.Add(new MenuOption("Jump to sector", Common.PushMenuFromEnumerable(gs, "Sectors", gs.Sectors, (_, sec) => JumpToSector(gs, sec, s))));
                if (gs.PlayerColonies.Any())
                {
                    options.Add(new MenuOption("Jump to colony", Common.PushMenuFromEnumerable(gs, "Colonies", gs.PlayerColonies, (_, cols) => JumpToColony(gs, cols, s))));
                }
                options.Add(Common.GoBackMenuOption(() => { gs.Select(null); }));

                dataSectionTitle = $"{s.Name}";
                var report = new List<string>
                    {
                        Common.FormatValue("ship type", s.Type.ToString()),
                        "",
                        "Cargo Bays",
                    };

                foreach (var cb in s.CargoBays)
                    report.Add(Common.FormatValue(cb.ResourceType.ToString(), cb.Quantity.ToString()));

                dataSectionText = report;
            }
            return new Menu(title, options, mc => mc.SwapMenus(ContextMenu(gs, s)))
            {
                DataSectionTitle = dataSectionTitle,
                DataSectionText = dataSectionText
            };
        }

        private static Action<MenuControl> JumpToSector(GameState gs, Sector sector, Ship s) =>
            mc =>
            {
                mc.PopMenu();
                gs.JumpToSector(sector.Number);
                s.JumpToSector(sector);
                s.Position = Config.ShipEntryPoint;
            };

        private static Action<MenuControl> JumpToColony(GameState gs, Colony col, Ship s) =>
            mc =>
            {
                mc.PopMenu();
                gs.JumpToSector(col.Sector.Number);
                s.JumpToSector(col.Sector);
                s.Position = col.GetOpenAdjacentPos();
            };
    }
}
