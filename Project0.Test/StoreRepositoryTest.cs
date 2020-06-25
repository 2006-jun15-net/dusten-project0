using Xunit;

using Project0.Business.Database;
using Project0.Business;

namespace Project0.Test {

    public class StoreRepositoryTest {

        private readonly StoreRepository mStoreRepository;

        public StoreRepositoryTest () {

            var productRepository = new ProductRepository ("../../../../products.json");
            productRepository.LoadItems ();

            mStoreRepository = new StoreRepository ("../../../../stores.json", productRepository);
            mStoreRepository.LoadItems ();
        }

        [Fact]
        public void TestLoadFromJsonFile () {

            var testStore = mStoreRepository.FindByID (1);

            Assert.Equal ((ulong)1, testStore.ID);
            Assert.Equal ("Milk and Cheese", testStore.Name);
        }

        [Fact]
        public void TestProductsLoaded () {

            var testStore = mStoreRepository.FindByID (1);
            var products = testStore.Products;
            var quantities = testStore.Quantities;

            // Should be only two products
            Assert.Equal (2, products.Count);
            Assert.Equal (2, quantities.Count);
            
            // Test IDs
            Assert.Equal ((ulong)1, products[0].ID);
            Assert.Equal ((ulong)2, products[1].ID);

            // Test quantities
            Assert.Equal (50, quantities[0]);
            Assert.Equal (50, quantities[1]);
        }
    }
}
