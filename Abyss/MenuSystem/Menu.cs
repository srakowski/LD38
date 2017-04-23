using System;
using System.Collections.Generic;

namespace Abyss.MenuSystem
{
    public class Menu
    {
        public string Title { get; }

        public IEnumerable<MenuOption> Options { get; }

        public Action<MenuControl> Update { get; }

        public Menu(string text, IEnumerable<MenuOption> options, Action<MenuControl> update = null)
        {
            Title = text;
            Options = options;
            Update = update;
        }

        public string DataSectionTitle { get; set; } = null;
        public IEnumerable<string> DataSectionText { get; set; }
    }
}
