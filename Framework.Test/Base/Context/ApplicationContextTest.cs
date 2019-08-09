// <copyright file="ApplicationContextTest.cs" company="">
//
// </copyright>

using Xunit;

namespace Framework.Test.Base.Context
{
    public class ApplicationContextTest : BaseTest
    {
        [Fact]
        public void ApplicationContextInstantiation()
        {
            if (!applicationContext.Initialized) applicationContext.Init();

            Assert.True(null != contactsApplication
                        && applicationContext?.Properties != null
                        && null != applicationContext.UserContext
                        && null != applicationContext.ServiceContext
                        && applicationContext.Initialized);
        }

        [Fact]
        public void Init()
        {
            if (null != applicationContext
                && !applicationContext.Initialized)
                applicationContext.Init();

            Assert.True(applicationContext?.Properties != null
                        && null != applicationContext.UserContext
                        && null != applicationContext.ServiceContext
                        && applicationContext.Initialized);
        }
    }
}