using System;
using System.Collections.Generic;

using Project0.Business.Behavior;
using Project0.Business.Database;

namespace Project0.Business {

    /// <summary>
    /// Order placed to a store by a customer
    /// </summary>
    public class Order : ISerialized {

        /// <summary>
        /// Arbitrary maximum quantity of products per order
        /// </summary>
        private const int MAX_PRODUCTS = 20;

        private int mNetQuantity;

        /// <summary>
        /// Products added to the order by the customer
        /// </summary>
        /// 
        public List<Product> Products { get; set; }

        /// <summary>
        /// Time when the order was placed (set after customer finished adding products)
        /// </summary>
        public DateTime Timestamp {  get; set; }

        /// <summary>
        /// ID of the customer who placed the order
        /// </summary>
        public ulong CustomerID { get; set; }

        /// <summary>
        /// ID of the store the order is for
        /// </summary>
        public ulong StoreID { get; set; }

        /// <summary>
        /// The order's unique ID
        /// </summary>
        public ulong ID { get; set; }

        public Order () { 

            Products = new List<Product> ();
            mNetQuantity = 0;
        }

        public Order (Customer customer, Store store) : this () {

            CustomerID = customer.ID;
            StoreID = store.ID;
        }

        /// <summary>
        /// Adds a product to the order and checks whether or not the order 
        /// has too many products in it
        /// </summary>
        /// <param name="product"></param>
        /// <returns>False if the added products went over the order quantity limit</returns>
        public bool AddProduct (Product product) {

            mNetQuantity += product.Quantity;

            if (mNetQuantity > MAX_PRODUCTS) {
                return false;
            }

            Products.Add (product);

            return true;
        }

        /// <summary>
        /// FInalize the order w/ timestamp
        /// </summary>
        public void Finish () {
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Print all relevant info of this order
        /// </summary>
        /// <param name="storeDb">Database of stores</param>
        /// <param name="customerDb">Database of customers</param>
        public void ShowInfo (StoreDatabase storeDb, CustomerDatabase customerDb) {

            var store = storeDb.FindByID (StoreID);
            var customer = customerDb.FindByID (CustomerID);

            string output = $"Order #{ID} placed at {store.Name} by {customer.Name}:";

            double total = 0.0;

            for (int i = 0; i < Products.Count; i ++) {

                var product = Products[i];

                output += $"\n\t{i + 1}. {product}\n";
                total += product.Price * product.Quantity;
            }

            output += $"\n\tOrder total: {total:#.00}\n";

            Console.WriteLine (output);
        }
    }
}
