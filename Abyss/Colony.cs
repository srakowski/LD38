using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss
{
    public class Colony : INamed
    {
        public string Name { get; }

        public Colony(string name) => Name = name;
    }
}
