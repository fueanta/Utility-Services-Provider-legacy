using Entities;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IArea : IRepository<Area>
    {
        IEnumerable<Area> GetAreasByCityId(int? cityId);
    }
    public interface ICity : IRepository<City> { }
    public interface IClient : IRepository<Client> { }
    public interface ILabour : IRepository<Labour> { }
    public interface IEmployee : IRepository<Employee> { }
    public interface IUserLogin : IRepository<UserLogin> { }
    public interface IService : IRepository<Service> { }
    public interface IAdmin : IRepository<Admin> { }
    public interface ILabourServiceMap : IRepository<LabourServiceMap> { }
    public interface IServiceRequest : IRepository<ServiceRequest> { }
    public interface ILabourAssigned : IRepository<LabourAssigned> { }
    public interface ITransaction : IRepository<Transaction> { }

    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        int Insert(T ob);
        int Update(T ob);
        int Delete(T ob);
    }
}
