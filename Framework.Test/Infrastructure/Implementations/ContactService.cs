using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Base.Services;
using Framework.Test.Infrastructure.Interfaces;
using Framework.Test.Infrastructure.Model;

namespace Framework.Test.Infrastructure.Implementations
{
    public class ContactService : BaseService, IContactService
    {
        public ContactService(IContactDao contactDao)
        {
            ConcreteDataAccessObject = contactDao;
        }

        public bool AddContact(Contact contact)
        {
            var result = DataAccessObject<IContactDao>().Insert(contact);
            return result > 0;
        }

        public async Task<int> AddContactAsync(Contact contact)
        {
            return await DataAccessObject<IContactDao>().InsertAsync(contact);
        }

        public bool DeleteContact(int contactId)
        {
            var result = DataAccessObject<IContactDao>().Delete(contactId);
            return result > 0;
        }

        public async Task<int> DeleteContactAsync(int contactId)
        {
            return await DataAccessObject<IContactDao>().DeleteAsync(contactId);
        }

        public Contact GetContact(int contactId)
        {
            return DataAccessObject<IContactDao>().Get(contactId);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return DataAccessObject<IContactDao>().GetAll();
        }

        public IEnumerable<string> GetContactsAsJSON()
        {
            return DataAccessObject<IContactDao>().GetAll().Select(contact => contact.ToString());
        }

        public bool UpdateContact(Contact contact)
        {
            var result = DataAccessObject<IContactDao>().Update(contact);
            return result > 0;
        }

        public async Task<int> UpdateContactAsync(Contact contact)
        {
            return await DataAccessObject<IContactDao>().UpdateAsync(contact);
        }
    }
}