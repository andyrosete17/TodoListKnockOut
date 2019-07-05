namespace TDL.Services
{
    using System;
    using TDL.Common;
    using TDL.Common.Enums;
    using TDL.Domain;
    using TDL.Services.Interfaces.Repository;
    using TDL.Services.IOCRegistry;
    using TDL.Services.Models;
    using Unity;
    using Unity.Resolution;
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TodoListService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TodoListService.svc or TodoListService.svc.cs at the Solution Explorer and start debugging.
    public class TodoListService : ITodoListService
    {
        private readonly ITodoListRepository<ToDoListItems> _repository;
        private readonly LocalDataContext localDataContext;

        public TodoListService()
        {
            localDataContext = new LocalDataContext();
            ///IOC register initialization
            TodoListServiceRegistry.RegisterComponents();
            _repository = TodoListServiceRegistry.container.Resolve<ITodoListRepository<ToDoListItems>>(new ResolverOverride[]
                                                            {
                                                                new ParameterOverride("dataContext", localDataContext)
                                                            });

        }

        public TodoListService(ITodoListRepository<ToDoListItems> repository, LocalDataContext _localDataContext)
        {
            localDataContext = _localDataContext;
            ///IOC register initialization
            TodoListServiceRegistry.RegisterComponents();
            _repository = repository;
        }

        public Response AddNewItem(string description)
        {
            var result = _repository.Create();
            var item = result.Result as ToDoListItems;
            item.Description = description;
            item.Id = Guid.NewGuid();
            item.Status = ToDoListStatusEnum.InProgress;
            result.Result = item;
            var response = MapResults(result);
            _repository.CommitContextChanges();
            return response;
        }

       

        public Response ChangeStatus(Guid id, string status)
        {
            var result = _repository.Get(id);
            var item = result.Result as ToDoListItems;
            item.Status = (ToDoListStatusEnum)Enum.Parse(typeof(ToDoListStatusEnum), status);
            var response = MapResults(result);
            _repository.CommitContextChanges();
            return response;
        }

        public Response GetAllData()
        {
            return _repository.GetAll();
        }

        public Response RemoveData(Guid id)
        {
            var result = _repository.RemoveData(id);
            var response = MapResults(result);
            _repository.CommitContextChanges();
            return response;
        }

        private Response MapResults(Response result)
        {
            return new Response
            {
                IsSuccess = result.IsSuccess,
                Result = result.Result,
                Message = result.Message
            };
        }
    }
}
