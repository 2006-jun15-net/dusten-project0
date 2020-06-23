using Xunit;

using Project0.Business.Database;

namespace Project0.Test {
    public class StoreDatabaseTest {

        private readonly StoreDatabase mStoreDatabase;

        public StoreDatabaseTest () {

            mStoreDatabase = new StoreDatabase ("../../../../stores.json");
            mStoreDatabase.LoadItems ();
        }

        [Fact]
        public void TestLoadFromJsonFile () {

            var testStore = mStoreDatabase.FindByID (0);

            Assert.Equal ((ulong)0, testStore.ID);
            Assert.Equal ("Milk and Cheese", testStore.Name);
        }

        [Fact]
        public void TestProductsLoaded () {

            var testStore = mStoreDatabase.FindByID (0);
            var products = testStore.Products;

            Assert.Equal (2, products.Count);
            
            Assert.Equal ("Milk", products[0].Name);
            Assert.Equal (1.5, products[0].Price);
            Assert.Equal (50, products[0].Quantity);

            Assert.Equal ("Cheese", products[1].Name);
            Assert.Equal (2.0, products[1].Price);
            Assert.Equal (50, products[1].Quantity);
        }
    }
}
