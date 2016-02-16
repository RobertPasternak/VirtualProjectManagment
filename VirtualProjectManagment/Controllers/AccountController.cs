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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountModels accountModels)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    Backendless.UserService.Login(accountModels.Login, accountModels.Password);
                    if (Backendless.UserService.IsValidLogin())
                    {
                        return RedirectToAction("Menu", "Application");
                    }
                }
                catch (BackendlessException exception)
                {
                    if (exception.FaultCode == "3003")
                    {
                        ModelState.AddModelError("", "Błędne dane logowania");
                    }
                    else
                    {
                        ModelState.AddModelError("", exception.ToString());
                    }
                }

            }
            return View(accountModels);
        }

        public ActionResult Logout()
        {
            Backendless.UserService.Logout();
            return RedirectToAction("Index", "Home");
        }


    }
}