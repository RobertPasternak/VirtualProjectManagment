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
        public List<TaskModel> GetObjectsFromTable(string query)
        {
            BackendlessDataQuery dataQuery = new BackendlessDataQuery(query) { QueryOptions = new QueryOptions() };
            return Backendless.Persistence.Of<TaskModel>().Find(dataQuery).Data;
        }

        public int GetNumberOfObjectsFromTable(string query)
        {
            BackendlessDataQuery dataQuery = new BackendlessDataQuery(query) { QueryOptions = new QueryOptions() };
            return Backendless.Data.Of<TaskModel>().Find(dataQuery).TotalObjects;
        }
    }
}