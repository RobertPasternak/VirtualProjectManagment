using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualProjectManagment.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application
        public ActionResult Menu()
        {
            return View();
        }
    }
}