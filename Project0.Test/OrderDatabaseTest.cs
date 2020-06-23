using System;

using Xunit;

using Project0.Business.Database;
using Project0.Business;

namespace Project0.Test {

    public class OrderDatabaseTest {

        private readonly OrderDatabase mOrderDatabase;

        public OrderDatabaseTest () {

            mOrderDatabase = new OrderDatabase ("../../../../orders.json");
            mOrderDatabase.LoadItems ();
        }

        [Fact]
        public void TestLoadFromJsonFile () {

            var testOrder = mOrderDatabase.FindByID (0);

            // Test IDs
            Assert.Equal ((ulong)0, testOrder.ID);
            Assert.Equal ((ulong)0, testOrder.CustomerID);
            Assert.Equal ((ulong)0, testOrder.StoreID);

            // Test timestamp
            Assert.Equal (new DateTime (2020, 6, 23), testOrder.Timestamp);
        }

        [Fact]
        public void TestProductsLoaded () {

            var testOrder = mOrderDatabase.FindByID (0);
            var products = testOrder.Products;

            // Should be only one product
            Assert.Single (products);

            Assert.Equal ("Milk", products[0].Name);
            Assert.Equal (1.5, products[0].Price);
            Assert.Equal (2, products[0].Quantity);
        }

        [Fact]
        public void TestFindByStore () {

            var testOrders = mOrderDatabase.FindByStore (new Store () { ID = 0 });
            
            // Should only have one order for store 0
            Assert.Single (testOrders);

            // Test that the correct order with the correct store ID was found
            Assert.Equal((ulong)0, testOrders[0].ID);
            Assert.Equal((ulong)0, testOrders[0].StoreID);
        }

        [Fact]
        public void TestFindByCustomer () {

            var testOrders = mOrderDatabase.FindByCustomer (new Customer () { ID = 0 });
            
            // Should only have one order for store 0
            Assert.Single (testOrders);

            // Test that the correct order with the correct store ID was found
            Assert.Equal((ulong)0, testOrders[0].ID);
            Assert.Equal((ulong)0, testOrders[0].CustomerID);
        }
    }
}
