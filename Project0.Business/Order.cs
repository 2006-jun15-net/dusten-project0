using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        public const int MAX_PRODUCTS = 20;

        /// <summary>
        /// Products added to the order by the customer
        /// </summary>
        /// 
        public List<Product> Products { get; set; }

        /// <summary>
        /// Quantity of each product in the order
        /// </summary>
        public List<int> Quantities { get; set; }

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

        public int ProductCount {
            get => Quantities.Sum (); 
        }

        public Order () { 

            Products = new List<Product> ();
            Quantities = new List<int> ();
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
        public bool AddProduct (Store store, string productName, int quantity) {

            // TODO FIX

            if (ProductCount + quantity > MAX_PRODUCTS) {
                return false;
            }

            store.RemoveProduct (productName, quantity);

            Quantities.Add (quantity);
            Products.Add (store.GetProductByName (productName));

            return true;
        }

        /// <summary>
        /// FInalize the order w/ timestamp
        /// </summary>
        public void Finish () {
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Print all info of an order from a specific customer
        /// </summary>
        /// <param name="customerName">Customer's name</param>
        /// <param name="storeDb">Database of stores</param>
        public void ShowInfoForStore (StoreRepository storeDb) {

            var store = storeDb.FindByID (StoreID);
            Console.WriteLine ($"Order #{ID} placed at {store.Name}:\n");

            ShowInfo (store);
            Console.WriteLine ();
        }

        public void ShowInfoForCustomer (CustomerRepository customerDb, Store store) {

            var customer = customerDb.FindByID (CustomerID);
            Console.WriteLine ($"Order #{ID} placed by {customer.Name}:\n");

            ShowInfo (store);
            Console.WriteLine ();
        }

        public void ShowInfo (Store store) {

            double total = 0.0;

            for (int i = 0; i < Products.Count; i ++) {

                var product = Products[i];
                Console.WriteLine ($"\t{i + 1}. {product}");

                total += product.Price * store.ProductQuantity (product.Name);
            }

            Console.Write($"\n\tOrder total: ${total:#.00}");
        }
    }
}
