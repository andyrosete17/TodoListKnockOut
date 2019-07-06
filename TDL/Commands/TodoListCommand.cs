namespace TDL.Front.Commands
{
    using Newtonsoft.Json;
    using System;
    using TDL.Front.TodoListService;
    using TDL.Services.Models;

    public class TodoListCommand
    {
        public Response AddNewToDoItem(string description)
        {
            var service = new TodoListServiceClient();
            var result = new Response();
            if (!string.IsNullOrEmpty(description))
            {
                try
                {
                    if (service != null)
                    {
                        var jsonResult = service.AddNewItem(description);
                        result = JsonConvert.DeserializeObject<Response>(jsonResult);
                    }
                }
                catch (Exception)
                {
                    return result;
                }
              
            }
            return result;
        }

        public Response RemoveToDoItem(Guid id)
        {
            var service = new TodoListServiceClient();
            var result = new Response();

            try
            {
                if (service != null)
                {
                    var jsonResult=  service.RemoveData(id);
                    result = JsonConvert.DeserializeObject<Response>(jsonResult);
                }
            }
            catch (Exception)
            {
                return result;
            }
            finally
            {
                service.Close();

            }
            return result;
        }

        public Response ChangeStatusToDoItem(Guid id, string status)
        {
            var service = new TodoListServiceClient();
            var result = new Response();

            try
            {
                if (service != null)
                {
                    var jsonResult = service.ChangeStatus(id, status);
                    result = JsonConvert.DeserializeObject<Response>(jsonResult);
                }
            }
            catch (Exception)
            {
                return result;
            }
            finally
            {
                service.Close();

            }
            return result;
        }

        public Response GetAllToDoItem()
        {
            var service = new TodoListServiceClient();
            var result = new Response();

            try
            {
                if (service != null)
                {
                    var jsonResult = service.GetAllData();
                    result = JsonConvert.DeserializeObject<Response>(jsonResult);
                }
            }
            catch (Exception)
            {
                return result;
            }
            finally
            {
                service.Close();

            }
            return result;
        }
    }
}