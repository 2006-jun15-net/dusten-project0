using Project0.Business.Behavior;
using System;

namespace Project0.Business {

    public class Store : ISerialized {

        readonly int mID;

        /// <summary>
        /// The store's unique ID
        /// </summary>
        public int ID {
            get => mID;
        }

        public Store (int id) {
            mID = id;
        }
    }
}
