using Xunit;

using Project0.Business;

namespace Project0.Test {

    public class OrderTest {

        [Fact]
        public void TestMaximumQuantity () {

            var testProduct = new Product () {

                Name = "Test",
                Quantity = 21
            };

            var testOrder = new Order ();

            bool productsCanBeAdded = testOrder.AddProduct (testProduct);

            Assert.False (productsCanBeAdded);
        }
    }
}
