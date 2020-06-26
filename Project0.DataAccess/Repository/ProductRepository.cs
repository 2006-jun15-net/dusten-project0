using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    public class ProductRepository : Repository, IRepository<Product> {

        public ProductRepository (DbContextOptions<Project0Context> options) 
            : base (options) { }

        public List<Product> FindById (int id) {

            using var context = new Project0Context(mOptions);
            return context.Product.ToList ();

        }
    }
}
