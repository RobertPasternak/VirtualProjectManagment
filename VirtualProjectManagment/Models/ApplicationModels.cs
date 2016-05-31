using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Configuration;
using BackendlessAPI;
using BackendlessAPI.Data;
using BackendlessAPI.Persistence;
using BackendlessAPI.Property;
using VirtualProjectManagment.Services;
using Weborb.Service;

namespace VirtualProjectManagment.Models
{
    public class OverviewModel
    {
        public OverviewModel()
        {
            BackendlessUser user = Backendless.UserService.CurrentUser;
            TaskRepository taskRepo = new TaskRepository();

            ListOfAllTasks = taskRepo.GetListOfObjectsFromTable("TaskName LIKE '%'");
            ListOfUserTasks = new List<TaskModel>();

            foreach (var task in ListOfAllTasks)
            {
                TotalNumberOfTasks++;
                if (task.TaskStatus == "Otwarte")
                    NumberOfTasksWithStatusOpen++;
                if (task.TaskStatus == "W Trakcie")
                    NumberOfTasksWithStatusInProgress++;
                if (task.TaskStatus == "Przegląd Kodu")
                    NumberOfTasksWithStatusCodeReview++;
                if (task.TaskStatus == "Zakończone")
                    NumberOfTasksWithStatusDone++;
                if (task.TaskPriority == "Krytyczny")
                    NumberOfTasksWithPriorityCritical++;
                if (task.TaskPriority == "Bardzo Wysoki")
                    NumberOfTasksWithPriorityVeryHigh++;
                if (task.TaskPriority == "Wysoki")
                    NumberOfTasksWithPriorityHigh++;
                if (task.TaskPriority == "Średni")
                    NumberOfTasksWithPriorityMedium++;
                if (task.TaskPriority == "Niski")
                    NumberOfTasksWithPriorityLow++;                
                if (user != null && task.TaskAssignedToUser == (user.Properties["name"] + " " + user.Properties["surname"]))
                {
                    NumberOfTasksAssignedToUser++;
                    ListOfUserTasks.Add(task);
                }

            }
        }

        public int NumberOfTasksAssignedToUser { get; set; }

        public int TotalNumberOfTasks { get; set; }

        public int NumberOfTasksWithPriorityCritical { get; set; }
        public int NumberOfTasksWithPriorityVeryHigh { get; set; }
        public int NumberOfTasksWithPriorityHigh { get; set; }
        public int NumberOfTasksWithPriorityMedium { get; set; }
        public int NumberOfTasksWithPriorityLow { get; set; }

        public int NumberOfTasksWithStatusOpen { get; set; }
        public int NumberOfTasksWithStatusInProgress { get; set; }
        public int NumberOfTasksWithStatusCodeReview { get; set; }
        public int NumberOfTasksWithStatusDone { get; set; }

        public List<TaskModel> ListOfAllTasks { get; set; }

        public List<TaskModel> ListOfUserTasks { get; set; }
    }

    public class TaskModel
    {
        public string objectId { get; set; } //Must be lowercase. Backendless DB is case sensitive :)

        [Required]
        [Display(Name = "Priorytet Zadania")]
        public string TaskPriority { get; set; }

        [Required]
        [Display(Name = "Nazwa Zadania")]
        public string TaskName { get; set; }

        [Display(Name = "Data Stworzenia")]
        [DataType(DataType.Date)]
        public DateTime TaskCreateDate { get; set; }


        [Required]
        [Display(Name = "Termin Zakończenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TaskDueDate {get;set;}

        [Display(Name = "Autor")]
        public string TaskAuthor { get; set; }

        [Required]
        [Display(Name = "Przypisane do")]
        public string TaskAssignedToUser { get; set; }

        [Required]
        [Display(Name = "Status Zadania")]
        public string TaskStatus { get; set; }

        [Required]
        [Display(Name = "Opis Zadania")]
        public string TaskDescription { get; set; }

        public IEnumerable<Users> UsersList { get; set; }

        
        [Display(Name = "Komentarz")]
        public string Comment { get; set; }

        public IEnumerable<Comments> CommentsList { get; set; }


    }

    public class OverviewByStatus
    {
        TaskRepository taskRepo = new TaskRepository();

        public OverviewByStatus(string status)
        {
            ListOfTasksByStatus = taskRepo.GetListOfObjectsFromTable("TaskStatus LIKE '" + status + "'");
            Status = status;
        }

        public string Status { get; set; }

        public List<TaskModel> ListOfTasksByStatus { get; set; }
    }

    public class OverviewByPriority
    {
        TaskRepository taskRepo = new TaskRepository();
        public OverviewByPriority(string priority)
        {
            ListOfTasksByPriority = taskRepo.GetListOfObjectsFromTable("TaskPriority LIKE '" + priority + "'");
            Priority = priority;
        }

        public string Priority { get; set; }

        public List<TaskModel> ListOfTasksByPriority { get; set; }
    }


}