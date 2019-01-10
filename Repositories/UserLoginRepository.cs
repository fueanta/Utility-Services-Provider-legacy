using Data;
using Entities;
using Interfaces;

namespace Repositories
{
    public class UserLoginRepository : Repository<UserLogin>, IUserLogin
    {
        public UserLoginRepository(DB_Context context) : base(context) { }
    }
}
