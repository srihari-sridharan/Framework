// <copyright file="UserContextTest.cs" company="">
//
// </copyright>

using System;
using Xunit;

namespace Framework.Test.Base.Context
{
    public class UserContextTest : BaseTest
    {
        [Fact]
        public void GetName()
        {
            Assert.Throws<NotImplementedException>(new Action(InvokeName));
        }

        [Fact]
        public void GetRoles()
        {
            Assert.Throws<NotImplementedException>(new Action(InvokeRoles));
        }

        [Fact]
        public void GetSessionExpiresAt()
        {
            Assert.Throws<NotImplementedException>(new Action(InvokeSessionExpiresAt));
        }

        [Fact]
        public void IsInRole()
        {
            Assert.Throws<NotImplementedException>(new Action(InvokeIsInRole));
        }

        [Fact]
        public void RenewSession()
        {
            Assert.Throws<NotImplementedException>(new Action(InvokeRenewSession));
        }

        [Fact]
        public void UserContextInstantiation()
        {
            Assert.True(null != contactsApplication
                        && null != applicationContext
                        && null != userContext);
        }

        private void InvokeIsInRole()
        {
            Console.WriteLine("Not implemented exception thrown!");
            var isInRole = userContext.IsInRole(string.Empty);
        }

        private void InvokeName()
        {
            Console.WriteLine("Not implemented exception thrown!");
            var name = userContext.Name;
        }

        private void InvokeRenewSession()
        {
            Console.WriteLine("Not implemented exception thrown!");
            userContext.RenewSession();
        }

        private void InvokeRoles()
        {
            Console.WriteLine("Not implemented exception thrown!");
            var roles = userContext.Roles;
        }

        private void InvokeSessionExpiresAt()
        {
            Console.WriteLine("Not implemented exception thrown!");
            var sessionExpiresAt = userContext.SessionExpiresAt;
        }
    }
}