using Data;
using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP_Application.ViewModels;

namespace USP_Application.Controllers
{
    public class HomeController : Controller
    {
        IUserLogin userLoginRepository;
        IClient clientRepository;
        IEmployee employeeRepository;
        ICity cityRepository;
        public HomeController(IUserLogin ulRepo, IClient clRepo, IEmployee emRepo, ICity ctRepo)
        {
            userLoginRepository = ulRepo;
            clientRepository = clRepo;
            employeeRepository = emRepo;
            cityRepository = ctRepo;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            return RedirectToAction("Insert", "Client");
        }

        public ActionResult Login()
        {
            var viewModel = new LoginFormViewModel();
            viewModel.UserLogin = new UserLogin();

            if (Request.Cookies["LoginEmail"] != null)
            {
                // populate data from cookie
                viewModel.UserLogin.Email = Request.Cookies["LoginEmail"].Value;
                viewModel.UserLogin.Password = Request.Cookies["LoginPassword"].Value;
                viewModel.RememberMe = true;
            }

            return View(viewModel);
        }

        public ActionResult Logout()
        {
            HttpCookie loginIdCookie = Request.Cookies["LoginId"];
            loginIdCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(loginIdCookie);

            HttpCookie loginTypeCookie = Request.Cookies["LoginType"];
            loginTypeCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(loginTypeCookie);

            if (Request.Cookies["LoginName"] != null)
            {
                HttpCookie loginNameCookie = Request.Cookies["LoginName"];
                loginNameCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(loginNameCookie);
            }   

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LogInSubmit(LoginFormViewModel viewModel)
        {
            var user = userLoginRepository.GetAll().FirstOrDefault(u => u.Email.Equals(viewModel.UserLogin.Email) && u.Password.Equals(viewModel.UserLogin.Password));

            if (user != null)
            {
                // store cookie for 1 day
                HttpCookie loginIdCookie = new HttpCookie("LoginId");
                loginIdCookie.Expires = DateTime.Now.AddDays(1d);
                loginIdCookie.Value = user.Id.ToString();
                Response.Cookies.Add(loginIdCookie);

                HttpCookie loginTypeCookie = new HttpCookie("LoginType");
                loginTypeCookie.Expires = DateTime.Now.AddDays(1d);
                loginTypeCookie.Value = user.UserType.ToString();
                Response.Cookies.Add(loginTypeCookie);

                if (viewModel.RememberMe)
                {
                    // store cookie for 1 day
                    HttpCookie loginEmailCookie = new HttpCookie("LoginEmail");
                    loginEmailCookie.Expires = DateTime.Now.AddDays(1d);
                    loginEmailCookie.Value = user.Email;
                    Response.Cookies.Add(loginEmailCookie);

                    HttpCookie loginPasswordCookie = new HttpCookie("LoginPassword");
                    loginPasswordCookie.Expires = DateTime.Now.AddDays(1d);
                    loginPasswordCookie.Value = user.Password;
                    Response.Cookies.Add(loginPasswordCookie);
                }
                else
                {
                    // delete cookie
                    if (Request.Cookies["LoginEmail"] != null)
                    {
                        HttpCookie loginEmailCookie = Request.Cookies["LoginEmail"];
                        loginEmailCookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(loginEmailCookie);

                        HttpCookie loginPasswordCookie = Request.Cookies["LoginPassword"];
                        loginPasswordCookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(loginPasswordCookie);
                    }
                }

                switch (user.UserType)
                {
                    case UserType.Client:
                        // logged in user is client

                        HttpCookie loginNameCookie = new HttpCookie("LoginName");
                        loginNameCookie.Expires = DateTime.Now.AddDays(1d);
                        loginNameCookie.Value = clientRepository.Get(user.Id).Name;
                        Response.Cookies.Add(loginNameCookie);

                        return RedirectToAction("Index", "Client");
                    case UserType.Employee:
                        // logged in user is employee

                        HttpCookie loginEmpNameCookie = new HttpCookie("LoginName");
                        loginEmpNameCookie.Expires = DateTime.Now.AddDays(1d);
                        loginEmpNameCookie.Value = employeeRepository.Get(user.Id).Name;
                        Response.Cookies.Add(loginEmpNameCookie);

                        return RedirectToAction("Index", "Employee");
                    case UserType.Labour:
                        // logged in user is labour
                        return RedirectToAction("Index", "Labour");
                    default:
                        // logged in user is admin
                        return RedirectToAction("Index", "Admin");
                }
            }

            viewModel.Invalid = true;
            return View("Login", viewModel);
        }
    }
}