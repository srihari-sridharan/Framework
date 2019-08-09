using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Base.DataAccess;
using Framework.Entities.Interfaces;
using Framework.Interfaces.DataSources;
using Framework.Test.Infrastructure.Interfaces;
using Framework.Test.Infrastructure.Model;

namespace Framework.Test.Infrastructure.Implementations
{
    public class ContactDao : BaseDao, IContactDao
    {
        private IEFDataSource EFDataSource => GetDS<IEFDataSource>(Constants.DatabaseName);

        public int Delete(int contactId)
        {
            return EFDataSource.Delete<Contact>(contactId);
        }

        public async Task<int> DeleteAsync(int contactId)
        {
            return await EFDataSource.DeleteAsync<Contact>(contactId);
        }

        public override bool DoesDuplicateExist(IBaseEntity baseEntity)
        {
            return EFDataSource.GetById<Contact>(baseEntity.Id) != null;
        }

        public Contact Get(int contactId)
        {
            return EFDataSource.GetById<Contact>(contactId);
        }

        public IEnumerable<Contact> GetAll()
        {
            return EFDataSource.GetAll<Contact>();
        }

        public int Insert(Contact contact)
        {
            return EFDataSource.Insert(contact);
        }

        public async Task<int> InsertAsync(Contact contact)
        {
            return await EFDataSource.InsertAsync(contact);
        }

        public int InsertRange(IEnumerable<Contact> contacts)
        {
            return EFDataSource.InsertRange(contacts);
        }

        public async Task<int> InsertRangeAsync(IEnumerable<Contact> contacts)
        {
            return await EFDataSource.InsertRangeAsync(contacts);
        }

        public int Update(Contact contact)
        {
            return EFDataSource.Update(contact);
        }

        public async Task<int> UpdateAsync(Contact contact)
        {
            return await EFDataSource.UpdateAsync(contact);
        }
    }
}