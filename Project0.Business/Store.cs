using System;
using System.Linq;
using System.Collections.Generic;

using Project0.Business.Behavior;
using Project0.Business.Database;

namespace Project0.Business {

    /// <summary>
    /// Store where customers can purchase products from
    /// </summary>
    public class Store : ISerialized {

        // TODO dictionaries?

        /// <summary>
        /// The store's listed products
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Quantities of each product
        /// </summary>
        public List<int> Quantities { get; set; }

        // END TODO

        /// <summary>
        /// The store's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The store's unique ID
        /// </summary>
        public ulong ID { get; set; }

        public Store () {

            Products = new List<Product> ();
            Quantities = new List<int> ();
        }

        /// <summary>
        /// Show products that are available for customer purchase
        /// </summary>
        public void ShowProductStock () {

            Console.WriteLine ();

            var zipped = Products.Zip (Quantities);

            foreach (var (product, quantity) in zipped) {

                // Out of stock
                if (quantity == 0) {
                    continue;
                }

                Console.WriteLine (product);
            }

            Console.WriteLine ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int ProductQuantity (string name) {

            var zipped = Products.Zip (Quantities);

            foreach (var (product, quantity) in zipped) {

                if (product.Name == name) {
                    return quantity;
                }
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="quantity"></param>
        internal void RemoveProduct (string productName, int quantity) {
            
            for (int i = 0; i < Products.Count; i ++) {

                if (Products[i].Name == productName) {

                    Quantities[i] -= quantity;
                    break;
                }
            }
        }

        /// <summary>
        /// Check if the current product exists and is in stock
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <returns>True if the product is in stock</returns>
        public bool HasProductInStock (string name) {

            var zipped = Products.Zip (Quantities);

            foreach (var (product, quantity) in zipped) {

                if (product.Name == name) {
                    return quantity > 0;
                }
            }

            return false;
        }

        /// <summary>
        /// Find a listed product by name
        /// </summary>
        /// <param name="name">The name of the products</param>
        /// <returns>Product with the given name</returns>
        public Product GetProductByName (string name) {
            
            foreach (var product in Products) {

                if (product.Name == name) {
                    return product;
                }
            }

            return default;
        }
    }
}
