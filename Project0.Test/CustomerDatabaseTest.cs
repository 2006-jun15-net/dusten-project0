using System.Collections.Generic;

using Xunit;

using Project0.Business;
using Project0.Business.Database;

namespace Project0.Test {

    public class CustomerDatabaseTest {

        private readonly CustomerRepository mCustomerDatabase;

        public CustomerDatabaseTest () {

            mCustomerDatabase = new CustomerRepository ("../../../../customers.json");
            mCustomerDatabase.LoadItems ();
        }

        [Fact]
        public void TestLoadFromJsonFile () {

            var testCustomer = mCustomerDatabase.FindByID (0);

            Assert.Equal ((ulong)0, testCustomer.StoreID);
            Assert.Equal ("John", testCustomer.Firstname);
            Assert.Equal ("Smith", testCustomer.Lastname);
        }

        [Fact]
        public void TestFindByName () {

            var testCustomer = mCustomerDatabase.FindByID (0);
            var testCustomerByName = mCustomerDatabase.FindByName ("John Smith");

            List<Customer> testCustomerByFirstname = mCustomerDatabase.FindByFirstname ("John");
            List<Customer> testCustomerByLastname = mCustomerDatabase.FindByLastname ("Smith");
            
            // Test list sizes
            Assert.Single (testCustomerByFirstname);
            Assert.Single (testCustomerByLastname);

            // Test that get by name(s) and get by ID correspond to the same customer
            Assert.Equal (testCustomer, testCustomerByName);
            Assert.Equal (testCustomer, testCustomerByFirstname[0]);
            Assert.Equal (testCustomer, testCustomerByLastname[0]);
        }
    }
}
