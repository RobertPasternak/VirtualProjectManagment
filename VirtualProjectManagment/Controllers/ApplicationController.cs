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
        public ActionResult Overview(OverviewModel overviewModel)
        {
            BackendlessUser user = Backendless.UserService.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }      
            return View(overviewModel);
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
        public ActionResult AddTask(TaskModel taskModel)
        {
            BackendlessUser user = Backendless.UserService.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                taskModel.TaskAuthor = (string) user.Properties["name"] + " " + (string) user.Properties["surname"];
                taskModel.TaskCreateDate = DateTime.Now.Date;

                try
                {
                   

                    Backendless.Data.Save(taskModel);                   
                }
                catch (BackendlessException exception)
                {
                    
                    ModelState.AddModelError("", exception.ToString());
                    
                }


                ModelState.AddModelError("","Zadanie zostało dodane.");
                return RedirectToAction("Overview", "Application");
            }
            return View(taskModel);
        }
    }
}