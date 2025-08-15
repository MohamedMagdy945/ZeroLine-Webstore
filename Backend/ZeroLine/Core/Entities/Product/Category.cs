﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroLine.Core.Entities.Product
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
