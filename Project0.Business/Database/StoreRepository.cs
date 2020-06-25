using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Project0.Business.Database {

    /// <summary>
    /// Repository containing all Store instances
    /// </summary>
    public class StoreRepository : Repository<Store> {

        private readonly ProductRepository mProductRepository;

        public StoreRepository (string jsonFile, ProductRepository productRepository) : base (jsonFile) { 
            mProductRepository = productRepository;
        }

        /// <summary>
        /// Returns all stores in the database
        /// </summary>
        public List<Store> FindAll {
            get => mItems;
        }

        /// <summary>
        /// Find a single store in the database with the given name
        /// </summary>
        /// <param name="storename">Name of the store</param>
        /// <returns>Store with matching name</returns>
        public Store FindByName (string storename) {

            var stores = from item in mItems
                        where item.Name == storename
                        select item;

            return stores.First ();
        }

        /// <summary>
        /// Deserializes all items from a JSON file
        /// </summary>
        public override async void LoadItems () {

            string jsonText = await File.ReadAllTextAsync (mJsonFile);

            var items = JsonConvert.DeserializeObject<List<RawStoreData>> (jsonText);

            foreach (var item in items) {

                List<Product> products = item.Products.Select (
                    p => this.mProductRepository.FindByID (p)
                    ).ToList ();

                mItems.Add (new Store () {

                    Products = products,
                    Quantities = item.Quantities,
                    Name = item.Name,
                    ID = item.ID,
                });

                if (item.ID > mUuid) {
                    mUuid = item.ID;
                }
            }
        }

        /// <summary>
        /// Serializes all items and saves the resulting JSON text to a file
        /// </summary>
        public override async void SaveItems () {

            var items = new List<RawStoreData> ();

            foreach (var item in mItems) {

                List<ulong> rawProducts = item.Products.Select (p => p.ID).ToList ();

                items.Add (new RawStoreData () {

                    Products = rawProducts,
                    Quantities = item.Quantities,
                    Name = item.Name,
                    ID = item.ID
                });
            }

            string jsonText = JsonConvert.SerializeObject (items);
            await File.WriteAllTextAsync (mJsonFile, jsonText);
        }

        // Used for sanitizing data
        struct RawStoreData {

            public List<ulong> Products;
            public List<int> Quantities;

            public string Name;
            public ulong ID;
        }
    }
}
