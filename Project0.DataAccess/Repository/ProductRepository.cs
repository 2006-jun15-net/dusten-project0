using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    public class ProductRepository : Repository, IRepository<Product> {

        public ProductRepository (DbContextOptions<Project0Context> options) 
            : base (options) { }

        public List<Product> FindAll () {
            
            using var context = new Project0Context (mOptions);
            return context.Product.ToList ();
        }

        public Product FindById (int id) {

            using var context = new Project0Context (mOptions);
            return context.Product.Where (p => p.Id == id).FirstOrDefault ();
        }

        public Product FindByName (string name) {

            using var context = new Project0Context(mOptions);
            return context.Product.Where (p => p.Name == name).FirstOrDefault ();
        }
    }
}
