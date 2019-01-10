using Data;
using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ServiceRequestRepository : Repository<ServiceRequest>, IServiceRequest
    {
        public ServiceRequestRepository(DB_Context context) : base(context) { }

        public override ServiceRequest Get(int id)
        {
            return Context.Set<ServiceRequest>().SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<Client> GetLabourServiceMapByClientId(int? clientId)
        {
            return Context.Set<Client>().Where(c => c.Id == clientId).ToList();
        }

        public IEnumerable<Service> GetLabourServiceMapByServiceId(int? serviceId)
        {
            return Context.Set<Service>().Where(s => s.Id == serviceId).ToList();
        }
    }

}
