using System;
using Xunit;

using Project0.Business.Database;
using Project0.Business;

namespace Project0.Test {

    public class OrderDatabaseTest {

        [Fact]
        public void TestLoadFromJsonFile () {

            var orderDb = new OrderDatabase ("../../../../orders.json");

            orderDb.LoadItems ();

            var testOrder = orderDb.FindByID (0);

            // Test IDs
            Assert.Equal ((ulong)0, testOrder.ID);
            Assert.Equal ((ulong)0, testOrder.CustomerID);
            Assert.Equal ((ulong)0, testOrder.StoreID);

            Assert.Equal (new DateTime (2020, 6, 23), testOrder.Timestamp);
            Assert.Single (testOrder.Products);
        }
    }
}
