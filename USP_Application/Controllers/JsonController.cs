using Entities;
using Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using USP_Application.Models;

namespace USP_Application.Controllers
{
    public class JsonController : Controller
    {
        IClient clientRepository;
        ILabour labourRepository;
        IEmployee employeeRepository;
        IUserLogin userLoginRepository;
        ICity cityRepository;
        IArea areaRepository;
        IService serviceRepository;
        IAdmin adminRepository;
        ILabourServiceMap labourServiceMapRepository;
        IServiceRequest serviceRequestRepository;
        ILabourAssigned labourAssignedRepository;

        public JsonController(IClient clRepo, ILabour labRepo, IEmployee emRepo, IUserLogin ulRepo, ICity ctRepo, IArea arRepo, IService srRepo, IAdmin adRepo, ILabourServiceMap lsRepo, IServiceRequest sreqRepo, ILabourAssigned laRepo)
        {
            clientRepository = clRepo;
            labourRepository = labRepo;
            employeeRepository = emRepo;
            userLoginRepository = ulRepo;
            cityRepository = ctRepo;
            areaRepository = arRepo;
            serviceRepository = srRepo;
            adminRepository = adRepo;
            labourServiceMapRepository = lsRepo;
            serviceRequestRepository = sreqRepo;
            labourAssignedRepository = laRepo;
        }

