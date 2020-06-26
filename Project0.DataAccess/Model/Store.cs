using System;
using System.Collections.Generic;

namespace Project0.DataAccess.Model
{
    public partial class Store : IModel
    {
        public Store()
        {
            Customer = new HashSet<Customer>();
            CustomerOrder = new HashSet<CustomerOrder>();
            StoreStock = new HashSet<StoreStock>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }
        public virtual ICollection<StoreStock> StoreStock { get; set; }
    }
}
