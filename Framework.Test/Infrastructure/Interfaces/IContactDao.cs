using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Entities.Interfaces;
using Framework.Interfaces.DataAccess;
using Framework.Test.Infrastructure.Model;

namespace Framework.Test.Infrastructure.Interfaces
{
    public interface IContactDao : IDao
    {
        int Delete(int contactId);

        Task<int> DeleteAsync(int contactId);

        bool DoesDuplicateExist(IBaseEntity baseEntity);

        Contact Get(int contactId);

        IEnumerable<Contact> GetAll();

        int Insert(Contact contact);

        Task<int> InsertAsync(Contact contact);

        int InsertRange(IEnumerable<Contact> contacts);

        Task<int> InsertRangeAsync(IEnumerable<Contact> contacts);

        int Update(Contact contact);

        Task<int> UpdateAsync(Contact contact);
    }
}