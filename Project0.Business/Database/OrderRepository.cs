using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Project0.Business.Database {

    /// <summary>
    /// Repository containing all Order instances
    /// </summary>
    public class OrderRepository : Repository<Order> {

        private readonly ProductRepository mProductRepository;

        public OrderRepository (string jsonFile, ProductRepository productRepository) : base (jsonFile) { 
            mProductRepository = productRepository;
        }

        /// <summary>
        /// Adds an order to the database
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder (Order order) {

            mUuid += 1;

            order.ID = mUuid;
            mItems.Add (order);
        }

        /// <summary>
        /// Find all orders from a given customer
        /// </summary>
        /// <param name="customer">Customer who placed the order</param>
        /// <returns></returns>
        public List<Order> FindByCustomer (Customer customer) {

            var orders = from item in mItems 
                        where item.CustomerID == customer.ID 
                        select item;

            return orders.ToList ();
        }

        /// <summary>
        /// Find all orders from a given store
        /// </summary>
        /// <param name="store">Store where the order was placed</param>
        /// <returns></returns>
        public List<Order> FindByStore (Store store) {

            var orders = from item in mItems 
                        where item.StoreID == store.ID 
                        select item;

            return orders.ToList ();
        }

        /// <summary>
        /// Deserializes all items from a JSON file
        /// </summary>
        public override async void LoadItems () {

            string jsonText = await File.ReadAllTextAsync (mJsonFile);

            var items = JsonConvert.DeserializeObject<List<RawOrderData>> (jsonText);
            mItems = new List<Order> ();

            foreach (var item in items) {

                List<Product> products = item.Products.Select (
                    p => mProductRepository.FindByID (p)
                    ).ToList ();

                mItems.Add (new Order () {

                    Products = products,

                    Quantities = item.Quantities,
                    Timestamp = item.Timestamp,

                    CustomerID = item.CustomerID,
                    StoreID = item.StoreID,
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

            var items = new List<RawOrderData> ();

            foreach (var item in mItems) {

                List<ulong> rawProducts = item.Products.Select (p => p.ID).ToList ();

                items.Add (new RawOrderData () {

                    Products = rawProducts,

                    Quantities = item.Quantities,
                    Timestamp = item.Timestamp,

                    CustomerID = item.CustomerID,
                    StoreID = item.StoreID,
                    ID = item.ID,
                });
            }

            string jsonText = JsonConvert.SerializeObject (items);
            await File.WriteAllTextAsync (mJsonFile, jsonText);
        }

        /// <summary>
        /// 
        /// </summary>
        private struct RawOrderData {

            public List<ulong> Products;
            public List<int> Quantities;

            public DateTime Timestamp;
            public ulong CustomerID;
            public ulong StoreID;
            public ulong ID;
        }
    }
}
