using Interfaces;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using USP_Application.Models;
using USP_Application.ViewModels;

namespace USP_Application.Controllers
{
    public class ClientController : Controller
    {
        IServiceRequest serviceRequestRepository;
        IClient clientRepository;
        IUserLogin userLoginRepository;
        ICity cityRepository;
        IArea areaRepository;

        public ClientController(IServiceRequest srRepo, IClient clRepo, IUserLogin ulRepo, ICity ctRepo, IArea arRepo)
        {
            serviceRequestRepository = srRepo;
            clientRepository = clRepo;
            userLoginRepository = ulRepo;
            cityRepository = ctRepo;
            areaRepository = arRepo;
        }

        // GET: Client
        public ActionResult Index(int? page, int? rows) // id should not be nullable
        {
            if (Request.Cookies["LoginId"] != null)
            {
                var fakeId = Int32.Parse(Request.Cookies["LoginId"].Value);
                var id = clientRepository.Get(fakeId).Id;

                var pageNo = page ?? 1;
                var numOfRows = rows ?? 5;

                var serviceRequests = serviceRequestRepository.GetAll().Where(c=> c.ClientId == id).OrderByDescending(sr => sr.ServiceTime).ToList().ToPagedList(pageNo, numOfRows);
                return View(serviceRequests);
            }
            return HttpNotFound();
        }

        public ActionResult CLientList(int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            var clients = clientRepository.GetAll();
            var userLogins = userLoginRepository.GetAll();

            var query =
                from userLogin in userLogins
                join client in clients on userLogin.Id equals client.FakeId
                select new ClientUserLoginModel
                {
                    Id = client.FakeId,
                    Name = client.Name,
                    Phone = userLogin.Phone,
                    Email = userLogin.Email,
                    City = client.City.Name,
                    Area = client.Area.Name
                };
            return View(query.ToList().ToPagedList(pageNo, numOfRows));
        }

        public ActionResult Insert()
        {
            var cities = cityRepository.GetAll().OrderBy(c => c.Name);
            var viewModel = new ClientFormViewModel
            {
                Cities = cities
            };
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var client = clientRepository.Get(id);
            var userLogin = userLoginRepository.Get(id);
            var viewModel = new ClientFormViewModel
            {
                Client = client,
                UserLogin = userLogin
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(ClientFormViewModel viewModel) // model binding
        {
            viewModel.Client.Name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(viewModel.Client.Name.ToLower());
            if (viewModel.UserLogin.Id == 0) // Create
            {
                viewModel.Client.JoiningDate = DateTime.Now;
                viewModel.UserLogin.UserType = Entities.UserType.Client;

                userLoginRepository.Insert(viewModel.UserLogin);
                viewModel.Client.FakeId = viewModel.UserLogin.Id;
                clientRepository.Insert(viewModel.Client);
                
                return RedirectToAction("Index", "Client");
            }
            else // Update
            {
                var client = clientRepository.Update(viewModel.Client);
                var userLogin = userLoginRepository.Update(viewModel.UserLogin);
                return RedirectToAction("Details", "Client", new { id = viewModel.Client.FakeId });
            }
        }

        public ActionResult Edit(int id)
        {
            var client = clientRepository.Get(id);
            var userLogin = userLoginRepository.Get(id);
            var cities = cityRepository.GetAll().OrderBy(c => c.Name);
            var areas = areaRepository.GetAreasByCityId(client.CityId).OrderBy(a => a.Name);

            if (client == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ClientFormViewModel
            {
                Client = client,
                UserLogin = userLogin,
                Cities = cities,
                Areas = areas
            };
            return View(viewModel);
        }

        public ActionResult Remove(int id)
        {
            clientRepository.Delete(clientRepository.Get(id));
            userLoginRepository.Delete(userLoginRepository.Get(id));
            return RedirectToAction("ClientList", "Client");
        }
    }
}