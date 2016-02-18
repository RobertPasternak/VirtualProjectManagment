using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackendlessAPI;
using BackendlessAPI.Exception;
using VirtualProjectManagment.Models;

namespace VirtualProjectManagment.Controllers
{
    public class AccountController : Controller
    {



        public ActionResult Logout()
        {
            Backendless.UserService.Logout();
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Backendless.UserService.Login(loginModel.Login, loginModel.Password);
                    if (Backendless.UserService.IsValidLogin())
                    {
                        return RedirectToAction("Overview", "Application");
                    }
                }
                catch (BackendlessException exception)
                {
                    if (exception.FaultCode == "3003")
                    {
                        ModelState.AddModelError("", "Błędne dane logowania.");
                    }
                    else
                    {
                        ModelState.AddModelError("", exception.ToString());
                    }
                }
            }
            return View(loginModel);
        }



        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BackendlessUser newUser = new BackendlessUser();
                    newUser.SetProperty("login", registerModel.Login);
                    newUser.SetProperty("email", registerModel.Email);
                    newUser.SetProperty("name", registerModel.Name);
                    newUser.Password = registerModel.Password;
                    Backendless.UserService.Register(newUser);
                    return RedirectToAction("Login", "Account");
                }
                catch (BackendlessException exception)
                {
                    if (exception.FaultCode == "3033")
                    {
                        ModelState.AddModelError("", "Login jest zajęty.");
                    }
                    else
                    {
                        ModelState.AddModelError("", exception.ToString());
                    }
                }
            }
            return View(registerModel);
        }



        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Backendless.UserService.RestorePassword(forgotPasswordModel.Login);
                    ModelState.AddModelError("", "Nowe hasło zostało wysłane.");
                }
                catch (BackendlessException exception)
                {
                    if (exception.FaultCode == "3020")
                    {
                        ModelState.AddModelError("", "Użytkownik nie istnieje.");
                    }
                    else
                    {
                        ModelState.AddModelError("", exception.ToString());
                    }
                }
            }
            return View(forgotPasswordModel);
        }
    }
}