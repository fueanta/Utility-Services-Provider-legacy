using Data;
using Entities;
using Interfaces;
using System.Linq;

namespace Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployee
    {
        public EmployeeRepository(DB_Context context) : base(context) { }

        public override Employee Get(int id)
        {
            return Context.Set<Employee>().SingleOrDefault(e => e.FakeId == id);
        }
    }
}
