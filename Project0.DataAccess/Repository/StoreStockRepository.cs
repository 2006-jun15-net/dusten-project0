﻿using System;
using System.Linq;
using System.Collections.Generic;

using Project0.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Project0.DataAccess.Repository {
    public class StoreStockRepository : Repository, IRepository<StoreStock> {

        public List<StoreStock> FindAll {

            get {

                using var context = new Project0Context (mOptions);
                return context.StoreStock.ToList ();
            }
        }

        public StoreStockRepository (DbContextOptions<Project0Context> options) : base (options) {}

        public StoreStock FindById (int id) {
            
            using var context = new Project0Context (mOptions);
            return context.StoreStock.Where (s => s.Id == id).FirstOrDefault ();
        }

        public void SaveStoreStockQuantities (ICollection<StoreStock> storeStock) {

            using var context = new Project0Context (mOptions);
            
            foreach (var stock in storeStock) {
                context.StoreStock.Where (s => s.Id == stock.Id).First ().ProductQuantity = stock.ProductQuantity;
            }

            context.SaveChanges ();
        }
    }
}
