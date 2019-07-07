using Newtonsoft.Json;
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

        [HttpPost]
        public string CreateNewTodoListItem(string description)
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
                result = response.Message;
            }

            return result.ToString();
        }

        [HttpPost]
        public string RemoveTodoListItem(Guid id)
        {
            var result = new object();
            var command = new TodoListCommand();
            var response = command.RemoveToDoItem(id);
            if (response.IsSuccess)
            {
                result = JsonConvert.SerializeObject(response.IsSuccess); 
            }
            else
            {
                result = response.Message;
            }

            return result.ToString();
        }

        [HttpPost]
        public string GetAllToDoItem()
        {
            var result = new object();
            var command = new TodoListCommand();
            var response = command.GetAllToDoItem();
            if (response.IsSuccess)
            {
                result = response.Result;
            }
            else
            {
                result = response.Message;
            }

            return result.ToString();
        }

        [HttpPost]
        public string ChangeStatusToDoItem(Guid id, string status)
        {
            var result = new object();
            var command = new TodoListCommand();
            var response = command.ChangeStatusToDoItem(id, status);
            if (response.IsSuccess)
            {
                result = response.Result;
            }
            else
            {
                result = response.Message;
            }

            return result.ToString();
        }
    }
}

