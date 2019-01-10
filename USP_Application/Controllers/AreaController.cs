using Interfaces;
using System.Web.Mvc;
using System.Linq;
using PagedList;
using System;
using System.Collections.Generic;
using Entities;
using USP_Application.ViewModels;

namespace USP_Application.Controllers
{
    public class AreaController : Controller
    {
        IArea areaRepository;
        ICity cityRepository;

        public AreaController(IArea arRepo, ICity ctRepo)
        {
            areaRepository = arRepo;
            cityRepository = ctRepo;
        }

        // GET: Area
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AreaList(int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            var areas = areaRepository.GetAll().OrderBy(a => a.City.Name).ToList().ToPagedList(pageNo, numOfRows);
            return View(areas);
        }

        public ActionResult Insert()
        {
            var cities = cityRepository.GetAll().OrderBy(c => c.Name);
            var viewModel = new AreaFormViewModel
            {
                Cities = cities
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateOrUpdate(AreaFormViewModel viewModel) // model binding
        {
            viewModel.Area.Name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(viewModel.Area.Name.ToLower());
            if (viewModel.Area.Id == 0) // Create
            {
                areaRepository.Insert(viewModel.Area);
                return RedirectToAction("AreaList", "Area");
            }
            else // Update
            {
                var area = areaRepository.Update(viewModel.Area);
                return RedirectToAction("AreaList", "Area");
            }
        }

        public ActionResult Edit(int id)
        {
            var area = areaRepository.Get(id);
            var cities = cityRepository.GetAll().OrderBy(c => c.Name);

            if (area == null)
            {
                return HttpNotFound();
            }
            var viewModel = new AreaFormViewModel
            {
                Area = area,
                Cities = cities
            };
            return View(viewModel);
        }

        public ActionResult Remove(int id)
        {
            areaRepository.Delete(areaRepository.Get(id));
            return RedirectToAction("AreaList", "Area");
        }
    }
}