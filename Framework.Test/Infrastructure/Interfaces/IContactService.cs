using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Interfaces.Services;
using Framework.Test.Infrastructure.Model;

namespace Framework.Test.Infrastructure.Interfaces
{
    public interface IContactService : IService
    {
        bool AddContact(Contact contact);

        Task<int> AddContactAsync(Contact contact);

        bool DeleteContact(int contactId);

        Task<int> DeleteContactAsync(int contactId);

        Contact GetContact(int contactId);

        IEnumerable<Contact> GetContacts();

        IEnumerable<string> GetContactsAsJSON();

        bool UpdateContact(Contact contact);

        Task<int> UpdateContactAsync(Contact contact);
    }
}