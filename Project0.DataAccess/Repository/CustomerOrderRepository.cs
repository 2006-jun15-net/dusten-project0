﻿using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    public class CustomerOrderRepository : Repository, IRepository<CustomerOrder> {

        public CustomerOrderRepository (DbContextOptions<Project0Context> options) 
            : base (options) { }

        public List<CustomerOrder> FindAll () {
            
            using var context = new Project0Context (mOptions);
            return context.CustomerOrder.ToList ();
        }

        public CustomerOrder FindById (int id) {

            using var context = new Project0Context (mOptions);
            return context.CustomerOrder.Where (o => o.Id == id).FirstOrDefault ();
        }

        public List<CustomerOrder> FindByCustomer (Customer customer) {

            using var context = new Project0Context (mOptions);  

            return context.CustomerOrder.Where (
                o => o.Customer == customer).ToList ();
        }

        public List<CustomerOrder> FindByStore (Store store) {

            using var context = new Project0Context (mOptions);  

            return context.CustomerOrder.Where (
                o => o.Store == store).ToList ();
        }
    }
}
