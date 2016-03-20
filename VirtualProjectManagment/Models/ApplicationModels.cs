using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Weborb.Service;

namespace VirtualProjectManagment.Models
{
    public class OverviewModel
    {
        public int TaskId { get; set; }

        public string TaskStatus { get; set; }

        public string TaskPriority { get; set; }

        public string TaskName { get; set; }

        public string TaskAuthor { get; set; }
    }

    public class AddTaskModel
    {
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