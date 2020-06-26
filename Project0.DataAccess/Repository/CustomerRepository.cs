using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    public class CustomerRepository : Repository, IRepository<Customer> {

        public CustomerRepository (DbContextOptions<Project0Context> options) 
            : base (options) { }

        public List<Customer> FindById (int id) {

            using var context = new Project0Context(mOptions);
            return context.Customer.ToList ();

        }
    }
}
