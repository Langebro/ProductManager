using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Domain
{
    internal class Product
    {
        public required string Name { get; set; }

        public required string SKU { get; set; }

        public required string Description { get; set; }

        public required string Picture { get; set; }

        public required string Price { get; set; } 

    }
}
