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

        // TODO default store loc

        /// <summary>
        /// The customer's full name
        /// </summary>
        public string Name {
            get => mFirstname + " " + mLastname;
        }

        public Customer (string firstname, string lastname) {

            mFirstname = firstname;
            mLastname = lastname;
        }

        public void Serialize (string file) {
            throw new NotImplementedException ();
        }

        public void Deserialize (string file) {
            throw new NotImplementedException ();
        }
    }
}
