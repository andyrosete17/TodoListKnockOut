namespace TDL.Services
{
    using Newtonsoft.Json;
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

        public string AddNewItem(string description)
        {
            var result = _repository.Create();
            var item = result.Result as ToDoListItems;
            item.Description = description;
            item.Id = Guid.NewGuid();
            item.Status = ToDoListStatusEnum.InProgress;
            result.Result = item;
            var jsonResult = JsonConvert.SerializeObject(result);
            var response = MapResults(result);
            _repository.CommitContextChanges();
            return jsonResult;
        }

        public string ChangeStatus(Guid id, string status)
        {
            var result = _repository.Get(id);
            var item = result.Result as ToDoListItems;
            bool.TryParse(status, out var statusEnumValue);

            item.Status = statusEnumValue ? ToDoListStatusEnum.Completed : ToDoListStatusEnum.InProgress;
            var jsonResult = JsonConvert.SerializeObject(result);
            var response = MapResults(result);
            _repository.CommitContextChanges();
            return jsonResult;
        }

        public string GetAllData()
        {
            var result=  _repository.GetAll();
            var jsonResult = JsonConvert.SerializeObject(result);
            return jsonResult;
        }

        public string RemoveData(Guid id)
        {
            var result = _repository.RemoveData(id);
            var jsonResult = JsonConvert.SerializeObject(result);            
            _repository.CommitContextChanges();
            return jsonResult;
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
