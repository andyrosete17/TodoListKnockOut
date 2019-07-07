namespace TDL.Tests.Services
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using System;
    using TDL.Common.Enums;
    using TDL.Domain;
    using TDL.Services;
    using TDL.Services.Interfaces.Repository;
    using TDL.Services.Models;
    using TDL.TestUtilities.BaseImplementation;

    [TestClass]
    public sealed partial class TodoListServiceTest : BaseTestHelper<TodoListService>
    {
        private Guid id = Guid.NewGuid();
        private Mock<ITodoListRepository<ToDoListItems>> mockRepository;
        private Mock<LocalDataContext> mockDataContext;
        private ToDoListItems todoListItem;
        private readonly string description = "Aloha";
        private Response response = new Response();

        [TestInitialize]
        public void Initialize()
        {
            this.mockRepository = new Mock<ITodoListRepository<ToDoListItems>>();
            this.mockDataContext = new Mock<LocalDataContext>();
            this.response = new Response
            {
                IsSuccess = true, 
                Message = "OK",
                Result = new ToDoListItems
                {
                    Description = "Aloha",
                    Id = id,
                    Status = ToDoListStatusEnum.InProgress
                }
            };
        }

       

        public override TodoListService CreateSut()
        {
            return new TodoListService(mockRepository.Object, mockDataContext.Object);
        }
    }
}
