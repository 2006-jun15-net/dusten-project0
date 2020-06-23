using System;
using System.Collections.Generic;

using Project0.Business.Behavior;

namespace Project0.Business {

    /// <summary>
    /// Store where customers can purchase products from
    /// </summary>
    public class Store : ISerialized {

        /// <summary>
        /// The store's listed products
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// The store's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The store's unique ID
        /// </summary>
        public ulong ID { get; set; }

        /// <summary>
        /// Show products that are available for customer purchase
        /// </summary>
        public void ShowProductStock () {

            Console.WriteLine ();

            foreach (var product in Products) {

                // Out of stock
                if (product.Quantity == 0) {
                    continue;
                }

                Console.WriteLine (product);
            }

            Console.WriteLine ();
        }

        /// <summary>
        /// Check if the current product exists and is in stock
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <returns>True if the product is in stock</returns>
        public bool HasProductInStock (string name) {

            foreach (var product in Products) {

                if (product.Name == name) {
                    return product.Quantity > 0;
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
