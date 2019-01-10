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
    public class LabourAssignedRepository : Repository<LabourAssigned>, ILabourAssigned
    {
        public LabourAssignedRepository(DB_Context context) : base(context) { }

        public override LabourAssigned Get(int id)
        {
            return Context.Set<LabourAssigned>().SingleOrDefault(l => l.Id == id);
        }

        public IEnumerable<Labour> GetLabourByLabourId(int? labourId)
        {
            return Context.Set<Labour>().Where(l => l.Id == labourId).ToList();
        }

        public IEnumerable<ServiceRequest> GetServiceRequestByRequestId(int? serviceRequestId)
        {
            return Context.Set<ServiceRequest>().Where(s => s.Id == serviceRequestId).ToList();
        }

        public IEnumerable<Employee> GetEmployeeByEmployeeId(int? employeeId)
        {
            return Context.Set<Employee>().Where(e => e.Id == employeeId).ToList();
        }
    }
}
