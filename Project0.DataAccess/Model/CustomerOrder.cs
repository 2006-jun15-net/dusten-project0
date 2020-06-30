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

    public partial class CustomerOrder : IModel {

        public const int MAX_PRODUCTS = 20;

        public void ShowInfoForStore () {

            Console.WriteLine ($"Order #{Id} placed at {Store.Name}:\n");

            ShowInfo ();
            Console.WriteLine ();
        }

        /// <summary>
        /// Print all info of the order from it's specified customer
        /// </summary>
        public void ShowInfoForCustomer () {

            Console.WriteLine ($"Order #{Id} placed by {Customer.Name}:\n");

            ShowInfo ();
            Console.WriteLine ();
        }

        /// <summary>
        /// Print all info for the order
        /// </summary>
        public void ShowInfo () {

            double total = 0.0;

            foreach (var line in OrderLine) {

                Console.WriteLine ($"{line.Product} ({line.ProductQuantity})");
                total += line.Product.Price * line.ProductQuantity;
            }

            Console.Write($"\n\tOrder total: ${total:#.00}");
        }
    }
}
