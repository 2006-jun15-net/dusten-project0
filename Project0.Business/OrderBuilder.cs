using System;
using System.Collections.Generic;
using System.Linq;
using Project0.DataAccess.Model;
using Project0.DataAccess.Repository;

namespace Project0.Business {

    public class OrderBuilder {

        private const int MAX_PRODUCTS = 20;

        private readonly List<OrderLine> mOrderLines;
      
        public OrderBuilder () {
            mOrderLines = new List<OrderLine> ();
        }

        public void AddProduct (Store store, string productName, int quantity) {

            var stock = store.StoreStock.Where (s => s.Product.Name == productName).FirstOrDefault ();

            if (stock == default) {
                throw new BusinessLogicException ($"Couldn't find {productName} at {store.Name}");
            }

            if (stock.ProductQuantity < quantity) {
                throw new BusinessLogicException ($"Invalid quantity of {productName}");
            }

            if (OrderFullAfter (quantity)) {
                throw new BusinessLogicException ($"This order doesn't have enough space for {quantity} more item(s)");
            }

            stock.ProductQuantity -= quantity;

            mOrderLines.Add (new OrderLine {

                ProductId = stock.ProductId,
                ProductQuantity = quantity
            });
        }

        private bool OrderFullAfter (int quantity) {

            int netQuantity = quantity;

            foreach (var line in mOrderLines) {
                netQuantity += line.ProductQuantity;
            }

            return netQuantity >= MAX_PRODUCTS;
        }

        public CustomerOrder GetFinishedOrder (Customer customer, Store store, StoreStockRepository storeStockRepository) {

            storeStockRepository.SaveStoreStockQuantities (store.StoreStock);

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
