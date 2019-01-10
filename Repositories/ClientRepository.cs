using Data;
using Entities;
using Interfaces;
using System.Linq;

namespace Repositories
{
    public class ClientRepository : Repository<Client>, IClient
    {
        public ClientRepository(DB_Context context) : base(context) { }

        public override Client Get(int id)
        {
            return Context.Set<Client>().SingleOrDefault(c => c.FakeId == id);
        }
    }
}
