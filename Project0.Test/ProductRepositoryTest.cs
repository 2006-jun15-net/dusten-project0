using Project0.Business.Database;
using Xunit;

namespace Project0.Test {

    public class ProductRepositoryTest {

        private readonly ProductRepository mProductRepository;

        public ProductRepositoryTest () {

            mProductRepository = new ProductRepository ("../../../../products.json");
            mProductRepository.LoadItems ();
        }

        [Fact]
         public void TestLoadFromJsonFile () {

            var testProduct = mProductRepository.FindByID (1);
            
            Assert.Equal ("Milk", testProduct.Name);
            Assert.Equal (1.5, testProduct.Price);
        }

        [Fact]
        public void TestFindByName () {

            var testProduct = mProductRepository.FindByID (1);
            var testProductByName = mProductRepository.FindByName ("Milk");

            Assert.Equal (testProduct, testProductByName);
        }
    }
}
