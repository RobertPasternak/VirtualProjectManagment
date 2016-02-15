using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                if (accountModels.Login == "123" && accountModels.Password == "123")
                {                  
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Błędne dane logowania");
            }
            return View(accountModels);
        }
    }
}