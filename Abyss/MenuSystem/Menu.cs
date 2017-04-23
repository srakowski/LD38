using System.Collections.Generic;

namespace Abyss.MenuSystem
{
    public class Menu
    {
        public string Title { get; }
        public IEnumerable<MenuOption> Options { get; }
        public Menu(string text, IEnumerable<MenuOption> options)
        {
            Title = text;
            Options = options;
        }
    }
}
