using System.Linq;

namespace Project0.Business.Database {

    /// <summary>
    /// Repository containing all Product instances
    /// </summary>
    public class ProductRepository : Repository<Product> {

        public ProductRepository (string jsonFile) : base (jsonFile) { }

        /// <summary>
        /// Find a single product in the database with the given name
        /// </summary>
        /// <param name="productname">Name of the product</param>
        /// <returns>Store with matching name</returns>
        public Product FindByName (string productname) {

            var stores = from item in mItems
                         where item.Name == productname
                         select item;

            return stores.First ();
        }
    }
}
