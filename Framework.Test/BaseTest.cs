using System;
using Framework.Base;
using Framework.Interfaces.Context;
using Framework.Test.Infrastructure.Implementations;

namespace Framework.Test
{
    public abstract class BaseTest : IDisposable
    {
        protected IApplicationContext applicationContext;
        protected ContactsApplication contactsApplication;
        protected IServiceContext serviceContext;
        protected ITransactionContext transactionContext;
        protected IUserContext userContext;

        public BaseTest()
        {
            contactsApplication = new ContactsApplication();
            applicationContext = Application.Context;
            serviceContext = applicationContext.ServiceContext;
            transactionContext = serviceContext.TransactionContext;
            userContext = applicationContext.UserContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != userContext) userContext = null;

                if (null != transactionContext) transactionContext = null;

                if (null != serviceContext) serviceContext = null;

                if (null != applicationContext) applicationContext = null;

                if (null != contactsApplication) contactsApplication = null;
            }
        }
    }
}