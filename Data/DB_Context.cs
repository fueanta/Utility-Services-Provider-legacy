using Entities;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Data
{
    public class DB_Context : DbContext
    {
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Labour> Labours { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<LabourServiceMap> LabourServiceMaps { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<LabourAssigned> LabourAssigneds { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DB_Context() : base("name=USP_DefaultConnection")
        {

        }
    }
}
