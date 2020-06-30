using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Project0.DataAccess;
using Project0.DataAccess.Model;

namespace Project0.Test.DataAccess.Repository {
    public class RepositoryTest {

        protected DbContextOptions<Project0Context> mOptions;

        public RepositoryTest () {

            ILoggerFactory MyLoggerFactory = LoggerFactory.Create (builder => { builder.AddConsole (); });

            string connectionString = ConnectionString.mConnectionString;

            mOptions = new DbContextOptionsBuilder<Project0Context> ()
                .UseLoggerFactory (MyLoggerFactory)
                .UseSqlServer (connectionString)
                .Options;
        }
    }
}
