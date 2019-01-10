using Data;
using Entities;
using Interfaces;
using System.Linq;

namespace Repositories
{
    public class LabourRepository : Repository<Labour>, ILabour
    {
        public LabourRepository(DB_Context context) : base(context) { }

        public override Labour Get(int id)
        {
            return Context.Set<Labour>().SingleOrDefault(l => l.FakeId == id);
        }
    }
}
