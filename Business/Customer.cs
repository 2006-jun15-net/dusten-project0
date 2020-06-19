using Project0.Business.Behavior;
using System;
using System.IO;

namespace Project0.Business {

    /// <summary>
    /// Customer able to place an order to a store.
    /// Has a default store to place orders at, unless
    /// otherwise specified
    /// </summary>
    public class Customer : ISerialized {

        readonly string mFirstname;
        readonly string mLastname;

        readonly int mID;

        int mStoreID;

        /// <summary>
        /// The customer's first name
        /// </summary>
        public string Firstname {
            get => mFirstname;
        }

        /// <summary>
        /// The customer's last name
        /// </summary>
        public string Lastname {
            get => mLastname;
        }

        /// <summary>
        /// The customer's full name
        /// </summary>
        public string Name {
            get => mFirstname + " " + mLastname;
        }

        /// <summary>
        /// The customer's unique ID
        /// </summary>
        public int ID {
            get => mID;
        }

        /// <summary>
        /// Store ID that the customer is currently ordering from
        /// </summary>
        public int StoreID {

            get => mStoreID;
            set => mStoreID = value;
        }

        public Customer (string firstname, string lastname, int id) {

            mFirstname = firstname;
            mLastname = lastname;

            mID = id;
        }
    }
}
