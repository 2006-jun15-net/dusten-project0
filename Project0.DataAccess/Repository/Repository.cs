using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Project0.Business.Behavior;
using Project0.DataAccess.Model;

namespace Project0.DataAccess.Repository {

    public class Repository {

        protected readonly DbContextOptions<Project0Context> mOptions;

        public Repository (DbContextOptions<Project0Context> options) {
            mOptions = options;
        }
    }
}
