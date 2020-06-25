using Project0.Business;
using Project0.Business.Database;
using System;
using Xunit;

namespace Project0.Test {

    public class OrderRepositoryTest {

        private readonly OrderRepository mOrderRepository;

        public OrderRepositoryTest () {

            var productRepository = new ProductRepository ("../../../../products.json");
            productRepository.LoadItems ();

            var storeRepository = new StoreRepository ("../../../../stores.json", productRepository);
            storeRepository.LoadItems ();

            var customerRepository = new CustomerRepository ("../../../../customers.json", storeRepository);
            customerRepository.LoadItems ();

            mOrderRepository = new OrderRepository ("../../../../orders.json", productRepository, customerRepository, storeRepository);
            mOrderRepository.LoadItems ();
        }

        [Fact]
        public void TestLoadFromJsonFile () {

            var testOrder = mOrderRepository.FindByID (1);

            // Test IDs
            Assert.Equal ((ulong)1, testOrder.ID);
            Assert.Equal ((ulong)1, testOrder.Customer.ID);
            Assert.Equal ((ulong)1, testOrder.Store.ID);

            // Test timestamp
            Assert.Equal (new DateTime (2020, 6, 23), testOrder.Timestamp);
        }

        [Fact]
        public void TestProductsLoaded () {

            var testOrder = mOrderRepository.FindByID (1);
            var products = testOrder.Products;
            var quantities = testOrder.Quantities;

            // Should be only two products
            Assert.Equal (2, products.Count);
            Assert.Equal (2, quantities.Count);

            // Test IDs
            Assert.Equal ((ulong)1, products[0].ID);
            Assert.Equal ((ulong)2, products[1].ID);

            // Test quanitites
            Assert.Equal (2, quantities[0]);
            Assert.Equal (5, quantities[1]);
        }

        [Fact]
        public void TestFindByStore () {

            var testOrders = mOrderRepository.FindByStore (new Store () { ID = 1 });
            
            // Should only have one order for store 0
            Assert.Single (testOrders);

            // Test that the correct order with the correct store ID was found
            Assert.Equal((ulong)1, testOrders[0].ID);
            Assert.Equal((ulong)1, testOrders[0].Store.ID);
        }

        [Fact]
        public void TestFindByCustomer () {

            var testOrders = mOrderRepository.FindByCustomer (new Customer () { ID = 1 });
            
            // Should only have one order for store 0
            Assert.Single (testOrders);

            // Test that the correct order with the correct store ID was found
            Assert.Equal((ulong)1, testOrders[0].ID);
            Assert.Equal((ulong)1, testOrders[0].Customer.ID);
        }
    }
}
