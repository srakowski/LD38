using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss
{
    public class CargoBay
    {
        public ResourceType ResourceType { get; set; }
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; if (_quantity <= 0) ResourceType = ResourceType.Empty; }
        }

    }
}
