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
}
