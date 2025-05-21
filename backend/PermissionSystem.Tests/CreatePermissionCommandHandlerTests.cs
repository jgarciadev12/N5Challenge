using Moq;
using PermissionSystem.Application.Permissions.Commands;
using PermissionSystem.Domain.Entities;
using PermissionSystem.Domain.Events;
using PermissionSystem.Domain.Repositories;
using PermissionSystem.Domain.Services;

namespace PermissionSystem.Test
{
    public class CreatePermissionCommandHandlerTests
    {
        [Fact]
        public async Task Should_Publish_Kafka_And_Index_Elasticsearch_When_Creating_Permission()
        {
            var repoMock = new Mock<IPermissionRepository>();
            var elasticMock = new Mock<IElasticsearchService>();
            var kafkaMock = new Mock<IMessagingService>();

            var handler = new CreatePermissionCommandHandler(
                repoMock.Object,
                elasticMock.Object,
                kafkaMock.Object
            );

            var command = new CreatePermissionCommand
            {
                EmployeeName = "Jose",
                EmployeeLastName = "Garcia",
                PermissionTypeId = 1,
                PermissionDate = System.DateTime.UtcNow
            };

            var result = await handler.Handle(command, CancellationToken.None);

            kafkaMock.Verify(m => m.SendEventAsync(It.IsAny<PermissionEventDto>()), Times.Once);
            elasticMock.Verify(e => e.IndexPermissionAsync(It.IsAny<Permission>()), Times.Once);
            repoMock.Verify(r => r.AddAsync(It.IsAny<Permission>()), Times.Once);
        }
    }
}