using System.Linq;
using System.Collections.Generic;

namespace Project0.Business.Database {

    /// <summary>
    /// Repository containing all Customers' information
    /// </summary>
    public class CustomerRepository : Repository<Customer> {

        public CustomerRepository (string jsonFile) : base (jsonFile) { }

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
    }
}
