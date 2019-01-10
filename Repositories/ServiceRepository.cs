using Data;
using Entities;
using Interfaces;

namespace Repositories
{
    public class ServiceRepository : Repository<Service>, IService
    {
        public ServiceRepository(DB_Context context) : base(context) { }
    }
}
