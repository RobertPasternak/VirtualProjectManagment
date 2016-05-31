using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BackendlessAPI;
using BackendlessAPI.Data;
using BackendlessAPI.Exception;
using BackendlessAPI.Persistence;
using VirtualProjectManagment.Models;
using VirtualProjectManagment.Services;

namespace VirtualProjectManagment.Controllers
{
    public class ApplicationController : Controller
    {
        private TaskRepository taskRepo = new TaskRepository();
        private CommentRepository comRepo = new CommentRepository();
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
        public ActionResult TaskAdd()
        {
            BackendlessUser user = Backendless.UserService.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult TaskAdd(TaskModel taskModel)
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


        public ActionResult TaskDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BackendlessUser user = Backendless.UserService.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            TaskModel task = taskRepo.GetObjectsFromTable("objectId LIKE '" + id + "'").First();
            task.CommentsList = comRepo.GetObjectsFromTable("TaskId LIKE '" + id + "'");
            TempData["TaskID"] = id;
            return View(task);
        }

        [HttpPost]
        public ActionResult TaskDetails(TaskModel taskModel)
        {
            BackendlessUser user = Backendless.UserService.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

                comRepo.AddComment((string) TempData["TaskID"], (string) user.Properties["name"],
                    (string) user.Properties["surname"], taskModel.Comment, true);

            return RedirectToAction("TaskDetails", taskModel.objectId);

        }

        public ActionResult TaskRemove(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel task = taskRepo.GetObjectsFromTable("objectId LIKE '" + id + "'").First();
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("TaskRemove")]
        [ValidateAntiForgeryToken]
        public ActionResult RaskremoveConfirmed(string id)
        {
            TaskModel task = taskRepo.GetObjectsFromTable("objectId LIKE '" + id + "'").First();
            Backendless.Persistence.Of<TaskModel>().Remove(task);
            return RedirectToAction("Overview", "Application");
        }

        public ActionResult EditComment(string id)
        {
            return RedirectToAction("Overview", "Application");
        }

        public ActionResult RemoveComment(string id)
        {
            comRepo.RemoveComment(id);
            return RedirectToAction("Overview", "Application");
        }
    }
}