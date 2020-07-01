using System;
using System.Collections.Generic;
using System.Linq;
using Project0.DataAccess.Model;
using Project0.DataAccess.Repository;

namespace Project0.Business {

    public class OrderBuilder {

        private const int MAX_PRODUCTS = 20;

        private readonly List<OrderLine> mOrderLines;
        // This is necessary due to issues with saving the finished CustomerOrder:
        private readonly List<Product> mProducts;
      
        public OrderBuilder () {

            mOrderLines = new List<OrderLine> ();
            mProducts = new List<Product> ();
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

            mProducts.Add (stock.Product);
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
                Timestamp = DateTime.Now,
                OrderLine = mOrderLines
            };
        }

        public void ShowInfo () {
            
            Console.WriteLine ();

            double orderTotal = 0.0;

            var zipped = mOrderLines.Zip (mProducts);

            foreach (var (line, product) in zipped) {

                double totalPrice = product.Price * line.ProductQuantity;
                orderTotal += totalPrice;

                Console.WriteLine ($"\t{product.Name} (${product.Price:0.00}) x {line.ProductQuantity}: ${totalPrice:0.00}");
            }

            Console.WriteLine ($"\n\tTotal: ${orderTotal:0.00}\n");
        }
    }
}
