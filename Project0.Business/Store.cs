using System;
using System.Collections.Generic;

using Project0.Business.Behavior;

namespace Project0.Business {

    /// <summary>
    /// 
    /// </summary>
    public class Store : ISerialized {

        /// <summary>
        /// 
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The store's unique ID
        /// </summary>
        public ulong ID { get; set; }

        public void ShowProductStock () {

            Console.WriteLine ();

            foreach (var product in Products) {

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
