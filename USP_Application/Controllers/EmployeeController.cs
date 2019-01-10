using Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP_Application.Models;
using USP_Application.ViewModels;

namespace USP_Application.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployee employeeRepository;
        IUserLogin userLoginRepository;
        IServiceRequest serviceRequestRepository;

        public EmployeeController(IEmployee emRepo, IUserLogin ulRepo, IServiceRequest srRepo)
        {
            employeeRepository = emRepo;
            userLoginRepository = ulRepo;
            serviceRequestRepository = srRepo;
        }

        // GET: Employee
        public ActionResult Index(int? page, int? rows)
        {
            if (Request.Cookies["LoginId"] != null)
            {
                var pageNo = page ?? 1;
                var numOfRows = rows ?? 5;

                var serviceRequests = serviceRequestRepository.GetAll().OrderByDescending(sr => sr.ServiceTime).ToList().ToPagedList(pageNo, numOfRows);
                return View(serviceRequests);
            }
            return HttpNotFound();
        }

        public ActionResult EmployeeList(int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            var employees = employeeRepository.GetAll();
            var userLogins = userLoginRepository.GetAll();

            var query =
                from userLogin in userLogins
                join employee in employees on userLogin.Id equals employee.FakeId
                select new EmployeeUserLoginModel
                {
                    Id = employee.FakeId,
                    Name = employee.Name,
                    Phone = userLogin.Phone,
                    Email = userLogin.Email,
                    Salary = employee.Salary,
                    Commission = employee.Commission
                };
            return View(query.ToList().ToPagedList(pageNo, numOfRows));
        }

        public ActionResult Insert()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var employee = employeeRepository.Get(id);
            var userLogin = userLoginRepository.Get(id);
            var viewModel = new EmployeeFormViewModel
            {
                Employee = employee,
                UserLogin = userLogin
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(EmployeeFormViewModel viewModel) // model binding
        {
            viewModel.Employee.Name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(viewModel.Employee.Name.ToLower());
            if (viewModel.UserLogin.Id == 0) // Create
            {
                viewModel.Employee.JoiningDate = DateTime.Now;
                //viewModel.Employee.Id = userLoginRepository.GetAll().Count() + 1;
                //var generalId = userLoginRepository.GetAll().Count() + 1;
                //viewModel.Employee.Id = generalId;
                //viewModel.UserLogin.Id = generalId;
                viewModel.UserLogin.UserType = Entities.UserType.Employee;

                userLoginRepository.Insert(viewModel.UserLogin);

                viewModel.Employee.FakeId = viewModel.UserLogin.Id;
                employeeRepository.Insert(viewModel.Employee);
                
                return RedirectToAction("EmployeeList", "Employee");
            }
            else // Update
            {
                if(Request.Cookies["LoginType"].Value.Equals("Employee"))
                {
                    return RedirectToAction("Index", "Employee");
                }

                var employee = employeeRepository.Update(viewModel.Employee);
                var userLogin = userLoginRepository.Update(viewModel.UserLogin);
                return RedirectToAction("Details", "Employee", new { id = viewModel.Employee.FakeId });
            }
        }

        public ActionResult Edit(int id)
        {
            var employee = employeeRepository.Get(id);
            var userLogin = userLoginRepository.Get(id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            var viewModel = new EmployeeFormViewModel
            {
                Employee = employee,
                UserLogin = userLogin
            };
            return View(viewModel);
        }

        public ActionResult Remove(int id)
        {
            employeeRepository.Delete(employeeRepository.Get(id));
            userLoginRepository.Delete(userLoginRepository.Get(id));
            return RedirectToAction("EmployeeList", "Employee");
        }
    }
}