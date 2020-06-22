using System;
using Xunit;

using Project0.Business.Database;

namespace Project0.Test {

    public class CustomerDatabaseTest {

        [Fact]
        public void TestLoadFromJsonFile () {
            
            var customerDb = new CustomerDatabase ();

            customerDb.LoadItems ("../../../../customers.json");

            var testCustomer = customerDb.FindByID (0);

            Assert.Equal ((ulong)0, testCustomer.StoreID);
            Assert.Equal ("John", testCustomer.Firstname);
            Assert.Equal ("Smith", testCustomer.Lastname);

            var testCustomerByName = customerDb.FindByName ("John Smith");

            Assert.Equal (testCustomer, testCustomerByName);
        }
    }
}
