﻿using Project0.Business.Database;

namespace Project0.Main {

    class Program {

#pragma warning disable IDE0060 // Remove unused parameter
        static void Main (string[] args) {
#pragma warning restore IDE0060 // Remove unused parameter

            // JSON files are stored at the solution's parent directory
            // (this would need to change if running the program by itself)
            var productRepository = new ProductRepository ("../../../../products.json");
            productRepository.LoadItems ();

            var customerRepository = new CustomerRepository ("../../../../customers.json");
            var orderRepository = new OrderRepository ("../../../../orders.json", productRepository);
            var storeRepository = new StoreRepository ("../../../../stores.json", productRepository);

            customerRepository.LoadItems ();
            orderRepository.LoadItems ();
            storeRepository.LoadItems ();

            var handler = new IOHandler ();

            // Let the customer input their name 
            handler.AcceptCustomerName (customerRepository);

            // Let the customer choose which store to order from
            handler.AcceptStoreChoice (storeRepository);

            bool running = true;

            while (running) {

                // The customer can choose to see their orders,
                // see orders placed at the current store, 
                // create a new order, or quit the application
                switch (handler.AcceptCustomerOption ()) {

                    case IOHandler.Option.LIST_CUSTOMER_ORDERS:

                        handler.ListCustomerOrders (orderRepository, storeRepository);
                        break;

                    case IOHandler.Option.LIST_STORE_ORDERS:

                        handler.ListStoreOrders (orderRepository, customerRepository);
                        break;

                    case IOHandler.Option.NEW_ORDER:

                        handler.NewCustomerOrder (orderRepository);
                        break;

                    case IOHandler.Option.QUIT:
          
                        running = false;
                        break;

                    default:
                        break;
                }
            }

            customerRepository.SaveItems ();
            orderRepository.SaveItems ();
            storeRepository.SaveItems ();
        }
    }
}
