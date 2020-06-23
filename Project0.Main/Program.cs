using Project0.Business.Database;

namespace Project0.Main {

    class Program {

#pragma warning disable IDE0060 // Remove unused parameter
        static void Main (string[] args) {
#pragma warning restore IDE0060 // Remove unused parameter

            var customerDb = new CustomerDatabase ("../../../../customers.json");
            var orderDb = new OrderDatabase ("../../../../orders.json");
            var storeDb = new StoreDatabase ("../../../../stores.json");

            // JSON files are stored at the solution's parent directory
            // (this would need to change if running the program by itself)
            customerDb.LoadItems ();
            orderDb.LoadItems ();
            storeDb.LoadItems ();

            var handler = new IOHandler ();

            // Let the customer input their name 
            handler.AcceptCustomerName (customerDb);

            // Let the customer choose which store to order from
            handler.AcceptStoreChoice (storeDb);

            bool running = true;

            while (running) {

                // The customer can choose to see their orders,
                // see orders placed at the current store, 
                // create a new order, or quit the application
                switch (handler.AcceptCustomerOption ()) {

                    case IOHandler.Option.LIST_CUSTOMER_ORDERS:

                        handler.ListCustomerOrders (customerDb, orderDb, storeDb);
                        break;

                    case IOHandler.Option.LIST_STORE_ORDERS:

                        handler.ListStoreOrders (customerDb, orderDb, storeDb);
                        break;

                    case IOHandler.Option.NEW_ORDER:

                        handler.NewCustomerOrder (orderDb);
                        break;

                    case IOHandler.Option.QUIT:
          
                        running = false;
                        break;

                    default:
                        break;
                }
            }

            customerDb.SaveItems ();
            orderDb.SaveItems ();
            storeDb.SaveItems ();
        }
    }
}
