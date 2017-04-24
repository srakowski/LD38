using System;
using System.Collections.Generic;
using System.Text;
using Abyss.MenuSystem;
using System.Linq;

namespace Abyss.Menus
{
    class ColonyMenus
    {
        internal static Menu MainMenu(GameState gs, Colony c, MenuOption customReturn = null, IEnumerable<MenuOption> extraOptions = null)
        {
            var opts  = new List<MenuOption>(extraOptions ?? Enumerable.Empty<MenuOption>());
            opts.Add(customReturn ?? Common.GoBackMenuOption());
            return new Menu("Menu", opts, mc => mc.SwapMenus(MainMenu(gs, c, customReturn, extraOptions)))
            {
                DataSectionTitle = c.Name,
                DataSectionText = new []
                {
                    Common.FormatValue("", c.Credits.ToString())
                    //Common.FormatValue("population", c.Population.ToString()),
                    //Common.FormatValue("tax rate", c.TaxRate.ToString() + "pct"),
                    //"",
                    //"Balance Sheet",
                    //Common.FormatValue("tax revenue", $"${c.TaxRevenue}"),
                    ////Common.FormatValue("operating cost", $"[${c.OperatingCost}]"),
                    //Common.FormatValue("balace", c.Balance < 0 ? $"[${c.Balance}]" : $"${c.Balance}"),
                    //"",
                    //"Resources",
                    //Common.FormatValue(ResourceType.Water.ToString(), $"{c.WaterLevel}"),
                    //Common.FormatValue(ResourceType.Organics.ToString(), $"{c.OrganicsLevel}"),
                    //Common.FormatValue(ResourceType.Metals.ToString(), $"{c.MetalsLevel}"),
                    //Common.FormatValue(ResourceType.Uranium.ToString(), $"{c.UraniumLevel}"),
                    //"",
                    //"Planet Information",
                    //Common.FormatValue("habitable", c.Planet.Stats.IsHabitable ? "yes" : "no"),
                    //Common.FormatValue("water", c.Planet.Stats.Water.ToString()),
                    //Common.FormatValue("organics", c.Planet.Stats.Organics.ToString()),
                    //Common.FormatValue("metals", c.Planet.Stats.Metals.ToString()),
                    //Common.FormatValue("uranium", c.Planet.Stats.Uranium.ToString())
                }
            };
        }

        public static Menu Build(GameState gs, Colony c) =>
            new Menu("Menu", new[] {
                    new MenuOption("Settlement", Common.Confirm("Build settlement?", _ => c.Build(gs, Structure.Settlement))),
                    new MenuOption("Water collection facility", Common.Confirm("Build water facility?", _ => c.Build(gs, Structure.WaterFacility))),
                    new MenuOption("Farm", Common.Confirm("Build farm?", _ => c.Build(gs, Structure.Farm))),
                    new MenuOption("Mining facility", Common.Confirm("Build mining facility?", _ => c.Build(gs, Structure.MiningFacility)), enabled: false),
                    new MenuOption("Enrichment facility", Common.Confirm("Build enrichment facility?", _ => c.Build(gs, Structure.EnrichmentFacility)), enabled: false),
                    new MenuOption("Ship yard", Common.Confirm("Build mining ship yard?", _ => c.Build(gs, Structure.ShipYard)), enabled: false),
                    Common.GoBackMenuOption(),
                }, mc => mc.SwapMenus(Build(gs, c)))
            {
                DataSectionTitle = "More information",
                DataSectionText = new[]
                {
                    "Settlements are housing for",
                    "non-essential personnel. They",
                    "attract settlers to your colony.",
                    "Not useful for unhabitable planets",
                    Common.FormatValue("construction cost", "$100"),
                    Common.FormatValue("operating cost", "$0"),
                    "",
                    "Water collection facilities extract",
                    "and purify h20 from the planet.",
                    Common.FormatValue("construction cost", "$200"),
                    Common.FormatValue("operating cost", "$20"),
                    "",
                    "Farms cultivate organics and are",
                    "dependent on water to operate.",
                    Common.FormatValue("construction cost", "$200"),
                    Common.FormatValue("operating cost", "$20"),
                    "",
                    "Mining Facilities extract and refine",
                    "metals from the planet.",
                    Common.FormatValue("construction cost", "$300"),
                    Common.FormatValue("operating cost", "$30"),
                    "",
                    "Enrichment facilities enrich uranium",
                    "for energy and are dependent on",
                    "metals to operate.",
                    Common.FormatValue("construction cost", "$1000"),
                    Common.FormatValue("operating cost", "$40"),
                    "",
                    "Ship yards allow you to construct",
                    "new ships for trade and exploration.",
                    Common.FormatValue("construction cost", "$2000"),
                    Common.FormatValue("operating cost", "$10"),
                }
            };

        public static Menu Structures(Colony c) =>
            new Menu("Menu", new[] {
                    new MenuOption("Raise Rate", mc => c.RaiseTaxRate()),
                    new MenuOption("lower Rate", mc => c.LowerTaxRate()),
                    Common.GoBackMenuOption(),
                }, mc => mc.SwapMenus(Structures(c)))
            {
                DataSectionTitle = "More information",
                DataSectionText = new[]
                {
                    Common.FormatValue("tax rate", c.TaxRate.ToString() + "pct")
                }
            };

        public static Menu Taxes(Colony c) =>
            new Menu("Menu", new[] {
                    new MenuOption("Raise Rate", mc => c.RaiseTaxRate()),
                    new MenuOption("lower Rate", mc => c.LowerTaxRate()),
                    Common.GoBackMenuOption(),
                }, mc => mc.SwapMenus(Taxes(c)))
            {
                DataSectionTitle = "More information",
                DataSectionText = new[]
                {
                    Common.FormatValue("tax rate", c.TaxRate.ToString() + "pct")
                }
            };
    }
}
