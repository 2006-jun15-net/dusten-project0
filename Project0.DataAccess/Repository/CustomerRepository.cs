﻿using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    public class CustomerRepository : Repository, IRepository<Customer> {

        public List<Customer> FindAll {
            
            get {

                using var context = new Project0Context (mOptions);
                return context.Customer.ToList ();
            } 
        }

        public CustomerRepository (DbContextOptions<Project0Context> options) 
            : base (options) { }

        /// <summary>
        /// FOR UNIT TESTS ONLY!!!!
        /// </summary>
        public CustomerRepository () { }

        public virtual Customer FindById (int id) {

            using var context = new Project0Context (mOptions);
            return context.Customer.Where (c => c.Id == id).FirstOrDefault ();
        }

        public void Add (Customer customer) {

            using var context = new Project0Context (mOptions);

            context.Customer.Add (customer);
            context.SaveChanges ();
        }

        public void UpdateStoreId (int customerId, int storeId) {

            using var context = new Project0Context (mOptions);

            var exisitingCustomer = context.Customer.Where (c => c.Id == customerId).First ();
            exisitingCustomer.StoreId = storeId;

            context.SaveChanges ();
        }

        public void DeleteCustomer (Customer customer) {

            using var context = new Project0Context(mOptions);
            context.Customer.Remove (customer);
        }

        public virtual Customer FindByName (string name) {

            using var context = new Project0Context(mOptions);

            return context.Customer.Where (c => (c.Firstname + " " + c.Lastname) == name)
                .Include (c => c.Store).ThenInclude (s => s.CustomerOrder)
                .Include (c => c.Store).ThenInclude (s => s.StoreStock).ThenInclude (s => s.Product)
                .Include (c => c.CustomerOrder).FirstOrDefault ();
        }
    }
}
