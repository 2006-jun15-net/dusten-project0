﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Project0.Business;
using Project0.DataAccess.Model;
using Project0.DataAccess.Repository;

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

            while (!Regex.Match(name, CustomerBuilder.NAME_REGEX).Success) {

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

                // 'name' should to have two values because of the Regex matching
                mCurrentCustomer = new CustomerBuilder ().Build (name);

                if (mCurrentCustomer == default) {
                    // TODO 
                }

                customerRepository.Add (mCurrentCustomer);
            }
        }

        /// <summary>
        /// Customer chooses which store they want to shop at
        /// (defaults to the last store they shopped at)
        /// </summary>
        /// <param name="storeRepository">Repository of stores</param>
        /// <param name="customerRepository">Repository of customer</param>
        internal void AcceptStoreChoice (StoreRepository storeRepository, CustomerRepository customerRepository) {

            var stores = storeRepository.FindAll;
            var storeNames = stores.Select (s => s.Name);

            Console.WriteLine ("\nStores:");

            foreach (var store in stores) {
                Console.WriteLine ($"\t{store.Id}: {store.Name}");
            }

            Store selection = default;
            string selectedName = default;

            while (!storeNames.Contains(selectedName)) {

                selectedName = UserSelection ();

                if (mCurrentCustomer.StoreId != null && selectedName.Trim () == "") {

                    mCurrentStore = mCurrentCustomer.Store;
                    Console.WriteLine ($"\nWelcome back to {mCurrentStore.Name}!");

                    return;
                }

                bool selectByID = int.TryParse (selectedName, out int selectedID);

                if (selectByID) {

                    try {
                        selection = storeRepository.FindById (selectedID);
                    } catch (Exception) { }

                    if (selection != default(Store)) {
                        break;
                    }
                }
            }

            if (selection == default(Store)) {
                selection = storeRepository.FindByName (selectedName);
            }

            mCurrentStore = selection;

            // Same store the customer visited previously
            if (mCurrentCustomer.StoreId == mCurrentStore.Id) {
                Console.WriteLine ($"\nWelcome back to {mCurrentStore.Name}!");
            }

            // Different store from previous visit
            else {

                Console.WriteLine ($"\nWelcome to {mCurrentStore.Name}!");
                customerRepository.UpdateStoreId (mCurrentCustomer.Id, mCurrentStore.Id);
            }
        }

        private string UserSelection () {

            // New customer
            if (mCurrentCustomer.StoreId == null) {
                Console.Write ($"\nPlease select a store: ");
            }

            // Customer has a previously visited store
            else {
                Console.Write ($"\nPlease select a store (default={mCurrentCustomer.StoreId}): ");
            }

            return Console.ReadLine ();
        }
        
        /// <summary>
        /// The customer can choose to list their current orders,
        /// start a new order, or quit the application
        /// </summary>
        /// <returns>An Option based on the user's input</returns>
        internal Option AcceptCustomerOption () {
            
            char input = default;
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
        internal void ListCustomerOrders (CustomerOrderRepository orderRepository) {

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
        internal void ListStoreOrders (CustomerOrderRepository orderRepository) {

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
        /// <param name="storeStockRepository">Repository of store stock</param>
        internal void NewCustomerOrder (CustomerOrderRepository orderRepository, StoreStockRepository storeStockRepository) {

            var orderBuilder = new OrderBuilder ();

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
                    orderBuilder.ShowInfo ();
                }

                else {

                    int quantity = 0;

                    while (quantity <= 0) {

                        Console.Write ("How many: ");
                        bool gotQuantity = int.TryParse (Console.ReadLine (), out quantity);

                        if (!gotQuantity) {
                            quantity = 0;
                        }
                    }

                    bool success = orderBuilder.AddProduct (mCurrentStore, storeStockRepository, input, quantity);

                    if (!success) {
                        continue;
                    }
                }
            }

            orderRepository.Add (orderBuilder.GetFinishedOrder (mCurrentCustomer, mCurrentStore));
        }
    }
}
