using System.Linq;
using Framework.Base;
using Framework.Test.Infrastructure.Interfaces;
using Framework.Test.Infrastructure.Model;
using Xunit;

namespace Framework.Test.Infrastructure.Tests
{
    public class ContactServiceTest : BaseTest
    {
        private readonly IContactService contactService;

        public ContactServiceTest()
        {
            contactService = Application.Container.GetService<IContactService>();
        }

        //[Fact]
        //public void TestUpdateContact()
        //{
        //    Contact contact = GetNewContact(1);
        //    contactService.AddContact(contact);
        //    int count = contactService.GetContacts().Count();
        //    string newName = "Test2";
        //    contact.Name = newName;
        //    contactService.UpdateContact(contact);
        //    contact = contactService.GetContact(1);
        //    string persistedName = contact.Name;
        //    contactService.DeleteContact(1);
        //    Assert.True(newName.Equals(persistedName));
        //}

        [Fact]
        public void TestAddContact()
        {
            var contact = GetNewContact(1);
            contactService.AddContact(contact);
            var count = contactService.GetContacts().Count();
            contactService.DeleteContact(1);
            Assert.True(count.Equals(1));
        }

        [Fact]
        public async void TestAddContactAsync()
        {
            var contact = GetNewContact(1);
            await contactService.AddContactAsync(contact);
            var count = contactService.GetContacts().Count();
            contactService.DeleteContact(1);
            Assert.True(count.Equals(1));
        }

        [Fact]
        public void TestContactServiceInstantiation()
        {
            Assert.NotNull(contactService);
        }

        [Fact]
        public void TestDeleteContact()
        {
            var contact = GetNewContact(1);
            contactService.AddContact(contact);
            var count = contactService.GetContacts().Count();
            contactService.DeleteContact(1);
            var countAfterDelete = contactService.GetContacts().Count();
            Assert.True(count.Equals(1) && countAfterDelete.Equals(0));
        }

        private static Contact GetNewContact(int id)
        {
            var contact = new Contact
            {
                Id = id,
                Name = "Test1",
                Email = "Test@Test.com",
                Phone = "1234567890"
            };
            return contact;
        }
    }
}