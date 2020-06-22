using Project0.Business.Database;

namespace Project0.Main {

    class Program {

        static void Main (string[] args) {

            var customerDb = new CustomerDatabase ();
            var orderDb = new OrderDatabase ();
            var storeDb = new StoreDatabase ();

            // TODO load from JSON file, if applicable

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
        }
    }
}
