using Xunit;

using Project0.Business;

namespace Project0.Test {

    public class OrderTest {

        private readonly Store mStore;

        public OrderTest () {

            mStore = new Store ();

            mStore.Products.Add (new Product () {
                Name = "Test"
            });
        }

        [Fact]
        public void TestMaximumQuantity () {

            var testOrder = new Order ();

            bool productsCanBeAdded = testOrder.AddProduct (mStore, "Test", 21);

            Assert.False (productsCanBeAdded);
        }
    }
}
