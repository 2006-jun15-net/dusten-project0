
namespace Project0.Business {

    /// <summary>
    /// Product that can be added to an order by a customer
    /// </summary>
    public class Product {

        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Quantity of the product (either at 
        /// the store or in a customer order)
        /// </summary>
        public int Quantity { get; set; }

        public Product () { }

        public Product (Product other, int quantity) {

            Name = other.Name;
            Price = other.Price;
            Quantity = quantity;
        }

        public override string ToString () {
            return $"{Name} - ${Price:#.00} (quamtity: {Quantity}";
        }
    }
}
