using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using TDL.Common.Enums;
using TDL.Domain;
using TDL.Services.Models;

namespace TDL.Tests.Services
{
    public sealed partial class TodoListServiceTest
    {
        [TestMethod]
        public void ChangeStatus_Request_Success()
        {
            // Arrange
            var sut = this.CreateSut();
            this.mockRepository
                .Setup(x => x.Get(id))
                .Returns(this.response)
                .Verifiable();

            this.mockRepository
                .Setup(x => x.CommitContextChanges())
                .Returns(this.response)
                .Verifiable();

            // Act
            var responseJson = sut.ChangeStatus(id, "True");
            var response = JsonConvert.DeserializeObject<Response>(responseJson);
            var result = JsonConvert.DeserializeObject<ToDoListItems>(response.Result.ToString());

            // Assert
            response.IsSuccess.Should().Be(true);
            result.Description.Should().Be("Aloha");
            result.Status.Should().Be(ToDoListStatusEnum.Completed);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void ChangeStatus_Request_Fail_Wrong_Id()
        {
            // Arrange
            var sut = this.CreateSut();
            this.mockRepository
                .Setup(x => x.Get(id))
                .Returns((Response)null)
                .Verifiable();

            // Act
            var responseJson = sut.ChangeStatus(id, "True");
            var response = JsonConvert.DeserializeObject<Response>(responseJson);

            // Assert
            response.IsSuccess.Should().Be(false);
            response.Message.Should().Be("Todo item could not be found");
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void ChangeStatus_Request_Fail_EmptyStatus()
        {
            // Arrange
            var sut = this.CreateSut();          
          
            // Act
            var responseJson = sut.ChangeStatus(id, "");
            var response = JsonConvert.DeserializeObject<Response>(responseJson);

            // Assert
            response.IsSuccess.Should().Be(false);
            response.Message.Should().Be("Status can not be empty");
        }
    }
}
