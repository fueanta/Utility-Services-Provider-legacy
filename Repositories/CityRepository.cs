using Data;
using Entities;
using Interfaces;

namespace Repositories
{
    public class CityRepository : Repository<City>, ICity
    {
        public CityRepository(DB_Context context) : base(context) { }
    }
}
