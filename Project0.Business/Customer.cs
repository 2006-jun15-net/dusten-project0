using Project0.Business.Behavior;

namespace Project0.Business {

    /// <summary>
    /// Customer able to place an order to a store.
    /// Has a default store to place orders at, unless
    /// otherwise specified
    /// </summary>
    public class Customer : ISerialized {

        /// <summary>
        /// Store that the customer is currently ordering from
        /// </summary>
        public Store Store { get; set; }

        /// <summary>
        /// The customer's first name
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// The customer's last name
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// The customer's full name
        /// </summary>
        /// 
        public string Name {
            get => Firstname + " " + Lastname;
        }

        /// <summary>
        /// The customer's unique ID
        /// </summary>
        public ulong ID { get; set; }
    }
}
