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
    public class LabourAssignedController : Controller
    {
        ILabourAssigned labourAssignedRepository;
        ILabour labourRepository;
        IServiceRequest serviceRequestRepository;
        IEmployee employeeRepository;

        public LabourAssignedController(ILabourAssigned laRepo, ILabour lRepo, IServiceRequest srRepo, IEmployee emRepo)
        {
            labourAssignedRepository = laRepo;
            labourRepository = lRepo;
            serviceRequestRepository = srRepo;
            employeeRepository = emRepo;
        }

        // GET: LabourAssigned
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LabourAssignedList(int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            var labourAssigneds = labourAssignedRepository.GetAll().OrderBy(l => l.Labour.Name).ToList().ToPagedList(pageNo, numOfRows);
            return View(labourAssigneds);
        }

        public ActionResult Insert()
        {
            var labours = labourRepository.GetAll().OrderBy(l => l.Name);
            var serviceRequests = serviceRequestRepository.GetAll().OrderBy(s => s.Id);
            var employees = employeeRepository.GetAll().OrderBy(e => e.Name);

            var viewModel = new LabourAssignedFormViewModel
            {
                Labours = labours,
                ServiceRequests = serviceRequests,
                Employees = employees
            };
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {

            var labourAssigned = labourAssignedRepository.Get(id);
            var viewModel = new LabourAssignedFormViewModel
            {
                LabourAssigned = labourAssigned
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(LabourAssignedFormViewModel viewModel) // model binding
        {
            if (viewModel.LabourAssigned.Id == 0) // Create
            {
                labourAssignedRepository.Insert(viewModel.LabourAssigned);
                return RedirectToAction("LabourAssignedList", "LabourAssigned");
            }
            else // Update
            {
                var labourAssigned = labourAssignedRepository.Update(viewModel.LabourAssigned);
                return RedirectToAction("LabourAssignedList", "LabourAssigned");
            }
        }

        public ActionResult Edit(int id)
        {
            var labourAssigned = labourAssignedRepository.Get(id);
            var labours = labourRepository.GetAll().OrderBy(l => l.Name);
            var employees = employeeRepository.GetAll().OrderBy(l => l.Name);
            var serviceRequests = serviceRequestRepository.GetAll().OrderBy(s => s.Id);

            if (labourAssigned == null)
            {
                return HttpNotFound();
            }
            var viewModel = new LabourAssignedFormViewModel
            {
                LabourAssigned = labourAssigned,
                Labours = labours,
                Employees = employees,
                ServiceRequests = serviceRequests
            };
            return View(viewModel);
        }

        public ActionResult Remove(int id)
        {
            labourAssignedRepository.Delete(labourAssignedRepository.Get(id));
            return RedirectToAction("LabourAssignedList", "LabourAssigned");
        }
    }
}