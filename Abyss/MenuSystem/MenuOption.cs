using Microsoft.Xna.Framework;
using System;

namespace Abyss.MenuSystem
{
    public class MenuOption
    {
        public string Text { get; }
        public Color Color { get; }
        public Action<MenuControl> Action { get; set; }
        public bool IsCancel { get; }
        public bool JustText { get; }
        public MenuOption(string text, Action<MenuControl> action, Color? color = null, bool isCancel = false, bool justText = false)
        {
            this.Text = text;
            this.Action = action;
            this.Color = color ?? GameColors.Foreground;
            this.IsCancel = isCancel;
            this.JustText = justText;
        }

        internal void Invoke(MenuControl mc)
        {
            if (!JustText) Action.Invoke(mc);
        }
    }
}