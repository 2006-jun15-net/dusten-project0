using Project0.Business.Database;

namespace Project0.Main {

    class Program {

        static void Main (string[] args) {

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

                // The customer can choose to see their active orders,
                // create a new order, or quit the application
                switch (handler.AcceptCustomerOption ()) {

                    case IOHandler.Option.LIST_ORDERS:

                        handler.ListCustomerOrders (orderDb);
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
