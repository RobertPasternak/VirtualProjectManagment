using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackendlessAPI;
using BackendlessAPI.Persistence;
using VirtualProjectManagment.Models;

namespace VirtualProjectManagment.Services
{
    public class TaskRepository
    {
        public List<TaskModel> GetListOfObjectsFromTable(string query)
        {
            BackendlessDataQuery dataQuery = new BackendlessDataQuery(query) { QueryOptions = new QueryOptions() };
            return Backendless.Persistence.Of<TaskModel>().Find(dataQuery).Data;
        }

        public int GetNumberOfObjectsFromTable(string query)
        {
            BackendlessDataQuery dataQuery = new BackendlessDataQuery(query) { QueryOptions = new QueryOptions() };
            return Backendless.Data.Of<TaskModel>().Find(dataQuery).TotalObjects;
        }

        public TaskModel GetObjectFromTable(string query)
        {
            BackendlessDataQuery dataQuery = new BackendlessDataQuery(query) { QueryOptions = new QueryOptions() };
            return Backendless.Data.Find<TaskModel>(dataQuery).Data[0];
        }

        public void UpdateTask(string id, DateTime dueDate, string assignedToUser, string priority, string status, string description, string taskName)
        {
            IEnumerable<TaskModel> taskModel = GetListOfObjectsFromTable("objectId LIKE '" + id + "'");
            foreach (var task in taskModel)
            {
                task.TaskDueDate = dueDate;
                task.TaskAssignedToUser = assignedToUser;
                task.TaskPriority = priority;
                task.TaskStatus = status;
                task.TaskDescription = description;
                task.TaskName = taskName;
                Backendless.Persistence.Save(task);
            }            
        }
    }
}