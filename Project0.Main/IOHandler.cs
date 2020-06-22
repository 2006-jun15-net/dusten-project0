using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Project0.Business.Database;
using Project0.Business;

namespace Project0.Main {

    /// <summary>
    /// Handles the I/O and validation for interactions
    /// with the customer and the console
    /// </summary>
    internal class IOHandler {

        Customer mCurrentCustomer;
        Store mCurrentStore;

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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Option AcceptCustomerOption () {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDb"></param>
        internal void ListCustomerOrders (OrderDatabase orderDb) {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDb"></param>
        internal void NewCustomerOrder (OrderDatabase orderDb) {
            throw new NotImplementedException ();
        }
    }
}
