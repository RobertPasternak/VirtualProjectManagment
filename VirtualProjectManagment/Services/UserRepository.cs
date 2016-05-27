using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackendlessAPI;
using BackendlessAPI.Persistence;
using VirtualProjectManagment.Models;

namespace VirtualProjectManagment.Services
{
    public class UserRepository
    {
        public IEnumerable<Users> GetListOfAllUsers()
        {
            BackendlessDataQuery dataQuery = new BackendlessDataQuery("name LIKE '%'") { QueryOptions = new QueryOptions() };
            return Backendless.Persistence.Of<Users>().Find(dataQuery).Data;
        }
    }
}