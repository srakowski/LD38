﻿using Abyss.MenuSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss.Menus
{
    public static class Starting
    {
        public static Menu Main(GameState gs) =>
            new Menu("Abyss", new[] {
                new MenuOption("Play", mc => mc.PushMenu(PlayerSelect(gs))),
                new MenuOption("Exit", mc => mc.PopMenu(), isCancel: true)
                });

        public static Menu PlayerSelect(GameState gs) =>
            new Menu("Choose your faction:", new[] {
                new MenuOption("Red", StartGame(gs, Faction.Red), color: Color.Red),
                new MenuOption("Yellow", StartGame(gs, Faction.Yellow), color: Color.Yellow),
                new MenuOption("Cyan", StartGame(gs, Faction.Cyan), color: Color.Cyan),
                new MenuOption("White", StartGame(gs, Faction.White), color: Color.White),
                Common.GoBackMenuOption
            });

        private static Action<MenuControl> StartGame(GameState gs, Faction faction) =>
            mc => mc.PopMenu().PushMenu(Gameplay.Root(gs.Initialize(faction)));
    }
}
