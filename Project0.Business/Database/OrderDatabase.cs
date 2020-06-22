using System;
using System.Collections.Generic;

namespace Project0.Business.Database {

    /// <summary>
    /// Mock database containing all Order instances
    /// </summary>
    public class OrderDatabase : MockDatabase<Order> {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="store"></param>
        public void AddOrder (Customer customer, Store store, List<Product> products) {

            mUuid += 1;

            var order = new Order () {

                CustomerID = customer.ID,
                StoreID = store.ID,
                Products = products,
                ID = mUuid
            };

            mItems.Add (order);
        }

        /// <summary>
        /// Find all orders from a given customer
        /// </summary>
        /// <param name="customer">Customer who placed the order</param>
        /// <returns></returns>
        public List<Order> FindByCustomer (Customer customer) {

            var orders = new List<Order> ();

            foreach (var item in mItems) {

                if (item.CustomerID == customer.ID) {
                    orders.Add (item);
                }
            }

            return orders;
        }

        /// <summary>
        /// Find all orders from a given store
        /// </summary>
        /// <param name="store">Store where the order was placed</param>
        /// <returns></returns>
        public List<Order> FindByStore (Store store) {

            var orders = new List<Order> ();

            foreach (var item in mItems) {

                if (item.CustomerID == store.ID) {
                    orders.Add (item);
                }
            }

            return orders;
        }
    }
}
