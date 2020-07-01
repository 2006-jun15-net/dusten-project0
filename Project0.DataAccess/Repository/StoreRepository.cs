using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    public class StoreRepository : Repository, IRepository<Store> {

        public List<Store> FindAll {
            
            get {

                using var context = new Project0Context (mOptions);
                return context.Store.ToList ();
            }
        }

        public StoreRepository (DbContextOptions<Project0Context> options) 
            : base (options) { }

        /// <summary>
        /// FOR UNIT TESTS ONLY!!!!
        /// </summary>
        public StoreRepository () { }

        public virtual Store FindById (int id) {

            using var context = new Project0Context (mOptions);

            return context.Store.Where (s => s.Id == id)
                .Include (s => s.StoreStock).ThenInclude (s => s.Product)
                .Include (s => s.CustomerOrder).FirstOrDefault ();
        }

        public virtual Store FindByName (string name) {

            using var context = new Project0Context (mOptions);

            return context.Store.Where (s => s.Name == name)
                .Include (s => s.StoreStock).ThenInclude (s => s.Product)
                .Include (s => s.CustomerOrder).FirstOrDefault ();
        }
    }
}