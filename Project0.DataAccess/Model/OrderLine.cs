using System;
using System.Collections.Generic;

namespace Project0.DataAccess.Model
{
    public partial class OrderLine : IModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }

        public virtual CustomerOrder Order { get; set; }
        public virtual Product Product { get; set; }
    }

    public partial class OrderLine : IModel {

        public override string ToString () {

            double totalPrice = Product.Price * ProductQuantity;

            return $"{Product.Name} (${Product.Price:0.00}) x {ProductQuantity}: ${totalPrice:0.00}";
        }
    }
}
