using System.Collections.Generic;

namespace Project0.Business.Database {

    /// <summary>
    /// Mock database containing all Customers' information
    /// </summary>
    public class CustomerDatabase : MockDatabase<Customer> {

        public CustomerDatabase (string jsonFile) : base (jsonFile) { }

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

            foreach (var item in mItems) {

                if (item.Name == fullname) {
                    return item;
                }
            }

            return default;
        }

        /// <summary>
        /// Find all customers with a given last name
        /// </summary>
        /// <param name="firstname">First name of the customer</param>
        /// <returns>Customers with matching first name</returns>
        public List<Customer> FindByFirstname (string firstname) {

            var customers = new List<Customer> ();

            foreach (var item in mItems) {

                if (item.Firstname == firstname) {
                    customers.Add (item);
                }
            }

            return customers;
        }

        /// <summary>
        /// Find all customers with a given first name
        /// </summary>
        /// <param name="lastname">Last name of the customer</param>
        /// <returns>Customers with matching last name</returns>
        public List<Customer> FindByLastname (string lastname) {
            
            var customers = new List<Customer> ();

            foreach (var item in mItems) {

                if (item.Lastname == lastname) {
                    customers.Add (item);
                }
            }

            return customers;
        }
    }
}
