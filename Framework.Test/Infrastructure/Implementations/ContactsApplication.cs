// <copyright file="ContactsApplication.cs" company="">
//
// </copyright>

using Framework.Base;
using Framework.Base.DataSources.Implementation;

namespace Framework.Test.Infrastructure.Implementations
{
    public class ContactsApplication : Application
    {
        public ContactsApplication()
        {
            FullyQualifiedContainer = Framework.Base.Constants.FullyQualifiedContainer;
            Start();
        }

        protected override void OnStart()
        {
            Container
                .RegisterDataSource(Constants.DatabaseName,
                    new EFDataSource<ContactsDbContext>
                    {
                        ConnectionString = "Server=localhost;Database=test;Trusted_Connection=true",
                        DatabaseName = Constants.DatabaseName
                    });
        }
    }
}