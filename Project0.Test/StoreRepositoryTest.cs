using Xunit;

using Project0.Business.Database;

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

            var testStore = mStoreRepository.FindByID (0);

            Assert.Equal ((ulong)0, testStore.ID);
            Assert.Equal ("Milk and Cheese", testStore.Name);
        }

        [Fact]
        public void TestProductsLoaded () {

            var testStore = mStoreRepository.FindByID (0);
            var products = testStore.Products;

            Assert.Equal (2, products.Count);
            
            Assert.Equal ("Milk", products[0].Name);
            Assert.Equal (1.5, products[0].Price);
            Assert.Equal (50, testStore.ProductQuantity (products[0].Name));

            Assert.Equal ("Cheese", products[1].Name);
            Assert.Equal (2.0, products[1].Price);
            Assert.Equal (50, testStore.ProductQuantity (products[1].Name));
        }
    }
}
