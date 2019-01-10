using Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP_Application.ViewModels;

namespace USP_Application.Controllers
{
    public class ServiceRequestController : Controller
    {
        IServiceRequest serviceRequestRepository;
        IClient clientRepository;
        IService serviceRepository;

        public ServiceRequestController(IServiceRequest srRepo, IClient cRepo, IService sRepo)
        {
            serviceRequestRepository = srRepo;
            clientRepository = cRepo;
            serviceRepository = sRepo;
        }

        // GET: ServiceRequest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServiceRequestList(int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            var serviceRequests = serviceRequestRepository.GetAll().OrderBy(s => s.Client.Name).ToList().ToPagedList(pageNo, numOfRows);
            return View(serviceRequests);
        }

        public ActionResult Insert()
        {
            var clients = clientRepository.GetAll().OrderBy(l => l.Name);
            var services = serviceRepository.GetAll().OrderBy(s => s.ServiceName);
            var viewModel = new ServiceRequestFormViewModel
            {
                Clients = clients,
                Services = services
            };
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            
            var serviceRequest = serviceRequestRepository.Get(id);
            var viewModel = new ServiceRequestFormViewModel
            {
                ServiceRequest = serviceRequest
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(ServiceRequestFormViewModel viewModel) // model binding
        {
            if (viewModel.ServiceRequest.Id == 0) // Create
            {
                var fakeId = int.Parse(Request.Cookies["LoginId"].Value);
                var id = clientRepository.Get(fakeId).Id;

                viewModel.ServiceRequest.ClientId = id;
                viewModel.ServiceRequest.Status = Entities.Status.Pending;
                serviceRequestRepository.Insert(viewModel.ServiceRequest);
                return RedirectToAction("Index", "Client");
            }
            else // Update
            {
                var serviceRequest = serviceRequestRepository.Update(viewModel.ServiceRequest);
                return RedirectToAction("ServiceRequestList", "ServiceRequest");
            }
        }

        public ActionResult Edit(int id)
        {
            var serviceRequest = serviceRequestRepository.Get(id);
            var clients = clientRepository.GetAll().OrderBy(l => l.Name);
            var services = serviceRepository.GetAll().OrderBy(s => s.ServiceName);

            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ServiceRequestFormViewModel
            {
                ServiceRequest = serviceRequest,
                Clients = clients,
                Services = services
            };
            return View(viewModel);
        }

        public ActionResult Remove(int id)
        {
            serviceRequestRepository.Delete(serviceRequestRepository.Get(id));
            return RedirectToAction("ServiceRequestList", "ServiceRequest");
        }
    }
}