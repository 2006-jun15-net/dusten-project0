using Project0.DataAccess.Repository;
using Xunit;

namespace Project0.Test.DataAccess.Repository {

    public class StoreRepositoryTest : RepositoryTest {

        private readonly StoreRepository mStoreRepository;

        public StoreRepositoryTest () : base () {
            mStoreRepository = new StoreRepository (mOptions);
        }

        [Fact]
        public void TestFindByName () {

            var storeByName = mStoreRepository.FindByName ("Milk and Cheese");
            var storeById = mStoreRepository.FindById (1);

            Assert.Equal (storeByName.Name, storeById.Name);
            Assert.Equal (storeById.Id, storeByName.Id);
        }
    }
}
