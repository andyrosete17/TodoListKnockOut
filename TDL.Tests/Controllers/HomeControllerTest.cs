using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TDL.Controllers;

namespace TDL.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.TodoList() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
