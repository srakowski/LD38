using System;
using System.Collections.Generic;
using System.Text;
using Abyss.MenuSystem;
using System.Linq;

namespace Abyss.Menus
{
    public static class Common
    {
        public static string FormatValue(string label, string value)
        {
            var formatted = label;
            value = value.Replace("-", "");
            var dotsToAdd = 36 - (value.Length + formatted.Length);
            for (int i = 0; i < dotsToAdd; i++)
                formatted += ".";
            return formatted + value;
        }

        public static Menu Todo() => new Menu("TODO", new[]
            {
                GoBackMenuOption()
            });

        public static MenuOption GoBackMenuOption(Action onGoBack = null)
            => new MenuOption("Go Back", mc => { onGoBack?.Invoke(); mc.PopMenu(); }, isCancel: true);

        public static MenuOption EmptyMenuOption => new MenuOption("Empty", mc => mc.PopMenu(), justText: true);

        internal static string FormatValue(string v, object p)
        {
            throw new NotImplementedException();
        }

        public static Action<MenuControl> Confirm(string prompt, Action<MenuControl> ifYes) =>
            mc => mc.PushMenu(new Menu(prompt, new[]
            {
                        new MenuOption("Yes", _ => ifYes(mc.PopMenu())),
                        new MenuOption("No", _ => mc.PopMenu()),
            }));

        public static Action<MenuControl> PushMenuFromEnumerable<T>(GameState gs, string title, IEnumerable<T> t, Func<GameState, T, Action<MenuControl>> onSelect, Func<T, bool> shouldJustText = null) where T : INamed =>
            mc => mc.PushMenu(MenuFromEnumerable(gs, title, t, onSelect, shouldJustText: shouldJustText));

        private static Menu MenuFromEnumerable<T>(GameState gs, string title, IEnumerable<T> t, Func<GameState, T, Action<MenuControl>> onSelect, Func<T, bool> shouldJustText = null) where T : INamed =>
            new Menu(title,
                (t.Any() ? t.Select(c => new MenuOption(c.Name, onSelect(gs, c), justText: shouldJustText?.Invoke(c) ?? false)) : new[] { Common.EmptyMenuOption })
                .Concat(new[] { Common.GoBackMenuOption() }));

    }
}
