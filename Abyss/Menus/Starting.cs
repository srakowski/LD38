using Abyss.MenuSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss.Menus
{
    public static class Starting
    {
        public static Menu MainMenu(GameState gs) =>
            new Menu("Menu", new[] {
                new MenuOption("Play", StartGame(gs, Faction.Red)),
                new MenuOption("Exit", mc => mc.PopMenu(), isCancel: true)
                })
            {
                DataSectionTitle = "Abyss",
                DataSectionText = new []
                {
                    "A dumb game",
                    "created for Ludum Dare 38",
                    "by Shawn Rakowski"
                }
            };

        public static Menu PlayerSelect(GameState gs) =>
            new Menu("Choose your faction:", new[] {
                new MenuOption("Red", StartGame(gs, Faction.Red), color: Color.Red),
                new MenuOption("White", StartGame(gs, Faction.White), color: Color.White),
                new MenuOption("Yellow", StartGame(gs, Faction.Yellow), color: Color.Yellow),
                new MenuOption("Cyan", StartGame(gs, Faction.Cyan), color: Color.Cyan),
                Common.GoBackMenuOption()
            })
            {
                DataSectionTitle = "no impact to gameplay"
            };


        private static Action<MenuControl> StartGame(GameState gs, Faction faction) =>
            mc => mc.PopMenu().PushMenu(Gameplay.Root(gs.Initialize(faction)));
    }
}
