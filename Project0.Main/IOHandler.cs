using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Project0.Business.Database;
using Project0.Business;
using System.Net.NetworkInformation;

namespace Project0.Main {

    /// <summary>
    /// Handles the I/O and validation for interactions
    /// with the customer and the console
    /// </summary>
    internal class IOHandler {

        private Customer mCurrentCustomer;
        private Store mCurrentStore;

        internal enum Option {
            LIST_ORDERS, NEW_ORDER, QUIT
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerDb"></param>
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

                Console.WriteLine ($"Welcome back, {name}!");
                mCurrentCustomer = customerFromDb;
            }

            else {

                Console.WriteLine ($"Welcome, {name}!");

                // Guaranteed to have two values because of the Regex matching
                var firstlast = name.Split (" ");

                mCurrentCustomer = customerDb.AddCustomer (firstlast[0], firstlast[1]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeDb"></param>
        internal void AcceptStoreChoice (StoreDatabase storeDb) {

            // TODO load all stores, show selection with "ID: Name" format
            // TODO default choice (no input given) should result in the customer's saved StoreID

            var stores = storeDb.FindAll;
            var storeNames = new List<string> ();

            foreach (var store in stores) {

                Console.WriteLine ($"{store.ID}: {store.Name}");
                storeNames.Add (store.Name);
            }

            Console.Write ($"Please select a store (default={mCurrentCustomer.StoreID}): ");
            var selectedName = Console.ReadLine ();

            if (selectedName.Trim () == "") {

                mCurrentStore = storeDb.FindByID (mCurrentCustomer.ID);
                return;
            }

            Store selection = default;

            while (!storeNames.Contains(selectedName)) {

                try {
                    ulong selectedID = ulong.Parse (selectedName);

                    selection = storeDb.FindByID (selectedID);

                    if (selection != default(Store)) {
                        break;
                    }

                } catch (Exception) { }

                selectedName = Console.ReadLine ();
            }

            if (selection == default(Store)) {
                selection = storeDb.FindByName (selectedName);
            }

            mCurrentStore = selection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Option AcceptCustomerOption () {
            
            string input = "";
            var inputReg = @"^[lnq]$";

            while (!Regex.Match (input, inputReg).Success) {

                Console.Write ("Choose an option (h for help): ");
                input = Console.ReadLine ().ToLower ();

                if (input == "h") {
                    
                    Console.WriteLine("\nH/h: Show this help message");
                    Console.WriteLine("L/l: List all orders for current customer");
                    Console.WriteLine("N/n: Start a new order");
                    Console.WriteLine("Q/q: Quit\n");
                }
            }

            return input switch
            {
                "l" => Option.LIST_ORDERS,
                "n" => Option.NEW_ORDER,
                _ => Option.QUIT,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDb"></param>
        internal void ListCustomerOrders (OrderDatabase orderDb) {
            
            Console.WriteLine ();

            var orders = orderDb.FindByCustomer (mCurrentCustomer);

            if (orders.Count == 0) {

                Console.WriteLine ($"{mCurrentCustomer.Name} has no orders\n");
                return;
            }

            foreach (var order in orders) {
                Console.WriteLine ($"{order.ID}: {order}");
            }

            Console.WriteLine ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDb"></param>
        internal void NewCustomerOrder (OrderDatabase orderDb) {

            var products = new List<Product> ();

            // TODO allow customer to "shop"
            // TDOO list store options
            // TODO allow customer to choose product
            // TODO allow customer to say quantity
            // TODO repeat until done
            // TODO add to list of products as customer is "shopping"

            bool ordering = true;

            while (ordering) {

                mCurrentStore.ShowProductStock ();

                // TODO accept product name (or 'q' to quit shopping)
                // TODO accept quantity
                // TODO validate quantity
                // TODO add to list, remove quantity from store
            }

            orderDb.AddOrder (mCurrentCustomer, mCurrentStore, products);
        }
    }
}
