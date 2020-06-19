using System.Collections.Generic;

namespace Project0.Business.Database {

    /// <summary>
    /// Mock database containing all Customers' information
    /// </summary>
    public class CustomerDatabase : MockDatabase<Customer> {

        /// <summary>
        /// Find customer given the customer's full name.
        /// This method assumes there is only a single customer
        /// for a given firstname lastname pair
        /// </summary>
        /// <param name="fullname"></param>
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
        /// <param name="firstname"></param>
        /// <returns>Customers with matching first name</returns>
        public List<Customer> FindByFirstname (string firstname) {

            List<Customer> customers = new List<Customer> ();

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
        /// <param name="lastname"></param>
        /// <returns>Customers with matching last name</returns>
        public List<Customer> FindByLastname (string lastname) {
            
            List<Customer> customers = new List<Customer> ();

            foreach (var item in mItems) {

                if (item.Lastname == lastname) {
                    customers.Add (item);
                }
            }

            return customers;
        }
    }
}
