using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualProjectManagment.Models
{
    public class Comments
    {
        public string objectId { get; set; }

        public string TaskId { get; set; }

        public string AuthorName { get; set; }

        public string AuthorSurname { get; set; }

        public string CommentBody { get; set; }

        public bool CanBeEdited { get; set; }

        public DateTime CommentDateTime { get; set; }

    }
}