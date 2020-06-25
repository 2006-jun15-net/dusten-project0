using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Project0.Business.Database {

    /// <summary>
    /// Repository containing all Customers' information
    /// </summary>
    public class CustomerRepository : Repository<Customer> {

        private readonly StoreRepository mStoreRepository;

        public CustomerRepository (string jsonFile, StoreRepository storeRepository) : base (jsonFile) { 
            mStoreRepository = storeRepository;
        }

        public Customer AddCustomer(string firstname, string lastname) {

            mUuid += 1;

            var customer = new Customer {

                Firstname = firstname,
                Lastname = lastname,
                ID = mUuid
            };

            mItems.Add (customer);

            return customer;
        }

        /// <summary>
        /// Find customer given the customer's full name.
        /// This method assumes there is only a single customer
        /// for a given firstname lastname pair
        /// </summary>
        /// <param name="fullname">Full name of the customer</param>
        /// <returns>Customer with matching full name</returns>
        public Customer FindByName (string fullname) {

            var customers = from customer in mItems
                            where customer.Name == fullname
                            select customer;

            return customers.First ();
        }

        /// <summary>
        /// Find all customers with a given last name
        /// </summary>
        /// <param name="firstname">First name of the customer</param>
        /// <returns>Customers with matching first name</returns>
        public List<Customer> FindByFirstname (string firstname) {

            var customers = from customer in mItems
                            where customer.Firstname == firstname
                            select customer;

            return customers.ToList ();
        }

        /// <summary>
        /// Find all customers with a given first name
        /// </summary>
        /// <param name="lastname">Last name of the customer</param>
        /// <returns>Customers with matching last name</returns>
        public List<Customer> FindByLastname (string lastname) {
            
            var customers = from customer in mItems
                            where customer.Lastname == lastname
                            select customer;

            return customers.ToList ();
        }

        /// <summary>
        /// Deserializes all items from a JSON file
        /// </summary>
        protected override async void LoadItems () {

            string jsonText = await File.ReadAllTextAsync (mJsonFile);

            var items = JsonConvert.DeserializeObject<List<RawCustomerData>> (jsonText);
            mItems = new List<Customer> ();

            foreach (var item in items) {

                var store = mStoreRepository.FindByID (item.StoreID);

                mItems.Add (new Customer () {

                    Store = store,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    ID = item.ID
                });

                if (item.ID > mUuid) {
                    mUuid = item.ID;
                }
            }
        }

        /// <summary>
        /// Serializes all items and saves the resulting JSON text to a file
        /// </summary>
        public override async void SaveItems () {

            var items = new List<RawCustomerData> ();

            foreach (var item in mItems) {

                items.Add (new RawCustomerData () {

                    StoreID = item.Store.ID,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    ID = item.ID
                });
            }

            string jsonText = JsonConvert.SerializeObject (items);
            await File.WriteAllTextAsync (mJsonFile, jsonText);
        }

        private struct RawCustomerData {

            public string Firstname;
            public string Lastname;

            public ulong StoreID;
            public ulong ID;
        }
    }
}
