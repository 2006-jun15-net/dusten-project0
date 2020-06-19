using Project0.Business.Behavior;
using System;

namespace Project0.Business {

    public class Order : ISerialized {

        readonly int mID;

        public int ID {
            get => mID;
        }

        public Order (int id) {
            mID = id;
        }
    }
}
