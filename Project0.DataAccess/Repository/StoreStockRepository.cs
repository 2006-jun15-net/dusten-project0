using System;
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

        public StoreStock FindStockedProductByName (Store store, string name) {

            using var context = new Project0Context (mOptions);

            StoreStock stockedProduct = default;

            try {

                var product = context.Product.Where (p => p.Name == name).First ();
                stockedProduct = store.StoreStock.Where (s => s.ProductId == product.Id).First ();

            } catch (Exception) {}

            return stockedProduct;
        }

        public void RemoveQuantity (StoreStock storeStock, int quantity) {

            using var context = new Project0Context (mOptions);

            context.StoreStock.Where (s => s.Id == storeStock.Id).First ().ProductQuantity -= quantity;
            context.SaveChanges ();
        }
    }
}
