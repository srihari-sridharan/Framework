using Xunit;

namespace Framework.Test.Base.Context
{
    public class TransactionContextTest : BaseTest
    {
        [Fact]
        public void SetAndGetTransactionInitiatedByServiceName()
        {
            var transactionInitiatedByServiceName = "Test";
            transactionContext.TransactionInitiatedByServiceName = transactionInitiatedByServiceName;
            var output = transactionContext.TransactionInitiatedByServiceName;
            Assert.Equal(transactionInitiatedByServiceName, output);
        }

        [Fact]
        public void SetAndGetTransactionInitiatorLevel()
        {
            var transactionInitiatorLevel = 1;
            transactionContext.TransactionInitiatorLevel = transactionInitiatorLevel;
            var output = transactionContext.TransactionInitiatorLevel;
            Assert.Equal(transactionInitiatorLevel, output);
        }

        [Fact]
        public void SetAndGetTransactionInProgress()
        {
            var transactionInProgress = false;
            transactionContext.TransactionInProgress = transactionInProgress;
            var output = transactionContext.TransactionInProgress;
            Assert.Equal(transactionInProgress, output);
        }

        [Fact]
        public void TransactionContextInstantiation()
        {
            Assert.True(null != contactsApplication
                        && null != applicationContext
                        && null != serviceContext
                        && null != transactionContext);
        }
    }
}