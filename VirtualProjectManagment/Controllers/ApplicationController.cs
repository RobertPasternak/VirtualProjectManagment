using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackendlessAPI;
using BackendlessAPI.Data;
using BackendlessAPI.Exception;
using BackendlessAPI.Persistence;
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

        [HttpGet]
        public ActionResult AddTask()
        {
            BackendlessUser user = Backendless.UserService.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddTask(AddTaskModel addTaskModel)
        {
            BackendlessUser user = Backendless.UserService.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                addTaskModel.TaskAuthor = (string) user.Properties["name"] + " " + (string) user.Properties["surname"];
                addTaskModel.TaskCreateDate = DateTime.Now.Date;

                try
                {
                    Backendless.Data.Save(addTaskModel);                   
                }
                catch (BackendlessException exception)
                {
                    
                    ModelState.AddModelError("", exception.ToString());
                    
                }


                ModelState.AddModelError("","Zadanie zostało dodane.");
                return RedirectToAction("Overview", "Application");
            }
            return View(addTaskModel);
        }
    }
}