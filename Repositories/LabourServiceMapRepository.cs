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
    public class LabourServiceMapRepository : Repository<LabourServiceMap>, ILabourServiceMap
    {
        public LabourServiceMapRepository(DB_Context context) : base(context) { }

        public IEnumerable<Labour> GetLabourServiceMapByLabourId(int? labourId)
        {
            return Context.Set<Labour>().Where(l => l.Id == labourId).ToList();
        }

        public IEnumerable<Service> GetLabourServiceMapByServiceId(int? serviceId)
        {
            return Context.Set<Service>().Where(s => s.Id == serviceId).ToList();
        }

    }
}
