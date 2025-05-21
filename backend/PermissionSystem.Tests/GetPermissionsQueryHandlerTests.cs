using Moq;
using PermissionSystem.Application.Permissions.Queries;
using PermissionSystem.Domain.Entities;
using PermissionSystem.Domain.Events;
using PermissionSystem.Domain.Repositories;
using PermissionSystem.Domain.Services;

namespace PermissionSystem.Test
{
    public class GetPermissionsQueryHandlerTests
    {
        [Fact]
        public async Task Should_Return_PermissionDto_List_And_Publish_Event()
        {
            // Arrange
            var repositoryMock = new Mock<IPermissionRepository>();
            var messagingMock = new Mock<IMessagingService>();
            var elasticMock = new Mock<IElasticsearchService>();

            repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Permission>
        {
            new Permission
            {
                Id = 1,
                EmployeeName = "Ana",
                EmployeeLastName = "Lopez",
                PermissionDate = System.DateTime.UtcNow,
                PermissionTypeId = 2,
                PermissionType = new PermissionType { Id = 2, Description = "Vacation" }
            }
        });

            var handler = new GetPermissionsQueryHandler(repositoryMock.Object, messagingMock.Object, elasticMock.Object);

            // Act
            var result = await handler.Handle(new GetPermissionsQuery(), CancellationToken.None);

            // Assert
            Assert.Single(result);
            Assert.Equal("Ana", result.First().EmployeeName);

            messagingMock.Verify(m => m.SendEventAsync(It.Is<PermissionEventDto>(
                dto => dto.NameOperation == "get")), Times.Once);

            elasticMock.Verify(e => e.IndexPermissionEventAsync(It.IsAny<PermissionEventDto>()), Times.Once);
        }
    }
}
