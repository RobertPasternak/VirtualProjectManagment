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
using Weborb.Service;

namespace VirtualProjectManagment.Models
{
    public class OverviewModel
    {
        public OverviewModel()
        {
            BackendlessUser user = Backendless.UserService.CurrentUser;

            /*
            m_nStart = Environment.TickCount;
            Timer oTimer = new Timer();
            oTimer.Elapsed += new ElapsedEventHandler(OnTimeEvent);
            oTimer.Interval = 10;
            oTimer.Enabled = true;

            
            if (user != null)
            {
                NumberOfTaskAssignedToUser =
                    GetNumberOfObjectsFromTable("TaskAssignedToUser = '" +
                                                (string) user.Properties["name"] + " " +
                                                (string) user.Properties["surname"] + "'");
            }

            TotalNumberOfTasks = GetNumberOfObjectsFromTable("TaskName LIKE '%'");

            NumberOfTaskWithStatusOpen = GetNumberOfObjectsFromTable("TaskStatus = 'Otwarte'");
            NumberOfTaskWithStatusInProgress = GetNumberOfObjectsFromTable("TaskStatus = 'W Trakcie'");
            NumberOfTaskWithStatusCodeReview = GetNumberOfObjectsFromTable("TaskStatus = 'Przegląd Kodu'");
            NumberOfTaskWithStatusDone = GetNumberOfObjectsFromTable("TaskStatus = 'Zakończone'");

            NumberOfTaskWithPriorityCritical = GetNumberOfObjectsFromTable("TaskPriority = 'Krytyczny'");
            NumberOfTaskWithPriorityVeryHigh = GetNumberOfObjectsFromTable("TaskPriority = 'Bardzo Wysoki'");
            NumberOfTaskWithPriorityHigh = GetNumberOfObjectsFromTable("TaskPriority = 'Wysoki'");
            NumberOfTaskWithPriorityMedium = GetNumberOfObjectsFromTable("TaskPriority = 'Średni'");
            NumberOfTaskWithPriorityLow = GetNumberOfObjectsFromTable("TaskPriority = 'Niski'");


            oTimer.Stop();
            */

            
            List<Task> tasks = new List<Task>();


            if (user != null)
            {


                tasks.Add(Task.Run(() => { NumberOfTaskAssignedToUser = GetNumberOfObjectsFromTable("TaskAssignedToUser = '" +
                                         (string)user.Properties["name"] + " " +
                                         (string)user.Properties["surname"] + "'"); }));

                tasks.Add(Task.Run(() => { ListOfTasks = GetObjectsFromTable("TaskAssignedToUser = '" +
                                         (string)user.Properties["name"] + " " +
                                         (string)user.Properties["surname"] + "'"); }));
            }

            tasks.Add(Task.Run(() => { TotalNumberOfTasks = GetNumberOfObjectsFromTable("TaskName LIKE '%'"); }));

            tasks.Add(Task.Run(() => { NumberOfTaskWithStatusOpen = GetNumberOfObjectsFromTable("TaskStatus = 'Otwarte'"); }));
            tasks.Add(Task.Run(() => { NumberOfTaskWithStatusInProgress = GetNumberOfObjectsFromTable("TaskStatus = 'W Trakcie'"); }));
            tasks.Add(Task.Run(() => { NumberOfTaskWithStatusCodeReview = GetNumberOfObjectsFromTable("TaskStatus = 'Przegląd Kodu'"); }));
            tasks.Add(Task.Run(() => { NumberOfTaskWithStatusDone = GetNumberOfObjectsFromTable("TaskStatus = 'Zakończone'"); }));

            tasks.Add(Task.Run(() => { NumberOfTaskWithPriorityCritical = GetNumberOfObjectsFromTable("TaskPriority = 'Krytyczny'"); }));
            tasks.Add(Task.Run(() => { NumberOfTaskWithPriorityVeryHigh = GetNumberOfObjectsFromTable("TaskPriority = 'Bardzo Wysoki'"); }));
            tasks.Add(Task.Run(() => { NumberOfTaskWithPriorityHigh = GetNumberOfObjectsFromTable("TaskPriority = 'Wysoki'"); }));
            tasks.Add(Task.Run(() => { NumberOfTaskWithPriorityMedium = GetNumberOfObjectsFromTable("TaskPriority = 'Średni'"); }));
            tasks.Add(Task.Run(() => { NumberOfTaskWithPriorityLow = GetNumberOfObjectsFromTable("TaskPriority = 'Niski'"); }));



            Task.WaitAll(tasks.ToArray());
           
        }


        /*
        private int m_nStart = 0;



        private void OnTimeEvent(object oSource,
            ElapsedEventArgs oElapsedEventArgs)
        {
            System.Diagnostics.Debug.WriteLine("Upłyneło {0} milisekud",
                Environment.TickCount - m_nStart);
        }

    */



        public int NumberOfTaskAssignedToUser { get; set; }

        public int TotalNumberOfTasks { get; set; }

        public int NumberOfTaskWithPriorityCritical { get; set; }
        public int NumberOfTaskWithPriorityVeryHigh { get; set; }
        public int NumberOfTaskWithPriorityHigh { get; set; }
        public int NumberOfTaskWithPriorityMedium { get; set; }
        public int NumberOfTaskWithPriorityLow { get; set; }

        public int NumberOfTaskWithStatusOpen { get; set; }
        public int NumberOfTaskWithStatusInProgress { get; set; }
        public int NumberOfTaskWithStatusCodeReview { get; set; }
        public int NumberOfTaskWithStatusDone { get; set; }

        public List<TaskModel> ListOfTasks { get; set; }

        public List<TaskModel> GetObjectsFromTable(string query)
        {
            string whereClause = (query);
            BackendlessDataQuery dataQuery = new BackendlessDataQuery(whereClause) { QueryOptions = new QueryOptions() };
            return Backendless.Persistence.Of<TaskModel>().Find(dataQuery).Data;
        }

        public int GetNumberOfObjectsFromTable(string query)
        {
            string whereClause = (query);
            BackendlessDataQuery dataQuery = new BackendlessDataQuery(whereClause) { QueryOptions = new QueryOptions() };
            return Backendless.Data.Of<TaskModel>().Find(dataQuery).TotalObjects;
        }

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


        [DataType(DataType.Date)]
        public DateTime TaskCreateDate { get; set; }


        [Required]
        [Display(Name = "Termin Zakończenia Zadania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TaskDueDate {get;set;}


        public string TaskAuthor { get; set; }

        [Required]
        [Display(Name = "Zadanie Przypisane do Użytkownika")]
        public string TaskAssignedToUser { get; set; }

        [Required]
        [Display(Name = "Status Zadania")]
        public string TaskStatus { get; set; }


        [Required]
        [Display(Name = "Opis Zadania")]
        public string TaskDescription { get; set; }


    }
}