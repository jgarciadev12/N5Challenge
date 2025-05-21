using Moq;
using PermissionSystem.Application.Permissions.Commands;
using PermissionSystem.Domain.Entities;
using PermissionSystem.Domain.Events;
using PermissionSystem.Domain.Repositories;
using PermissionSystem.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionSystem.Test
{
    public class ModifyPermissionCommandHandlerTests
    {
        [Fact]
        public async Task Should_Update_Permission_And_Publish_Event()
        {
            // Arrange
            var repositoryMock = new Mock<IPermissionRepository>();
            var elasticMock = new Mock<IElasticsearchService>();
            var kafkaMock = new Mock<IMessagingService>();

            var existingPermission = new Permission
            {
                Id = 1,
                EmployeeName = "Mario",
                EmployeeLastName = "Gomez",
                PermissionDate = System.DateTime.UtcNow.AddDays(-1),
                PermissionTypeId = 1
            };

            repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingPermission);

            var handler = new ModifyPermissionCommandHandler(repositoryMock.Object, elasticMock.Object, kafkaMock.Object);

            var command = new ModifyPermissionCommand
            {
                Id = 1,
                EmployeeName = "Mario",
                EmployeeLastName = "Ramirez",
                PermissionTypeId = 2,
                PermissionDate = System.DateTime.UtcNow
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            kafkaMock.Verify(k => k.SendEventAsync(It.Is<PermissionEventDto>(
                dto => dto.NameOperation == "modify")), Times.Once);
            elasticMock.Verify(e => e.IndexPermissionAsync(It.IsAny<Permission>()), Times.Once);
        }
    }
}