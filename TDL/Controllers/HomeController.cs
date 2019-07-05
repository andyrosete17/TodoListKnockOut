using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDL.Front.Commands;

namespace TDL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult TodoList()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public JsonResult CreateNewTodoListItem(string description)
        {
            var result = new object();
            var command = new TodoListCommand();
            var response = command.AddNewToDoItem(description);
            if (response.IsSuccess)
            {
                result = response.Result;
            }
            else
            {
                ViewBag.Message = $"Your application crash, Error: {response.Message} ";
            }

            return Json(result);
        }
    }
}