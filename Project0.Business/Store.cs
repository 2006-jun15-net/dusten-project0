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
    }
}
