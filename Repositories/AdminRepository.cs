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
    public class AdminRepository : Repository<Admin>, IAdmin
    {
        public AdminRepository(DB_Context context) : base(context) { }
    }
}
