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
            set { _quantity = value; if (_quantity <= 0 && detype) ResourceType = ResourceType.Empty; }
        }

        private bool detype = true;

        public CargoBay() { }

        public CargoBay(ResourceType type, bool detype)
        {
            this.ResourceType = type;
            this.detype = detype;
        }
    }
}
