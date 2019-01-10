using Interfaces;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using USP_Application.Models;
using USP_Application.ViewModels;

namespace USP_Application.Controllers
{
    public class LabourController : Controller
    {
        ILabour labourRepository;
        ILabourServiceMap labourServiceMapRepository;
        IUserLogin userLoginRepository;
        IService serviceRepository;
        ICity cityRepository;
        IArea areaRepository;

        public LabourController(ILabour labRepo, ILabourServiceMap labSerMap, IUserLogin ulRepo, IService srRepo, ICity ctRepo, IArea arRepo)
        {
            labourRepository = labRepo;
            labourServiceMapRepository = labSerMap;
            userLoginRepository = ulRepo;
            serviceRepository = srRepo;
            cityRepository = ctRepo;
            areaRepository = arRepo;
        }

        // GET: Labour
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LabourList(int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            var labours = labourRepository.GetAll();
            var userLogins = userLoginRepository.GetAll();

            var query =
                from userLogin in userLogins
                join labour in labours on userLogin.Id equals labour.FakeId
                select new LabourUserLoginModel
                {
                    Id = labour.FakeId,
                    Name = labour.Name,
                    Phone = userLogin.Phone,
                    Email = userLogin.Email,
                    Wallet = labour.Wallet,
                    City = labour.City.Name,
                    Area = labour.Area.Name
                };
            return View(query.ToList().ToPagedList(pageNo, numOfRows));
        }

        public ActionResult Insert()
        {
            var cities = cityRepository.GetAll().OrderBy(c => c.Name);
            var services = serviceRepository.GetAll().OrderBy(s => s.ServiceName);
            var viewModel = new LabourFormViewModel
            {
                Cities = cities,
                Services = services
            };
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var labour = labourRepository.Get(id);
            var userLogin = userLoginRepository.Get(id);
            var viewModel = new LabourFormViewModel
            {
                Labour = labour,
                UserLogin = userLogin
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(LabourFormViewModel viewModel) // model binding
        {
            viewModel.Labour.Name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(viewModel.Labour.Name.ToLower());
            if (viewModel.UserLogin.Id == 0) // Create
            {
                viewModel.Labour.JoiningDate = DateTime.Now;
                viewModel.UserLogin.UserType = Entities.UserType.Labour;
                
                userLoginRepository.Insert(viewModel.UserLogin);
                viewModel.Labour.FakeId = viewModel.UserLogin.Id;
                labourRepository.Insert(viewModel.Labour);
                viewModel.LabourServiceMap.LabourId = viewModel.Labour.Id;
                labourServiceMapRepository.Insert(viewModel.LabourServiceMap);            

                return RedirectToAction("LabourList", "Labour");
            }
            else // Update
            {
                var labour = labourRepository.Update(viewModel.Labour);
                var userLogin = userLoginRepository.Update(viewModel.UserLogin);
                return RedirectToAction("Details", "Labour", new { id = viewModel.Labour.FakeId });
            }
        }

        public ActionResult Edit(int id)
        {
            var labour = labourRepository.Get(id);
            var userLogin = userLoginRepository.Get(id);
            var cities = cityRepository.GetAll().OrderBy(c => c.Name);
            var areas = areaRepository.GetAreasByCityId(labour.CityId).OrderBy(c => c.Name);
            var services = serviceRepository.GetAll().OrderBy(s => s.ServiceName);


            if (labour == null)
            {
                return HttpNotFound();
            }
            var viewModel = new LabourFormViewModel
            {
                Labour = labour,
                UserLogin = userLogin,
                Cities = cities,
                Areas = areas,
                Services = services
            };
            return View(viewModel);
        }

        public ActionResult Remove(int id)
        {
            labourRepository.Delete(labourRepository.Get(id));
            userLoginRepository.Delete(userLoginRepository.Get(id));
            return RedirectToAction("LabourList", "Labour");
        }
    }
}