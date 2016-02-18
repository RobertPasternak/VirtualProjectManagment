using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackendlessAPI;
using VirtualProjectManagment.Models;

namespace VirtualProjectManagment.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application
        public ActionResult Overview()
        {
            BackendlessUser user = Backendless.UserService.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}