using Project0.DataAccess.Model;
using Project0.DataAccess.Repository;

using Xunit;

namespace Project0.Test.DataAccess.Repository {

    public class CustomerRepositoryTest : RepositoryTest {

        private readonly CustomerRepository mCustomerRepository;

        public CustomerRepositoryTest () : base () {
            mCustomerRepository = new CustomerRepository (mOptions);
        }

        [Fact]
        public void TestFindByName () {

            var customerByName = mCustomerRepository.FindByName ("Agent Smith");
            var customerById = mCustomerRepository.FindById (3);

            Assert.Equal (customerById.Id, customerByName.Id);
            Assert.Equal (customerByName.Name, customerById.Name);
        }

        [Fact]
        public void TestFindByFirstname () {

            var customerByFirstname = mCustomerRepository.FindByFirstname ("Agent");

            Assert.Single (customerByFirstname);
        }

        [Fact]
        public void TestFindByLastname () {

            var customerByLastname = mCustomerRepository.FindByLastname ("Smith");

            Assert.Equal (2, customerByLastname.Count);
        }


    }
}
