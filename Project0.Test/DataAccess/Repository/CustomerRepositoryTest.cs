using Xunit;
using Moq;

using Project0.DataAccess.Repository;
using System.Collections.Generic;
using Project0.DataAccess.Model;
using System.Linq;

namespace Project0.Test.DataAccess.Repository {

    public class CustomerRepositoryTest {

        private readonly CustomerRepository mCustomerRepository;

        public CustomerRepositoryTest () {

            var mockCustomerRepo = new Mock<CustomerRepository> ();

            List<Customer> customers = new List<Customer> () {

                new Customer {

                    Firstname = "Test",
                    Lastname = "One",
                    Id = 1
                },

                new Customer {

                    Firstname = "Test",
                    Lastname = "Two",
                    Id = 2
                }
            };

            mockCustomerRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ())  
            ).Returns (
                (int id) => customers.Where (c => c.Id == id).FirstOrDefault ()
            );
            
            mockCustomerRepo.Setup (
                repo => repo.FindByName (It.IsAny<string> ())  
            ).Returns (
                (string name) => customers.Where (c => c.Firstname + " " + c.Lastname == name).FirstOrDefault ()
            );

            mCustomerRepository = mockCustomerRepo.Object;
        }

        [Fact]
        public void TestFindByNameSuccess () {

            var customerByName = mCustomerRepository.FindByName ("Test One");

            Assert.NotSame (default(Customer), customerByName);
            Assert.Equal ("Test One", customerByName.Name);
        }

        [Fact]
        public void TestFindByNameFail () {

            var customerByName = mCustomerRepository.FindByName ("Test Three");
            Assert.Same (default(Customer), customerByName);
        }

        [Fact]
        public void TestFindByNameHasId () {

            var customerByName = mCustomerRepository.FindByName ("Test Two");
            var customerById = mCustomerRepository.FindById (2);

            Assert.Equal (customerById.Id, customerByName.Id);
            Assert.Equal (customerByName.Name, customerById.Name);
        }
    }
}
