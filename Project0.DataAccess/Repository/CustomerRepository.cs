using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    public class CustomerRepository : Repository, IRepository<Customer> {

        public CustomerRepository (DbContextOptions<Project0Context> options) 
            : base (options) { }

        public List<Customer> FindAll () {
            
            using var context = new Project0Context (mOptions);
            return context.Customer.ToList ();
        }

        public Customer FindById (int id) {

            using var context = new Project0Context (mOptions);
            return context.Customer.Where (c => c.Id == id).FirstOrDefault ();
        }

        public void AddCustomer (string firstname, string lastname) {

            var customer = new Customer {

                Firstname = firstname,
                Lastname = lastname
            };

            using var context = new Project0Context(mOptions);
            context.Customer.Add (customer);
        }

        public void DeleteCustomer (Customer customer) {

            using var context = new Project0Context(mOptions);
            context.Customer.Remove (customer);
        }

        public List<Customer> FindByFirstname (string firstname) {

            using var context = new Project0Context(mOptions);
            return context.Customer.Where (c => c.Firstname == firstname).ToList ();
        }

        public List<Customer> FindByLastname (string lastname) {

            using var context = new Project0Context(mOptions);
            return context.Customer.Where (c => c.Lastname == lastname).ToList ();
        }

        public List<Customer> FindByName (string name) {

            using var context = new Project0Context(mOptions);
            return context.Customer.Where (c => c.Name == name).ToList ();
        }
    }
}
