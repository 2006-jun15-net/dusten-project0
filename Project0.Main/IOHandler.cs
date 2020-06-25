using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Project0.Business;
using Project0.Business.Database;

namespace Project0.Main {

    /// <summary>
    /// Handles the I/O and validation for interactions
    /// with the customer and the console
    /// </summary>
    internal class IOHandler {

        private Customer mCurrentCustomer;
        private Store mCurrentStore;

        internal enum Option {
            LIST_CUSTOMER_ORDERS, LIST_STORE_ORDERS, NEW_ORDER, QUIT
        }

        /// <summary>
        /// Customer inputs their name so the system can attempt
        /// to find their existing records, or create new records
        /// if none already exist
        /// </summary>
        /// <param name="customerRepository">Repository of customers</param>
        internal void AcceptCustomerName (CustomerRepository customerRepository) {

            Console.Write ("Please enter your name: ");

            var name = Console.ReadLine ();
            var nameReg = @"^[A-Z][a-z]+ [A-Z][a-z]+";

            while (!Regex.Match(name, nameReg).Success) {

                Console.Write ("Please enter in the form \"Firstname Lastname\": ");
                name = Console.ReadLine ();
            }

            var customerFromDb = customerRepository.FindByName (name);

            // Customer already exists
            if (customerFromDb != default(Customer)) {

                Console.WriteLine ($"\nWelcome back, {name}!");
                mCurrentCustomer = customerFromDb;
            }

            // New cutomer
            else {

                Console.WriteLine ($"\nWelcome, {name}!");

                // Guaranteed to have two values because of the Regex matching
                var firstlast = name.Split (" ");
                mCurrentCustomer = customerRepository.AddCustomer (firstlast[0], firstlast[1]);
            }
        }

        /// <summary>
        /// Customer chooses which store they want to shop at
        /// (defaults to the last store they shopped at)
        /// </summary>
        /// <param name="storeRepository">Repository of stores</param>
        internal void AcceptStoreChoice (StoreRepository storeRepository) {

            var stores = storeRepository.FindAll;
            var storeNames = new List<string> ();

            Console.WriteLine ("\nStores:");

            foreach (var store in stores) {

                Console.WriteLine ($"\t{store.ID}: {store.Name}");
                storeNames.Add (store.Name);
            }

            Console.Write ($"\nPlease select a store (default={mCurrentCustomer.Store.ID}): ");
            var selectedName = Console.ReadLine ();

            if (selectedName.Trim () == "") {

                mCurrentStore = mCurrentCustomer.Store;
                Console.WriteLine ($"\nWelcome back to {mCurrentStore.Name}!");

                return;
            }

            Store selection = default;

            while (!storeNames.Contains(selectedName)) {

                bool selectByID = ulong.TryParse (selectedName, out ulong selectedID);

                if (selectByID) {

                    try {
                        selection = storeRepository.FindByID (selectedID);
                    } catch (Exception) { }

                    if (selection != default(Store)) {
                        break;
                    }
                }

                Console.Write ($"Please select a store (default={mCurrentCustomer.Store.ID}): ");
                selectedName = Console.ReadLine ();
            }

            if (selection == default(Store)) {
                selection = storeRepository.FindByName (selectedName);
            }

            mCurrentStore = selection;

            // Same store the customer visited previously
            if (mCurrentCustomer.Store.ID == mCurrentStore.ID) {
                Console.WriteLine ($"\nWelcome back to {mCurrentStore.Name}!");
            }

            // Different store from previous visit
            else {

                Console.WriteLine ($"\nWelcome to {mCurrentStore.Name}!");
                mCurrentCustomer.Store = mCurrentStore;
            }
        }

