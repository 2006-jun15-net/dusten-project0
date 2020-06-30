using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    public class CustomerOrderRepository : Repository, IRepository<CustomerOrder> {

        public List<CustomerOrder> FindAll {
            
            get {

                using var context = new Project0Context (mOptions);
                return context.CustomerOrder.ToList ();
            }
        }

        public CustomerOrderRepository (DbContextOptions<Project0Context> options) 
            : base (options) { }

        public CustomerOrder FindById (int id) {

            using var context = new Project0Context (mOptions);
            return context.CustomerOrder.Where (o => o.Id == id).FirstOrDefault ();
        }

        public List<CustomerOrder> FindByCustomer (Customer customer) {

            using var context = new Project0Context (mOptions);

            return context.CustomerOrder.Where (
                o => o.CustomerId == customer.Id).ToList ();
        }

        public List<CustomerOrder> FindByStore (Store store) {

            using var context = new Project0Context (mOptions);  

            return context.CustomerOrder.Where (
                o => o.StoreId == store.Id).ToList ();
        }

        public void Add (CustomerOrder order) {

            using var context = new Project0Context (mOptions);
            
            context.CustomerOrder.Add (order);
            context.SaveChanges ();
        }
    }
}
