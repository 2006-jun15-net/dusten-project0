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
        /// <param name="customerDb">Database of customers</param>
        internal void AcceptCustomerName (CustomerDatabase customerDb) {

            Console.Write ("Please enter your name: ");

            var name = Console.ReadLine ();
            var nameReg = @"^[A-Z][a-z]+ [A-Z][a-z]+";

            while (!Regex.Match(name, nameReg).Success) {

                Console.Write ("Please enter in the form \"Firstname Lastname\": ");
                name = Console.ReadLine ();
            }

            var customerFromDb = customerDb.FindByName (name);

            if (customerFromDb != default(Customer)) {

                Console.WriteLine ($"\nWelcome back, {name}!");
                mCurrentCustomer = customerFromDb;
            }

            else {

                Console.WriteLine ($"\nWelcome, {name}!");

                // Guaranteed to have two values because of the Regex matching
                var firstlast = name.Split (" ");

                mCurrentCustomer = customerDb.AddCustomer (firstlast[0], firstlast[1]);
            }
        }

        /// <summary>
        /// Customer chooses which store they want to shop at
        /// (defaults to the last store they shopped at)
        /// </summary>
        /// <param name="storeDb">Database of stores</param>
        internal void AcceptStoreChoice (StoreDatabase storeDb) {

            var stores = storeDb.FindAll;
            var storeNames = new List<string> ();

            Console.WriteLine ("\nStores:\n");

            foreach (var store in stores) {

                Console.WriteLine ($"{store.ID}: {store.Name}");
                storeNames.Add (store.Name);
            }

            Console.Write ($"\nPlease select a store (default={mCurrentCustomer.StoreID}): ");
            var selectedName = Console.ReadLine ();

            if (selectedName.Trim () == "") {

                mCurrentStore = storeDb.FindByID (mCurrentCustomer.ID);
                return;
            }

            Store selection = default;

            while (!storeNames.Contains(selectedName)) {

                bool selectByID = ulong.TryParse (selectedName, out ulong selectedID);

                if (selectByID) {

                    try {
                        selection = storeDb.FindByID (selectedID);
                    } catch (Exception) { }

                    if (selection != default(Store)) {
                        break;
                    }
                }

                Console.Write ($"Please select a store (default={mCurrentCustomer.StoreID}): ");
                selectedName = Console.ReadLine ();
            }

            if (selection == default(Store)) {
                selection = storeDb.FindByName (selectedName);
            }

            mCurrentStore = selection;
            mCurrentCustomer.StoreID = mCurrentStore.ID;
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
                    Console.WriteLine("S/s: List alll orders placed at the current store");
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
        /// <param name="customerDb">Database of customers</param>
        /// <param name="orderDb">Database of customer orders</param>
        /// <param name="storeDb">Database of stores</param>
        internal void ListCustomerOrders (CustomerDatabase customerDb, OrderDatabase orderDb, StoreDatabase storeDb) {
            
            Console.WriteLine ();

            var orders = orderDb.FindByCustomer (mCurrentCustomer);
            ListOrders (orders, mCurrentCustomer.Name, customerDb, storeDb);
        }

        /// <summary>
        /// List all orders placed at the current store
        /// </summary>
        /// <param name="customerDb">Database of customers</param>
        /// <param name="orderDb">Database of customer orders</param>
        /// <param name="storeDb">Database of stores</param>
        internal void ListStoreOrders (CustomerDatabase customerDb, OrderDatabase orderDb, StoreDatabase storeDb) {
            
            Console.WriteLine ();

            var orders = orderDb.FindByStore (mCurrentStore);
            ListOrders (orders, mCurrentStore.Name, customerDb, storeDb);
        }

        private void ListOrders (List<Order> orders, string name, CustomerDatabase customerDb, StoreDatabase storeDb) {

            if (orders.Count == 0) {

                Console.WriteLine ($"{name} has no orders\n");
                return;
            }

            foreach (var order in orders) {
                order.ShowInfo (storeDb, customerDb);
            }

            Console.WriteLine ();
        }

        /// <summary>
        /// Allow the customer to create a new order by "shopping" for 
        /// products in the currently selected store
        /// </summary>
        /// <param name="orderDb">Database of customer orders</param>
        internal void NewCustomerOrder (OrderDatabase orderDb) {

            var order = new Order (mCurrentCustomer, mCurrentStore);

            mCurrentStore.ShowProductStock ();

            while (true) {

                Console.Write ("Please choose a product (q to quit, l to relist): ");
                var input = Console.ReadLine ().ToLower ();

                if (input == "q") {
                    break;
                }

                if (input == "l") {
                    mCurrentStore.ShowProductStock ();
                }

                else if (!mCurrentStore.HasProductInStock (input)) {
                    Console.WriteLine ($"Product {input} is out of stock");
                }

                else {

                    var productFromStore = mCurrentStore.GetProductByName (input);
                    int quantity;

                    Console.Write ("How many: ");

                    while (true) {

                        if (!int.TryParse(Console.ReadLine (), out quantity)) {

                            Console.Write("Please input a numeric value: ");
                            continue;
                        }

                        if (quantity > 0) {
                            
                            if (quantity > productFromStore.Quantity) {

                                Console.WriteLine ($"Invalid quantity of {productFromStore.Name}");
                                Console.Write ("How many: ");
                            }

                            else {
                                break;
                            }
                        }
                    }

                    var product = new Product(productFromStore, quantity);

                    productFromStore.Quantity -= quantity;
                    order.AddProduct (product);
                }
            }

            order.Finish ();
            orderDb.AddOrder (order);
        }
    }
}
