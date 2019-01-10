using Interfaces;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using Entities;
using USP_Application.ViewModels;

namespace USP_Application.Controllers
{
    public class ServiceController : Controller
    {
        IService serviceRepository;

        public ServiceController(IService srRepo)
        {
            serviceRepository = srRepo;
        }

        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServiceList(int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            var services = serviceRepository.GetAll().OrderBy(s => s.ServiceName).ToList().ToPagedList(pageNo, numOfRows);
            return View(services);
        }

        public ActionResult Insert()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var service = serviceRepository.Get(id);
            var viewModel = new ServiceFormViewModel
            {
                Service = service
            };
            return View(viewModel);
            
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(ServiceFormViewModel viewModel) // model binding
        {
            viewModel.Service.ServiceName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(viewModel.Service.ServiceName.ToLower());
            if (viewModel.Service.Id == 0) // Create
            {
                serviceRepository.Insert(viewModel.Service);
                return RedirectToAction("ServiceList", "Service");
            }
            else // Update
            {
                var service = serviceRepository.Update(viewModel.Service);
                return RedirectToAction("ServiceList", "Service");
            }
        }

        public ActionResult Edit(int id)
        {
            var service = serviceRepository.Get(id);

            var viewModel = new ServiceFormViewModel
            {
                Service = service
            };
            return View(viewModel);
        }

        public ActionResult Remove(int id)
        {
            serviceRepository.Delete(serviceRepository.Get(id));
            return RedirectToAction("ServiceList", "Service");
        }
    }
}