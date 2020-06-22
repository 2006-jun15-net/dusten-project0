using System;
using System.Collections.Generic;

using Project0.Business.Behavior;

namespace Project0.Business {

    /// <summary>
    /// 
    /// </summary>
    public class Order : ISerialized {

        private const int MAX_PRODUCTS = 20;

        /// <summary>
        /// 
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Timestamp {  get; set; }

        /// <summary>
        /// ID of the customer who placed the order
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// ID of the store the order is for
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// The order's unique ID
        /// </summary>
        public ulong ID { get; set; }

        public Order (List<Product> products) {

            Products = products;
            Timestamp = DateTime.Now.ToString ("MM/dd/yyyy | hh:mm");
        }

        // TODO product quantity logic
    }
}
