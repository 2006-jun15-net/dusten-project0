using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    public class CustomerOrderRepository : Repository, IRepository<CustomerOrder> {

        public CustomerOrderRepository (DbContextOptions<Project0Context> options) 
            : base (options) { }

        public List<CustomerOrder> FindById (int id) {

            using var context = new Project0Context(mOptions);
            return context.CustomerOrder.ToList ();

        }
    }
}
