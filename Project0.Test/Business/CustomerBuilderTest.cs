using Xunit;

using Project0.Business;
using Project0.DataAccess.Model;

namespace Project0.Test.Business {

    public class CustomerBuilderTest {

        readonly CustomerBuilder mCustomerBuilder;

        public CustomerBuilderTest () {
            mCustomerBuilder = new CustomerBuilder ();
        }

        [Fact]
        public void TestBuildSuccess () {

            var customer = mCustomerBuilder.Build ("Agent Smith");
            Assert.NotSame (default(Customer), customer);
        }

        [Fact]
        public void TestBuildFailure () {

            var customer = mCustomerBuilder.Build ("AgentSmith");
            Assert.Same (default(Customer), customer);
        }
    }
}
