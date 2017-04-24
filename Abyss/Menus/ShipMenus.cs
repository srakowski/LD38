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
                        Common.FormatValue("cost to survey", $"${Config.CostToSurvey}"),
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
                                    new MenuOption("collect credits", _ =>
                                {
                                    gs.Credits += c.Credits;
                                    c.Credits = 0;
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
                        Common.FormatValue("sector", p.Sector.Name),
                        Common.FormatValue("colonization cost", $"${p.Stats.CostToColonize}"),
                        //Common.FormatValue("operating cost", $"${p.Stats.CostToOperate}"),
                        //"",
                        //"Resources",
                        //Common.FormatValue("habitable", p.Stats.IsHabitable ? "yes" : "no"),
                        //Common.FormatValue("water", p.Stats.Water.ToString()),
                        //Common.FormatValue("organics", p.Stats.Organics.ToString()),
                        //Common.FormatValue("metals", p.Stats.Metals.ToString()),
                        //Common.FormatValue("uranium", p.Stats.Uranium.ToString()),
                        //"",
                        //"More information",
                        //"10 of each resource are required to",
                        //"colonize a planet. These resources",
                        //"must be available in the cargo bays",
                        //"of the colonizing ship",
                        //"",
                        //"Cargo Bays"
                    };

                    //foreach (var cb in s.CargoBays)
                    //    report.Add(Common.FormatValue(cb.ResourceType.ToString(), cb.Quantity.ToString()));

                    dataSectionText = report;
                }
                options.Add(new MenuOption("back to ship", mc =>
                {
                    s.Deselect();
                    gs.Select(s);
                }, isCancel: true));
            }



            else if (s.Selected != null && s.Selected is Colony c)
            {
                List<MenuOption> extraOpts = new List<MenuOption>();
                foreach (var item in s.Inventory)
                    extraOpts.Add(
                    new MenuOption($"Apply {(item == 0 ? "micro" : item == 1 ? "normal" : item == 2 ? "turbo" : item == 3 ? "mega" : item == 4 ? "monster" : item == 5 ? "ultra" : item == 6 ? "bf" : item == 7 ? "ultimate" : "bug")}extractor", mc =>
                    {
                        s.Inventory.Remove(item);
                        c.ApplyMultiplier(item);
                        mc.SwapMenus(ContextMenu(gs, s));
                    }));

                return ColonyMenus.MainMenu(gs, c,
                    new MenuOption("Collect credits", mc =>
                {
                    gs.Credits += c.Credits;
                    c.Credits = 0;
                    s.Deselect();
                    gs.Select(s);
                    mc.SwapMenus(ContextMenu(gs, s));
                }, isCancel: true), extraOpts);
            }




            else if (s.Selected != null && s.Selected is Store t)
            {
                title = "Store - Extractors";
                options.Add(new MenuOption(Common.FormatValue("micro", t.costs[0].ToString()), mc => { if (t.Buy(gs, 0)) s.AddInventory(0); }, enabled: s.Inventory.Count < 10 && gs.Credits >= t.costs[0]));
                options.Add(new MenuOption(Common.FormatValue("normal", t.costs[1].ToString()), mc => { if (t.Buy(gs, 1)) s.AddInventory(1); }, enabled: s.Inventory.Count < 10 && gs.Credits >= t.costs[1]));
                options.Add(new MenuOption(Common.FormatValue("turbo", t.costs[2].ToString()), mc => { if (t.Buy(gs, 2)) s.AddInventory(2); }, enabled: s.Inventory.Count < 10 && gs.Credits >= t.costs[2]));
                options.Add(new MenuOption(Common.FormatValue("mega", t.costs[3].ToString()), mc => { if (t.Buy(gs, 3)) s.AddInventory(3); }, enabled: s.Inventory.Count < 10 && gs.Credits >= t.costs[3]));
                options.Add(new MenuOption(Common.FormatValue("monster", t.costs[4].ToString()), mc => { if (t.Buy(gs, 4)) s.AddInventory(4); }, enabled: s.Inventory.Count < 10 && gs.Credits >= t.costs[4]));
                options.Add(new MenuOption(Common.FormatValue("ultra", t.costs[5].ToString()), mc => { if (t.Buy(gs, 5)) s.AddInventory(5); }, enabled: s.Inventory.Count < 10 && gs.Credits >= t.costs[5]));
                options.Add(new MenuOption(Common.FormatValue("bf", t.costs[6].ToString()), mc => { if (t.Buy(gs, 6)) s.AddInventory(6); }, enabled: s.Inventory.Count < 10 && gs.Credits >= t.costs[6]));
                options.Add(new MenuOption(Common.FormatValue("ultimate", t.costs[7].ToString()), mc => { if (t.Buy(gs, 7)) s.AddInventory(7); }, enabled: s.Inventory.Count < 10 && gs.Credits >= t.costs[7]));

                options.Add(new MenuOption("back to ship", mc =>
                {
                    s.Deselect();
                    gs.Select(s);
                }, isCancel: true));

                dataSectionTitle = "ship inventory";
                var report = new List<string>();

                foreach (var item in s.Inventory)
                {
                    report.Add(
                        (item == 0 ? "micro" :
                        item == 1 ? "normal" :
                        item == 2 ? "turbo" :
                        item == 3 ? "mega" :
                        item == 4 ? "monster" :
                        item == 5 ? "ultra" :
                        item == 6 ? "bf" :
                        item == 7 ? "ultimate" :
                        "bug") + " extractor");
                }

                dataSectionText = report;
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
                        Common.FormatValue("position", $"{s.Sector.Number}:{s.Position.X}:{s.Position.Y}"),
                        //"",
                        //"Cargo Bays",
                    };

                //foreach (var cb in s.CargoBays)
                //    report.Add(Common.FormatValue(cb.ResourceType.ToString(), cb.Quantity.ToString()));

                report.AddRange(new[]
                {
                    "",
                    "Use Arrows or WASD to explore every",
                    "move yields a credit to your global",
                    "account"
                });

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
