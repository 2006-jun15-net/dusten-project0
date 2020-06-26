using System;
using System.Collections.Generic;

namespace Project0.DataAccess.Model
{
    public partial class CustomerOrder : IModel
    {
        public CustomerOrder()
        {
            OrderLine = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
