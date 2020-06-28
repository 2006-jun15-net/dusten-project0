using System;
using System.Collections.Generic;

using Project0.DataAccess.Model;
using Project0.DataAccess.Repository;

namespace Project0.Business {

    public class OrderBuilder {

        private const int MAX_PRODUCTS = 20;

        private readonly List<OrderLine> mOrderLines;
      
        public OrderBuilder () {
            mOrderLines = new List<OrderLine> ();
        }

        public bool AddProduct (Store store, StoreStockRepository storeStockRepository, string productName, int quantity) {

            // TODO print error messages

            var stock = storeStockRepository.FindStockedProductByName (store, productName);

            if (stock == default) {

                Console.WriteLine ("");
                return false;
            }

            if (stock.ProductQuantity < quantity) {
                return false;
            }

            if (OrderFullAfter (quantity)) {
                return false;
            }

            storeStockRepository.RemoveQuantity (stock, quantity);

            mOrderLines.Add (new OrderLine {

                ProductId = stock.ProductId,
                ProductQuantity = quantity
            });

            return true;
        }

        private bool OrderFullAfter (int quantity) {

            int netQuantity = quantity;

            foreach (var line in mOrderLines) {
                netQuantity += line.ProductQuantity;
            }

            return netQuantity >= MAX_PRODUCTS;
        }

        public CustomerOrder GetFinishedOrder (Customer customer, Store store) {

            return new CustomerOrder {

                CustomerId = customer.Id,
                StoreId = store.Id,
                OrderLine = mOrderLines
            };
        }

        public void ShowInfo () {
            
            Console.WriteLine ();

            double orderTotal = 0.0;

            foreach (var line in mOrderLines) {

                Console.WriteLine ($"\t{line}");
                orderTotal += line.Product.Price * line.ProductQuantity;
            }

            Console.WriteLine ($"\n\tTotal: {orderTotal:#.00}\n");
        }
    }
}
