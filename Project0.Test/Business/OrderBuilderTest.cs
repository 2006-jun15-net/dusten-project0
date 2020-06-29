using Xunit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Project0.Business;
using Project0.DataAccess;
using Project0.DataAccess.Model;
using Project0.DataAccess.Repository;

namespace Project0.Test.Business {

    public class OrderBuilderTest {

        private readonly OrderBuilder mOrderBuilder;
        private readonly StoreStockRepository mStoreStockRepository;

        public OrderBuilderTest () {

            ILoggerFactory MyLoggerFactory = LoggerFactory.Create (builder => { builder.AddConsole (); });

            string connectionString = ConnectionString.mConnectionString;

            var options = new DbContextOptionsBuilder<Project0Context> ()
                .UseLoggerFactory (MyLoggerFactory)
                .UseSqlServer (connectionString)
                .Options;

            mOrderBuilder = new OrderBuilder ();
            mStoreStockRepository = new StoreStockRepository (options);
        }

        [Fact]
        public void TestAddProductSuccess () {

            var testStore = new Store {
                Id = 1
            };
            
            var testCustomer = new Customer {
                Id = 1
            };

            mOrderBuilder.AddProduct (testStore, mStoreStockRepository, "Milk", 2);
            
            var order = mOrderBuilder.GetFinishedOrder (testCustomer, testStore);

            Assert.Single(order.OrderLine);
        }

        [Fact]
        public void TestAddProductFailQuantity () {

            var testStore = new Store {
                Id = 1
            };

            // Quantity should not be allowed
            Assert.Throws<BusinessLogicException> (
                () => mOrderBuilder.AddProduct (testStore, mStoreStockRepository, "Milk", 21)
            );
        }

        [Fact]
        public void TestAddProductFailName () {

            var testStore = new Store {
                Id = 1
            };

            // Name should not exist
            Assert.Throws<BusinessLogicException> (
                () => mOrderBuilder.AddProduct (testStore, mStoreStockRepository, "Bacon", 2)
            );
        }
    }
}
