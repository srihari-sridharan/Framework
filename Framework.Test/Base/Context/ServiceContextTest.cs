using System.Collections.Generic;
using Framework.Base;
using Framework.Interfaces.Context;
using Xunit;

namespace Framework.Test.Base.Context
{
    public class ServiceContextTest : BaseTest
    {
        [Fact]
        public void ServiceContextInstantiation()
        {
            Assert.True(null != contactsApplication
                        && null != applicationContext
                        && null != serviceContext);
        }

        [Fact]
        public void SetAndGetCurrentLevel()
        {
            var currentLevel = 1;
            serviceContext.CurrentLevel = currentLevel;
            var output = serviceContext.CurrentLevel;
            Assert.Equal(currentLevel, output);
        }

        [Fact]
        public void SetAndGetCurrentServiceName()
        {
            var serviceName = "Test";
            serviceContext.CurrentServiceName = serviceName;
            var output = serviceContext.CurrentServiceName;
            Assert.Equal(serviceName, output);
        }

        [Fact]
        public void SetAndGetErrorInService()
        {
            var errorInService = "Error In Service";
            serviceContext.ErrorInService = errorInService;
            var output = serviceContext.ErrorInService;
            Assert.Equal(errorInService, output);
        }

        [Fact]
        public void SetAndGetErrorMessage()
        {
            var errorMessage = "Error Message!";
            serviceContext.ErrorInService = errorMessage;
            var output = serviceContext.ErrorInService;
            Assert.Equal(errorMessage, output);
        }

        [Fact]
        public void SetAndGetIsInError()
        {
            var isInError = false;
            serviceContext.IsInError = isInError;
            var output = serviceContext.IsInError;
            Assert.Equal(isInError, output);
        }

        [Fact]
        public void SetAndGetServiceStack()
        {
            var serviceStack = new Stack<string>();
            serviceContext.ServiceStack = serviceStack;
            var output = serviceContext.ServiceStack;
            Assert.Equal(serviceStack, output);
        }

        [Fact]
        public void SetAndGetTransactionContext()
        {
            var transactionContext
                = Application.Container.GetInstanceOfType<ITransactionContext>(Constants.Context);
            serviceContext.TransactionContext = transactionContext;
            var output = serviceContext.TransactionContext;
            Assert.Equal(transactionContext, output);
        }
    }
}