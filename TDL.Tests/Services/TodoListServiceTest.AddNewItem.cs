using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TDL.Common.Enums;
using TDL.Domain;
using TDL.Services.Models;

namespace TDL.Tests.Services
{
    public sealed partial class TodoListServiceTest
    {
        [TestMethod]
        public void AddNewItem_Request_Success()
        {
            // Arrange
            var sut = this.CreateSut();

            this.mockRepository
                .Setup(x => x.Create())
                .Returns(this.response)
                .Verifiable();

            this.mockRepository
                .Setup(x => x.CommitContextChanges())
                .Returns(this.response)
                .Verifiable();

            // Act
            var responseJson = sut.AddNewItem(description);
            var response = JsonConvert.DeserializeObject<Response>(responseJson);
            var result = JsonConvert.DeserializeObject<ToDoListItems>(response.Result.ToString());

            // Assert
            response.IsSuccess.Should().Be(true);
            result.Description.Should().Be("Aloha");
            result.Status.Should().Be(ToDoListStatusEnum.InProgress);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void AddNewItem_Request_RequestEmpty()
        {
            // Arrange
            var sut = this.CreateSut();
            this.mockRepository
                .Setup(x => x.Create())
                .Returns(new Response())
                .Verifiable();

            this.mockRepository
                .Setup(x => x.CommitContextChanges())
                .Returns(this.response)
                .Verifiable();

            // Act
            var responseJson = sut.AddNewItem("");
            var response = JsonConvert.DeserializeObject<Response>(responseJson);

            // Assert
            response.IsSuccess.Should().Be(false);
            response.Message.Should().Be("Description can not be empty");

        }

        
    }
}
