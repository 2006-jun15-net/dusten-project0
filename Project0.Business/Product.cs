using System;

namespace Project0.Business {

    /// <summary>
    /// 
    /// </summary>
    public class Product {

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }

        public override string ToString () {
            return $"{Name} - ${Price:#.00} (quamtity: {Quantity}";
        }
    }
}
