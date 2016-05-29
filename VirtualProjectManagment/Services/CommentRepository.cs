using System;
using System.Collections.Generic;
using System.Linq;
using BackendlessAPI;
using BackendlessAPI.Persistence;
using VirtualProjectManagment.Models;

namespace VirtualProjectManagment.Services
{
    public class CommentRepository
    {

        public List<Comments> GetObjectsFromTable(string query)
        {
            BackendlessDataQuery dataQuery = new BackendlessDataQuery(query) { QueryOptions = new QueryOptions() };
            return Backendless.Persistence.Of<Comments>().Find(dataQuery).Data;
        }

        public void AddComment(string taskId, string name, string surname, string commentBody, bool canBeEdited)
        {


            Comments comment = new Comments()
            {
                TaskId = taskId,
                AuthorName = name,
                AuthorSurname = surname,
                CommentBody = commentBody,
                CanBeEdited = canBeEdited,
                CommentDateTime = DateTime.Now
            };
            Backendless.Data.Save(comment);
        }

        public void RemoveComment(string id)
        {
            Comments comment = GetObjectsFromTable("objectId LIKE '" + id + "'").First();
            Backendless.Persistence.Of<Comments>().Remove(comment);
        }

    }
}