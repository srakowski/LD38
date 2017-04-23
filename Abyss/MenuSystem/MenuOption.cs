using Microsoft.Xna.Framework;
using System;

namespace Abyss.MenuSystem
{
    public class MenuOption
    {
        public string Text { get; }
        public Color Color { get; }
        public Action<MenuControl> Action { get; }
        public MenuOption(string text, Action<MenuControl> action, Color? color = null)
        {
            this.Text = text;
            this.Action = action;
            this.Color = color ?? GameColors.Foreground;
        }
    }
}