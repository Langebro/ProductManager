using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Domain
{
    internal class Category
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }
    }
}
