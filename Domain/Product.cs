using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.Domain
{
    internal class Product
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        [Column(TypeName = "nchar(8)")]
        private string skuNr;
        public required string SKU 
        { 
            get => skuNr; 
            set 
            {
                skuNr = value;
            }
        }

        [MaxLength(150)]
        public required string Description { get; set; }

        private string URL;
        public required string Picture 
        {
            get => URL; 
            set
            {
            URL = value;
            }
        }
        [Column(TypeName = "nchar(20)")]
        public required string Price { get; set; }

        public ICollection<Category> Categoies { get; set; } = new List<Category>();
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    }
}
