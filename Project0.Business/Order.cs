using System;
using System.Collections.Generic;

using Project0.Business.Behavior;
using Project0.Business.Database;

namespace Project0.Business {

    /// <summary>
    /// 
    /// </summary>
    public class Order : ISerialized {

        private const int MAX_PRODUCTS = 20;

        private int mNetQuantity;

        /// <summary>
        /// 
        /// </summary>
        /// 
        // TODO Product quantities
        public List<Product> Products { get; set; }

        /// <summary>
        /// 
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

        public Order (Customer customer, Store store) {

            CustomerID = customer.ID;
            StoreID = store.ID;

            Products = new List<Product> ();
            mNetQuantity = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool AddProduct (Product product) {

            if (mNetQuantity + product.Quantity > MAX_PRODUCTS) {
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

        public string ToString (StoreDatabase storeDb, CustomerDatabase customerDb) {

            var store = storeDb.FindByID (StoreID);
            var customer = customerDb.FindByID (CustomerID);

            string output = $"Order #{ID} placed at {store.Name} by {customer.Name}:";

            foreach (var product in Products) {
                output += $"\t{product}";
            }

            return output;
        }
    }
}