        /// <summary>
        /// The customer can choose to list their current orders,
        /// start a new order, or quit the application
        /// </summary>
        /// <returns>An Option based on the user's input</returns>
        internal Option AcceptCustomerOption () {
            
            char input = ' ';
            var inputReg = @"^[lsnq]";

            Console.WriteLine ();

            while (!Regex.Match (new string (input, 1), inputReg).Success) {

                Console.Write ("Choose an option (h for help): ");
                input = Console.ReadLine ().ToLower ().First ();

                if (input == 'h') {
                    
                    Console.WriteLine("\nH/h: Show this help message");
                    Console.WriteLine("L/l: List all orders for current customer");
                    Console.WriteLine("S/s: List all orders placed at the current store");
                    Console.WriteLine("N/n: Start a new order");
                    Console.WriteLine("Q/q: Quit\n");
                }
            }

            return input switch
            {
                'l' => Option.LIST_CUSTOMER_ORDERS,
                's' => Option.LIST_STORE_ORDERS,
                'n' => Option.NEW_ORDER,
                _ => Option.QUIT,
            };
        }

        /// <summary>
        /// List all orders for the current customer
        /// </summary>
        /// <param name="orderRepository">Repository of customer orders</param>
        internal void ListCustomerOrders (OrderRepository orderRepository) {

            var orders = orderRepository.FindByCustomer (mCurrentCustomer);

            Console.WriteLine ();

            if (orders.Count == 0) {

                Console.WriteLine ($"{mCurrentCustomer.Name} has no orders\n");
                return;
            }

            Console.WriteLine ($"Listing orders for customer {mCurrentCustomer.Name}:\n");

            foreach (var order in orders) {
                order.ShowInfoForStore ();
            }
        }

        /// <summary>
        /// List all orders placed at the current store
        /// </summary>
        /// <param name="orderRepository">Repository of customer orders</param>
        internal void ListStoreOrders (OrderRepository orderRepository) {

            var orders = orderRepository.FindByStore (mCurrentStore);

            Console.WriteLine ();

            if (orders.Count == 0) {

                Console.WriteLine ($"{mCurrentStore.Name} has no orders\n");
                return;
            }

            Console.WriteLine ($"Listing orders for store {mCurrentStore.Name}:\n");

            foreach (var order in orders) {
                order.ShowInfoForCustomer ();
            }
        }

        /// <summary>
        /// Allow the customer to create a new order by "shopping" for 
        /// products in the currently selected store
        /// </summary>
        /// <param name="orderRepository">Repository of customer orders</param>
        internal void NewCustomerOrder (OrderRepository orderRepository) {

            var order = new Order (mCurrentCustomer, mCurrentStore);

            mCurrentStore.ShowProductStock ();

            while (true) {

                Console.Write ("Please choose a product (f to finish, l to relist, p to preview): ");
                var input = Console.ReadLine ();

                if (input.ToLower () == "f") {
                    break;
                }

                if (input.ToLower () == "l") {
                    mCurrentStore.ShowProductStock ();
                }

                else if (input.ToLower () == "p") {

                    Console.WriteLine ();
                    order.ShowInfo ();
                    Console.WriteLine ();
                }

                else if (!mCurrentStore.HasProductInStock (input)) {
                    Console.WriteLine ($"Product {input} is out of stock");
                }

                else {

                    var productFromStore = mCurrentStore.GetProductByName (input);
                    int quantity;

                    while (true) {

                        Console.Write ("How many: ");

                        if (!int.TryParse(Console.ReadLine (), out quantity)) {

                            Console.Write("Please input a numeric value: ");
                            continue;
                        }

                        if (quantity > 0) {
                            
                            if (quantity > mCurrentStore.ProductQuantity (productFromStore.Name)) {

                                Console.WriteLine ($"Invalid quantity of {productFromStore.Name}");
                                Console.Write ("How many: ");

                                continue;
                            }

                            // Loop only ends when a valid quantity has been entered
                            // (0 < quantity < availble stock in store)
                            break;
                        }
                    }
                    
                    if (!order.AddProduct (mCurrentStore, input, quantity)) {
                        Console.WriteLine ($"Can't add more than {Order.MAX_PRODUCTS} products to an order");
                    }

                    if (order.ProductCount == Order.MAX_PRODUCTS) {

                        Console.WriteLine ("Order is now full!");
                        break;
                    }

                    Console.WriteLine ($"Order now has {order.ProductCount} products");
                }
            }

            order.Finish ();
            orderRepository.AddOrder (order);
        }
    }
}
