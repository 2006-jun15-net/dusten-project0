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

        /// <summary>
        /// The store's listed products
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product> ();

        /// <summary>
        /// Quantities of each product
        /// </summary>
        public List<int> Quantities { get; set; } = new List<int> ();

        /// <summary>
        /// The store's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The store's unique ID
        /// </summary>
        public ulong ID { get; set; }

        public Store () {}

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
        /// Find the quantity of the product with matching name
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <returns>Quantity of the product available in stock</returns>
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
        /// Removes a certain number of a product from stock
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <param name="quantity">Quantity to remove</param>
        internal void RemoveProduct (string name, int quantity) {
            
            for (int i = 0; i < Products.Count; i ++) {

                if (Products[i].Name == name) {

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
            return ProductQuantity (name) > 0;
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
