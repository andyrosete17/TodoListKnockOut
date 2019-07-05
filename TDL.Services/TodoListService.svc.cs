namespace TDL.Services
{
    using System;
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
            _repository.CommitContextChanges();
            return result;
        }

        public Response ChangeStatus(Guid id, string status)
        {
            var item = _repository.Get(id);
            var result = item.Result as ToDoListItems;
            result.Status = (ToDoListStatusEnum)Enum.Parse(typeof(ToDoListStatusEnum), status);
            _repository.CommitContextChanges();
            item.Result = result;
            return item;
        }

        public Response GetAllData()
        {
            return _repository.GetAll();
        }

        public Response RemoveData(Guid id)
        {
            var result = _repository.RemoveData(id);
            _repository.CommitContextChanges();

            return result;
        }
    }
}
