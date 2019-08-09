using System.Linq;
using Framework.Test.Infrastructure.Model;

namespace Framework.Test.Infrastructure.Implementations
{
    public class ContactsSet : StubDbSet<Contact>
    {
        public override Contact Find(params object[] keyValues)
        {
            return this.SingleOrDefault(contact => contact.Id == (int)keyValues.Single());
        }
    }
}