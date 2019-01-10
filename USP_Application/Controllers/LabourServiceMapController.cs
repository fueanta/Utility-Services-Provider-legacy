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
    public class LabourServiceMapController : Controller
    {
        ILabourServiceMap labourServiceMapRepository;
        ILabour labourRepository;
        IService serviceRepository;

        public LabourServiceMapController(ILabourServiceMap lsRepo, ILabour lRepo, IService sRepo)
        {
            labourServiceMapRepository = lsRepo;
            labourRepository = lRepo;
            serviceRepository = sRepo;
        }

        // GET: LabourServiceMap
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LabourServiceMapList(int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            var labourServiceMaps = labourServiceMapRepository.GetAll().OrderBy(l => l.Labour.Name).ToList().ToPagedList(pageNo, numOfRows);
            return View(labourServiceMaps);
        }

        public ActionResult Insert()
        {
            var labours = labourRepository.GetAll().OrderBy(l => l.Name);
            var services = serviceRepository.GetAll().OrderBy(s => s.ServiceName);
            var viewModel = new LabourServiceMapFormViewModel
            {
                Labours = labours,
                Services = services
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(LabourServiceMapFormViewModel viewModel) // model binding
        {
            if (viewModel.LabourServiceMap.Id == 0) // Create
            {
                labourServiceMapRepository.Insert(viewModel.LabourServiceMap);
                return RedirectToAction("LabourServiceMapList", "LabourServiceMap");
            }
            else // Update
            {
                var labourServiceMap = labourServiceMapRepository.Update(viewModel.LabourServiceMap);
                return RedirectToAction("LabourServiceMapList", "LabourServiceMap");
            }
        }

        public ActionResult Edit(int id)
        {
            var labourServiceMap = labourServiceMapRepository.Get(id);
            var labours = labourRepository.GetAll().OrderBy(l => l.Name);
            var services = serviceRepository.GetAll().OrderBy(s => s.ServiceName);

            if (labourServiceMap == null)
            {
                return HttpNotFound();
            }
            var viewModel = new LabourServiceMapFormViewModel
            {
                LabourServiceMap = labourServiceMap,
                Labours = labours,
                Services = services
            };
            return View(viewModel);
        }

        public ActionResult Remove(int id)
        {
            labourServiceMapRepository.Delete(labourServiceMapRepository.Get(id));
            return RedirectToAction("LabourServiceMapList", "LabourServiceMap");
        }
    }
}