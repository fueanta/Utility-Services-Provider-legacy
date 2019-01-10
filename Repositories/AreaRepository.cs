using System.Collections.Generic;
using System.Linq;
using Data;
using Entities;
using Interfaces;

namespace Repositories
{
    public class AreaRepository : Repository<Area>, IArea
    {
        public AreaRepository(DB_Context context) : base(context) { }

        public IEnumerable<Area> GetAreasByCityId(int? cityId)
        {
            return Context.Set<Area>().Where(a => a.CityId == cityId).ToList();
        }
    }
}
