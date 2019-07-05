namespace TDL.Front.Commands
{
    using System;
    using TDL.Front.TodoListService;

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
                        result = service.AddNewItem(description);
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
                    result = service.RemoveData(id);
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
                    result = service.ChangeStatus(id, status);
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
                    result = service.GetAllData();
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