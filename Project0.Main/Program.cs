using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Project0.DataAccess;
using Project0.DataAccess.Model;
using Project0.DataAccess.Repository;

namespace Project0.Main {

    class Program {

#pragma warning disable IDE0060 // Remove unused parameter
        static void Main (string[] args) {
#pragma warning restore IDE0060 // Remove unused parameter

            ILoggerFactory MyLoggerFactory = LoggerFactory.Create (builder => { builder.AddConsole (); });

            string connectionString = ConnectionString.mConnectionString;

            DbContextOptions<Project0Context> options = new DbContextOptionsBuilder<Project0Context> ()
                .UseLoggerFactory (MyLoggerFactory)
                .UseSqlServer (connectionString)
                .Options;

            var handler = new IOHandler ();

            var storeRepository = new StoreRepository (options);
            var customerRepository = new CustomerRepository (options);
            var orderRepository = new CustomerOrderRepository (options);
            var stockRepository = new StoreStockRepository (options);

            // Let the customer input their name 
            handler.AcceptCustomerName (customerRepository);

            // Let the customer choose which store to order from
            handler.AcceptStoreChoice (storeRepository, customerRepository);

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

                        handler.NewCustomerOrder (orderRepository, stockRepository);
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
