using Xunit;

using Project0.Business;

namespace Project0.Test {

    public class StoreTest {

        private readonly Store mStore;

        public StoreTest () {

            // Test0 is in stock, Test1 is out of stock
            mStore = new Store () {
                Quantities = {1, 0}
            };

            mStore.Products.Add (new Product () {
                Name = "Test0"
            });

            mStore.Products.Add (new Product () {
                Name = "Test1"
            });
        }

        [Fact]
        public void TestProductInStock () {

            var productInStock = mStore.HasProductInStock ("Test0");
            var productOutOfStock = mStore.HasProductInStock ("Test1");

            // Test0 should be in stock, Test1 should be out of stock
            Assert.True (productInStock);
            Assert.False (productOutOfStock);
        }

        [Fact]
        public void TestGetProductByName () {

            var testProduct = mStore.GetProductByName ("Test0");
            var failProduct = mStore.GetProductByName ("Fail");

            Assert.NotNull (testProduct);
            Assert.Equal (1, mStore.ProductQuantity(testProduct.Name));

            // Store should have no product named "Fail" in it's listing
            Assert.Null (failProduct);
        }
    }
}
