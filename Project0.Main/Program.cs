using Project0.Business;
using Project0.Business.Database;

namespace Project0.Main {

    class Program {

#pragma warning disable IDE0060 // Remove unused parameter
        static void Main (string[] args) {
#pragma warning restore IDE0060 // Remove unused parameter

            // JSON files are stored at the solution's parent directory
            // (this would need to change if running the program by itself)
            var productRepository = new ProductRepository ("../../../../products.json");
            productRepository.LoadItems (); // Unfortunately, 'LoadItems' always has to be called separately

            var storeRepository = new StoreRepository ("../../../../stores.json", productRepository);
            storeRepository.LoadItems ();

            var customerRepository = new CustomerRepository ("../../../../customers.json", storeRepository);
            customerRepository.LoadItems ();

            var orderRepository = new OrderRepository ("../../../../orders.json", productRepository, customerRepository, storeRepository);
            orderRepository.LoadItems ();

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

                        handler.ListCustomerOrders (orderRepository);
                        break;

                    case IOHandler.Option.LIST_STORE_ORDERS:

                        handler.ListStoreOrders (orderRepository);
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

            // Products don't need ot be saved, because product information
            // doesn't change during this program's runtime
            customerRepository.SaveItems ();
            orderRepository.SaveItems ();
            storeRepository.SaveItems ();
        }
    }
}
