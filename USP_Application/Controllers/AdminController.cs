using Entities;
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
    public class AdminController : Controller
    {
        IAdmin adminRepository;
        IUserLogin userLoginRepository;

        public AdminController(IAdmin adRepo, IUserLogin ulRepo)
        {
            adminRepository = adRepo;
            userLoginRepository = ulRepo;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult AdminList(int? page, int? rows)
        //{
        //    var pageNo = page ?? 1;
        //    var numOfRows = rows ?? 5;

        //    var admin = adminRepository.GetAll().OrderBy(a => a.Name).ToList().ToPagedList(pageNo, numOfRows);
        //    return View(admin);
        //}

        //public ActionResult Insert()
        //{
        //    return View();
        //}

        //public ActionResult Details(int id)
        //{
        //    var userLogin = userLoginRepository.Get(id);
        //    return View(userLogin);

        //}

        [HttpPost]
        public ActionResult CreateOrUpdate(UserLogin userLogin) // model binding
        {
            if (userLogin.Id == 0) // Create
            {
                userLogin.UserType = Entities.UserType.Admin;
                userLoginRepository.Insert(userLogin);
                return RedirectToAction("Index", "Admin");
            }
            else // Update
            {
                var id = Request.Cookies["LoginId"].Value;
                userLoginRepository.Update(userLogin);
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult Edit(int id)
        {
            var userLogin = userLoginRepository.Get(id);
            return View(userLogin);
        }

        public ActionResult Remove(int id)
        {
            adminRepository.Delete(adminRepository.Get(id));
            return RedirectToAction("AdminList", "Admin");
        }
    }
}