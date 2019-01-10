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
    public class TransactionRepository : Repository<Transaction>, ITransaction
    {
        public TransactionRepository(DB_Context context) : base(context) { }

        public override Transaction Get(int id)
        {
            return Context.Set<Transaction>().SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<Labour> GetLabourByLabourId(int? labourId)
        {
            return Context.Set<Labour>().Where(l => l.Id == labourId).ToList();
        }

        public IEnumerable<Employee> GetEmployeeByEmployeeId(int? employeeId)
        {
            return Context.Set<Employee>().Where(e => e.Id == employeeId).ToList();
        }
    }
}
