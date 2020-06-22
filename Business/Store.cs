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
        public Dictionary<Product, int> Quantities { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The store's unique ID
        /// </summary>
        public ulong ID { get; set; }
    }



    // TODO 'stock' of products to order from
}
