using Xunit;

using Project0.Business.Database;

namespace Project0.Test {
    public class StoreDatabaseTest {

        [Fact]
        public void TestLoadFromJsonFile () {

            var storeDb = new StoreDatabase ("../../../../stores.json");

            storeDb.LoadItems ();

            var testStore = storeDb.FindByID (0);

            Assert.Equal ((ulong)0, testStore.ID);
            Assert.Equal ("Milk and Cheese", testStore.Name);
            Assert.Equal (2, testStore.Products.Count);
        }
    }
}
