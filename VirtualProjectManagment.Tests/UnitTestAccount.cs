using System;
using System.Web.Mvc;
using BackendlessAPI.Exception;
using BackendlessAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualProjectManagment.Controllers;
using VirtualProjectManagment.Models;


namespace VirtualProjectManagment.Tests
{
    [TestClass]
    public class UnitTestAccount
    {
        [TestMethod]
        public void TestLoginView()
        {
            AccountController controller = new AccountController();
            var result = controller.Login() as ViewResult;
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
