using System;
using System.Collections.Generic;

namespace Project0.DataAccess.Model
{
    public partial class Product : IModel
    {
        public Product()
        {
            OrderLine = new HashSet<OrderLine>();
            StoreStock = new HashSet<StoreStock>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
        public virtual ICollection<StoreStock> StoreStock { get; set; }
    }
}
