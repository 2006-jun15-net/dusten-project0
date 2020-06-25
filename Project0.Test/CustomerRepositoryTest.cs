using System.Collections.Generic;

using Xunit;

using Project0.Business;
using Project0.Business.Database;

namespace Project0.Test {

    public class CustomerRepositoryTest {

        private readonly CustomerRepository mCustomerRepository;

        public CustomerRepositoryTest () {

            var productRepository = new ProductRepository ("../../../../products.json");
            productRepository.LoadItems ();

            var storeRepository = new StoreRepository ("../../../../stores.json", productRepository);
            storeRepository.LoadItems ();

            mCustomerRepository = new CustomerRepository ("../../../../customers.json", storeRepository);
            mCustomerRepository.LoadItems ();
        }

        [Fact]
        public void TestLoadFromJsonFile () {

            var testCustomer = mCustomerRepository.FindByID (0);

            Assert.Equal ((ulong)0, testCustomer.Store.ID);
            Assert.Equal ("John", testCustomer.Firstname);
            Assert.Equal ("Smith", testCustomer.Lastname);
        }

        [Fact]
        public void TestFindByName () {

            var testCustomer = mCustomerRepository.FindByID (0);
            var testCustomerByName = mCustomerRepository.FindByName ("John Smith");

            List<Customer> testCustomerByFirstname = mCustomerRepository.FindByFirstname ("John");
            List<Customer> testCustomerByLastname = mCustomerRepository.FindByLastname ("Smith");
            
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
