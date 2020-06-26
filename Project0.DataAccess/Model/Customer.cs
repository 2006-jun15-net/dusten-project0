using System.Collections.Generic;

namespace Project0.DataAccess.Model
{
    public partial class Customer : IModel
    {
        public Customer()
        {
            CustomerOrder = new HashSet<CustomerOrder>();
        }

        public int Id { get; set; }
        public int? StoreId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }
    }

    public partial class Customer : IModel {

        public string Name {
            get => Firstname + " " + Lastname;
        }
    }
}
