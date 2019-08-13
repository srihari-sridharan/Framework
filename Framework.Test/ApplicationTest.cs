using Framework.Base;
using Framework.Test.Infrastructure.Implementations;
using Xunit;

namespace Framework.Test
{
    public class ApplicationTest
    {
        [Fact]
        public void AppInit()
        {
            var contactsApplication = new ContactsApplication();
            Assert.True(null != contactsApplication
                        && null != Application.Container
                        && null != Application.Context
                        && null != Application.Current);
        }
    }
}