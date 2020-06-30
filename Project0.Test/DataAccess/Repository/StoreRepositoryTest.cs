using Xunit;
using Moq;

using Project0.DataAccess.Repository;
using System.Collections.Generic;
using Project0.DataAccess.Model;
using System.Linq;

namespace Project0.Test.DataAccess.Repository {

    public class StoreRepositoryTest {

        private readonly StoreRepository mStoreRepository;

        public StoreRepositoryTest () {

            var mockStoreRepo = new Mock<StoreRepository> ();

            List<Store> stores = new List<Store> () {

                new Store {

                    Id = 1,
                    Name = "Test"
                }
            };

            // Find by Id
            mockStoreRepo.Setup (
                repo => repo.FindById (It.IsAny<int> ()) 
            ).Returns (
                (int i) => stores.SingleOrDefault (s => s.Id == i)
            );

            // Find by name
            mockStoreRepo.Setup (
                repo => repo.FindByName (It.IsAny<string> ())
            ).Returns (
                (string name) => stores.SingleOrDefault (s => s.Name == name)
            );

            mockStoreRepo.SetupAllProperties ();

            mStoreRepository = mockStoreRepo.Object;
        }

        [Fact]
        public void TestFindByNameSuccess () {

            var storeByName = mStoreRepository.FindByName ("Test");
            Assert.NotSame (default(Store), storeByName);
        }

        [Fact]
        public void TestFindByNameFail () {

            var storeByName = mStoreRepository.FindByName ("Not a store");
            Assert.Same (default(Store), storeByName);
        }

        [Fact]
        public void TestFindByNameHasId () {

            var storeByName = mStoreRepository.FindByName ("Test");
            var storeById = mStoreRepository.FindById (1);

            Assert.Equal (storeByName.Name, storeById.Name);
            Assert.Equal (storeById.Id, storeByName.Id);
        }
    }
}
