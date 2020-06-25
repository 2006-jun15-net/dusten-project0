using Project0.Business;
using Project0.Business.Database;
using System;
using Xunit;

namespace Project0.Test {

    public class OrderRepositoryTest {

        private readonly OrderRepository mOrderRepository;

        public OrderRepositoryTest () {

            var productRepository = new ProductRepository ("../../../../products.json");
            var storeRepository = new StoreRepository ("../../../../stores.json", productRepository);
            var customerRepository = new CustomerRepository ("../../../../customers.json", storeRepository);

            mOrderRepository = new OrderRepository ("../../../../orders.json", productRepository, customerRepository, storeRepository);
        }

        [Fact]
        public void TestLoadFromJsonFile () {

            var testOrder = mOrderRepository.FindByID (0);

            // Test IDs
            Assert.Equal ((ulong)0, testOrder.ID);
            Assert.Equal ((ulong)0, testOrder.Customer.ID);
            Assert.Equal ((ulong)0, testOrder.Store.ID);

            // Test timestamp
            Assert.Equal (new DateTime (2020, 6, 23), testOrder.Timestamp);
        }

        [Fact]
        public void TestProductsLoaded () {

            var testOrder = mOrderRepository.FindByID (0);
            var products = testOrder.Products;

            // Should be only one product
            Assert.Single (products);

            Assert.Equal ("Milk", products[0].Name);
            Assert.Equal (1.5, products[0].Price);
        }

        [Fact]
        public void TestFindByStore () {

            var testOrders = mOrderRepository.FindByStore (new Store () { ID = 0 });
            
            // Should only have one order for store 0
            Assert.Single (testOrders);

            // Test that the correct order with the correct store ID was found
            Assert.Equal((ulong)0, testOrders[0].ID);
            Assert.Equal((ulong)0, testOrders[0].Store.ID);
        }

        [Fact]
        public void TestFindByCustomer () {

            var testOrders = mOrderRepository.FindByCustomer (new Customer () { ID = 0 });
            
            // Should only have one order for store 0
            Assert.Single (testOrders);

            // Test that the correct order with the correct store ID was found
            Assert.Equal((ulong)0, testOrders[0].ID);
            Assert.Equal((ulong)0, testOrders[0].Customer.ID);
        }
    }
}