        /*City-Area*/
        public JsonResult AreaList(int id)
        {
            var area = areaRepository.GetAreasByCityId(id).OrderBy(a => a.Name).ToList();
            return Json(new SelectList(area.ToArray(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        public IEnumerable<Area> GetArea(int CityId)
        {
            return areaRepository.GetAreasByCityId(CityId).OrderBy(a => a.Name).ToList();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadClassesByCityId(string Name)
        {
            var areaList = this.GetArea(Convert.ToInt32(Name));
            var areaData = areaList.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),
            });
            return Json(areaData, JsonRequestBehavior.AllowGet);
        }
        /*City-Area*/

        /*Area-List Search*/

        public JsonResult SearchAreaList(string searchBy, string searchValue, int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;
            List<Area> areaList;

            switch (searchBy)
            {
                case "id":
                    //search by id
                    areaList = areaRepository.GetAll().Where(c => c.Id.ToString().Contains(searchValue) || searchValue == null).ToList();
                    break;
                case "city":
                    //search by city
                    areaList = areaRepository.GetAll().Where(c => c.City.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                default:
                    //by default, search by name
                    areaList = areaRepository.GetAll().Where(c => c.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
            }
            return Json(areaList, JsonRequestBehavior.AllowGet);
        }
        /*Area-List Search*/

        /*Client-List Search*/

        public JsonResult SearchClientList(string searchBy, string searchValue, int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            List<Client> clientList = clientRepository.GetAll().ToList();
            List<UserLogin> userLogins = userLoginRepository.GetAll().ToList();

            switch (searchBy)
            {
                case "phone":
                    //search by phone
                    userLogins = userLogins.Where(u => u.Phone.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "email":
                    //search by email
                    userLogins = userLogins.Where(u => u.Email.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "city":
                    //search by city
                    clientList = clientList.Where(c => c.City.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "area":
                    //search by area
                    clientList = clientList.Where(c => c.Area.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "id":
                    //search by id
                    clientList = clientList.Where(c => c.FakeId.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                default:
                    //by default, search by name
                    clientList = clientList.Where(c => c.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
            }

            IEnumerable< ClientUserLoginModel> clientUserLoginModel;

            if (searchBy.Equals("phone") | searchBy.Equals("email"))
            {
                clientUserLoginModel =
                   from userLogin in userLogins
                   join client in clientList on userLogin.Id equals client.FakeId
                   select new ClientUserLoginModel { Id = client.FakeId, Name = client.Name, City = client.City.Name, Area = client.Area.Name, Phone = userLogin.Phone, Email = userLogin.Email };
            }
            else
            {
                clientUserLoginModel =
                   from client in clientList
                   join userLogin in userLogins on client.FakeId equals userLogin.Id
                   select new ClientUserLoginModel { Id = client.FakeId, Name = client.Name, City = client.City.Name, Area = client.Area.Name, Phone = userLogin.Phone, Email = userLogin.Email };
            }

            return Json(clientUserLoginModel, JsonRequestBehavior.AllowGet);
            // .ToPagedList(pageNo, numOfRows)
        }
        /*Client-List Search*/

        /*Labour-List Search*/

        public JsonResult SearchLabourList(string searchBy, string searchValue, int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            List<Labour> labourList = labourRepository.GetAll().ToList();
            List<UserLogin> userLogins = userLoginRepository.GetAll().ToList();

            switch (searchBy)
            {
                case "phone":
                    //search by phone
                    userLogins = userLogins.Where(u => u.Phone.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "email":
                    //search by email
                    userLogins = userLogins.Where(u => u.Email.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "city":
                    //search by city
                    labourList = labourList.Where(l => l.City.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "area":
                    //search by area
                    labourList = labourList.Where(l => l.Area.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "id":
                    //search by id
                    labourList = labourList.Where(l => l.FakeId.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "wallet":
                    //search by wallet
                    labourList = labourList.Where(l => l.Wallet.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                default:
                    //by default, search by name
                    labourList = labourList.Where(l => l.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
            }

            IEnumerable<LabourUserLoginModel> labourUserLoginModel;

            if (searchBy.Equals("phone") | searchBy.Equals("email"))
            {
                labourUserLoginModel =
                   from userLogin in userLogins
                   join labour in labourList on userLogin.Id equals labour.FakeId
                   select new LabourUserLoginModel { Id = labour.FakeId, Name = labour.Name, City = labour.City.Name, Area = labour.Area.Name, Phone = userLogin.Phone, Email = userLogin.Email };
            }
            else
            {
                labourUserLoginModel =
                   from labour in labourList
                   join userLogin in userLogins on labour.FakeId equals userLogin.Id
                   select new LabourUserLoginModel { Id = labour.FakeId, Name = labour.Name, City = labour.City.Name, Area = labour.Area.Name, Phone = userLogin.Phone, Email = userLogin.Email };
            }

            return Json(labourUserLoginModel, JsonRequestBehavior.AllowGet);
            // .ToPagedList(pageNo, numOfRows)
        }
        /*Labour-List Search*/

        /*Employee-List Search*/

        public JsonResult SearchEmployeeList(string searchBy, string searchValue, int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            List<Employee> employeeList = employeeRepository.GetAll().ToList();
            List<UserLogin> userLogins = userLoginRepository.GetAll().ToList();

            switch (searchBy)
            {
                case "phone":
                    //search by phone
                    userLogins = userLogins.Where(u => u.Phone.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "email":
                    //search by email
                    userLogins = userLogins.Where(u => u.Email.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "salary":
                    //search by salary
                    employeeList = employeeList.Where(e => e.Salary.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "commission":
                    //search by commission
                    employeeList = employeeList.Where(e => e.Commission.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "id":
                    //search by id
                    employeeList = employeeList.Where(e => e.FakeId.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                default:
                    //by default, search by name
                    employeeList = employeeList.Where(e => e.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
            }

            IEnumerable<EmployeeUserLoginModel> employeeUserLoginModel;

            if (searchBy.Equals("phone") | searchBy.Equals("email"))
            {
                employeeUserLoginModel =
                   from userLogin in userLogins
                   join employee in employeeList on userLogin.Id equals employee.FakeId
                   select new EmployeeUserLoginModel { Id = employee.FakeId, Name = employee.Name, Salary = employee.Salary, Commission = employee.Commission, Phone = userLogin.Phone, Email = userLogin.Email };
            }
            else
            {
                employeeUserLoginModel =
                   from employee in employeeList
                   join userLogin in userLogins on employee.FakeId equals userLogin.Id
                   select new EmployeeUserLoginModel { Id = employee.FakeId, Name = employee.Name, Salary = employee.Salary, Commission = employee.Commission, Phone = userLogin.Phone, Email = userLogin.Email };
            }

            return Json(employeeUserLoginModel, JsonRequestBehavior.AllowGet);
            // .ToPagedList(pageNo, numOfRows)
        }
        /*Employee-List Search*/

        /*Service-List Search*/

        public JsonResult SearchServiceList(string searchBy, string searchValue, int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;
            List<Service> serviceList;

            switch (searchBy)
            {
                case "id":
                    //search by id
                    serviceList = serviceRepository.GetAll().Where(s => s.Id.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "name":
                    //search by name
                    serviceList = serviceRepository.GetAll().Where(s => s.ServiceName.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "cost":
                    //search by cost
                    serviceList = serviceRepository.GetAll().Where(s => s.ServiceCost.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "charge":
                    //search by labour charge
                    serviceList = serviceRepository.GetAll().Where(s => s.LabourCharge.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                default:
                    //by default, search by name
                    serviceList = serviceRepository.GetAll().Where(s => s.ServiceName.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
            }
            return Json(serviceList, JsonRequestBehavior.AllowGet);
        }
        /*Service-List Search*/

        /*Admin-List Search*/

        public JsonResult SearchAdminList(string searchBy, string searchValue, int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;
            List<Admin> adminList;

            switch (searchBy)
            {
                case "id":
                    //search by id
                    adminList = adminRepository.GetAll().Where(s => s.Id.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "name":
                    //search by name
                    adminList = adminRepository.GetAll().Where(a => a.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                default:
                    //by default, search by name
                    adminList = adminRepository.GetAll().Where(a => a.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
            }
            return Json(adminList, JsonRequestBehavior.AllowGet);
        }
        /*Admin-List Search*/

        /*City-List Search*/

        public JsonResult SearchCityList(string searchBy, string searchValue, int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;
            List<City> cityList;

            switch (searchBy)
            {
                case "id":
                    //search by id
                    cityList = cityRepository.GetAll().Where(c => c.Id.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "name":
                    //search by name
                    cityList = cityRepository.GetAll().Where(c => c.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                default:
                    //by default, search by name
                    cityList = cityRepository.GetAll().Where(c => c.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
            }
            return Json(cityList, JsonRequestBehavior.AllowGet);
        }
        /*City-List Search*/

        /*LabourServiceMap-List Search*/

        public JsonResult SearchLabourServiceMapList(string searchBy, string searchValue, int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;
            List<LabourServiceMap> labourServiceMapList;

            switch (searchBy)
            {
                case "id":
                    //search by id
                    labourServiceMapList = labourServiceMapRepository.GetAll().Where(l => l.Id.ToString().Contains(searchValue) || searchValue == null).ToList();
                    break;
                case "labourId":
                    //search by city
                    labourServiceMapList = labourServiceMapRepository.GetAll().Where(l => l.Labour.Id.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "serviceId":
                    //search by city
                    labourServiceMapList = labourServiceMapRepository.GetAll().Where(l => l.Service.Id.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "labourName":
                    //search by city
                    labourServiceMapList = labourServiceMapRepository.GetAll().Where(l => l.Labour.Name.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "serviceName":
                    //search by city
                    labourServiceMapList = labourServiceMapRepository.GetAll().Where(l => l.Service.ServiceName.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                default:
                    //by default, search by name
                    labourServiceMapList = labourServiceMapRepository.GetAll().Where(l => l.Id.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
            }
            return Json(labourServiceMapList, JsonRequestBehavior.AllowGet);
        }
        /*LabourServiceMap-List Search*/

        /*ServiceRequest-List Search*/

        public JsonResult SearchServiceRequestList(string searchBy, string searchValue, int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;
            List<ServiceRequest> serviceRequestList;

            switch (searchBy)
            {
                case "id":
                    //search by id
                    serviceRequestList = serviceRequestRepository.GetAll().Where(s => s.Id.ToString().Contains(searchValue) || searchValue == null).ToList();
                    break;
                case "clientName":
                    //search by city
                    serviceRequestList = serviceRequestRepository.GetAll().Where(s => s.Client.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "serviceName":
                    //search by city
                    serviceRequestList = serviceRequestRepository.GetAll().Where(s => s.Service.ServiceName.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "serviceTime":
                    //search by time
                    serviceRequestList = serviceRequestRepository.GetAll().Where(s => s.ServiceTime.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                case "status":
                    //search by city
                    serviceRequestList = serviceRequestRepository.GetAll().Where(s => s.Status.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                default:
                    //by default, search by name
                    serviceRequestList = serviceRequestRepository.GetAll().Where(s => s.Id.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
            }

            serviceRequestList.OrderByDescending(sr => sr.ServiceTime).ToList().ForEach(s => s.Helper = s.ServiceTime.ToString("dd/MM/yyyy hh:mm tt"));

            return Json(serviceRequestList, JsonRequestBehavior.AllowGet);
        }
        /*ServiceRequest-List Search*/

        /*LabourAssigned-List Search*/

        public JsonResult SearchLabourAssignedList(string searchBy, string searchValue, int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;
            List<LabourAssigned> labourAssignedList;

            switch (searchBy)
            {
                case "id":
                    //search by id
                    labourAssignedList = labourAssignedRepository.GetAll().Where(l => l.Id.ToString().Contains(searchValue) || searchValue == null).ToList();
                    break;
                case "labourName":
                    //search by labour name
                    labourAssignedList = labourAssignedRepository.GetAll().Where(s => s.Labour.Name.ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
                default:
                    //by default, search by name
                    labourAssignedList = labourAssignedRepository.GetAll().Where(l => l.Id.ToString().ToLower().Contains(searchValue.ToLower()) || searchValue == null).ToList();
                    break;
            }
            return Json(labourAssignedList, JsonRequestBehavior.AllowGet);
        }
        /*LabourAssigned-List Search*/
    }
}