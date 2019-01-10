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
    public class CityController : Controller
    {
        ICity cityRepository;

        public CityController(ICity ctRepo)
        {
            cityRepository = ctRepo;
        }

        // GET: City
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CityList(int? page, int? rows)
        {
            var pageNo = page ?? 1;
            var numOfRows = rows ?? 5;

            var city = cityRepository.GetAll().OrderBy(c => c.Name).ToList().ToPagedList(pageNo, numOfRows);
            return View(city);
        }

        public ActionResult Insert()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var city = cityRepository.Get(id);
            var viewModel = new CityFormViewModel
            {
                City = city
            };
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult CreateOrUpdate(CityFormViewModel viewModel) // model binding
        {
            viewModel.City.Name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(viewModel.City.Name.ToLower());
            if (viewModel.City.Id == 0) // Create
            {
                cityRepository.Insert(viewModel.City);
                return RedirectToAction("CityList", "City");
            }
            else // Update
            {
                var city = cityRepository.Update(viewModel.City);
                return RedirectToAction("CityList", "City");
            }
        }

        public ActionResult Edit(int id)
        {
            var city = cityRepository.Get(id);

            var viewModel = new CityFormViewModel
            {
                City = city
            };
            return View(viewModel);
        }

        public ActionResult Remove(int id)
        {
            cityRepository.Delete(cityRepository.Get(id));
            return RedirectToAction("CityList", "City");
        }
    }
}