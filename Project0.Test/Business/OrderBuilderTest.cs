using Xunit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Project0.Business;
using Project0.DataAccess;
using Project0.DataAccess.Model;
using Project0.DataAccess.Repository;

namespace Project0.Test.Business {

    public class OrderBuilderTest {

        private readonly OrderBuilder mOrderBuilder;
        private readonly Store mTestStore;

        public OrderBuilderTest () {

            var testProduct = new Product {
                Name = "Test"
            };

            var testStock = new StoreStock {
                Product = testProduct,
                ProductQuantity = 100
            };

            mTestStore = new Store {
                StoreStock = { testStock }
            };

            mOrderBuilder = new OrderBuilder ();
        }

        [Fact]
        public void TestAddProductSuccess () {

            // No thrown exception = success
            mOrderBuilder.AddProduct (mTestStore, "Test", 2);
        }

        [Fact]
        public void TestAddProductFailQuantity () {

            // Quantity should not be allowed
            Assert.Throws<BusinessLogicException> (
                () => mOrderBuilder.AddProduct (mTestStore, "Test", 21)
            );
        }

        [Fact]
        public void TestAddProductFailName () {

            // Name should not exist
            Assert.Throws<BusinessLogicException> (
                () => mOrderBuilder.AddProduct (mTestStore, "Bacon", 2)
            );
        }
    }
}
