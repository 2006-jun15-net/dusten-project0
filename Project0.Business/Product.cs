
using Project0.Business.Behavior;

namespace Project0.Business {

    /// <summary>
    /// Product that can be added to an order by a customer
    /// </summary>
    public class Product : ISerialized {

        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The product's unique ID
        /// </summary>
        public ulong ID { get; set; }

        public override string ToString () {
            return $"{Name} - ${Price:#.00}";
        }
    }
}
