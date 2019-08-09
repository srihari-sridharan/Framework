using System.Data.Entity;
using System.Threading.Tasks;
using Framework.Base.DataSources;
using Framework.Test.Infrastructure.Model;

namespace Framework.Test.Infrastructure.Implementations
{
    public class ContactsDbContext : EFBaseDbContext
    {
        public ContactsDbContext(string connectionString)
            : base(connectionString)
        {
            Contacts = new ContactsSet();
        }

        public ContactsSet Contacts { get; set; }

        public int SaveChangesCount { get; private set; }

        public override int SaveChanges()
        {
            SaveChangesCount++;
            return 1;
        }

        public override async Task<int> SaveChangesAsync()
        {
            var result = await Task.FromResult(SaveChanges());
            return result;
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            if (typeof(TEntity).Equals(typeof(Contact))) return Contacts as StubDbSet<TEntity>;
            return null;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}