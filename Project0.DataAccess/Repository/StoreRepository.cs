﻿using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    public class StoreRepository : Repository, IRepository<Store> {

        public StoreRepository (DbContextOptions<Project0Context> options) 
            : base (options) { }

        public List<Store> FindById (int id) {

            using var context = new Project0Context(mOptions);
            return context.Store.ToList ();

        }
    }
}