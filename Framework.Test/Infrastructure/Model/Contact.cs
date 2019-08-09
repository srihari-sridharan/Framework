using Framework.Entities.Implementation;
using Newtonsoft.Json;

namespace Framework.Test.Infrastructure.Model
{
    public class Contact : BaseEntity
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}