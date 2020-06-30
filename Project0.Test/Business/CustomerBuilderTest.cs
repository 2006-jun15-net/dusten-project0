using Xunit;

using Project0.Business;

namespace Project0.Test.Business {

    public class CustomerBuilderTest {

        readonly CustomerBuilder mCustomerBuilder;

        public CustomerBuilderTest () {
            mCustomerBuilder = new CustomerBuilder ();
        }

        [Fact]
        public void TestBuildSuccess () {

            var customer = mCustomerBuilder.Build ("Test Customer");

            Assert.Equal ("Test", customer.Firstname);
            Assert.Equal ("Customer", customer.Lastname);
        }

        [Fact]
        public void TestBuildFailure () {

            Assert.Throws<BusinessLogicException> (
                () => mCustomerBuilder.Build ("AgentSmith")
            );
        }
    }
}
