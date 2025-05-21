using Nest;
using PermissionSystem.Domain.Entities;
using PermissionSystem.Domain.Events;
using PermissionSystem.Domain.Services;

namespace PermissionSystem.Infrastructure.Elasticsearch
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IElasticClient _client;

        public ElasticsearchService(IElasticClient client)
        {
            _client = client;
        }

        public async Task IndexPermissionAsync(Permission permission)
        {
            var response = await _client.IndexAsync(permission, idx => idx.Index("permissions"));

            if (!response.IsValid)
            {
                throw new Exception("Elasticsearch indexing failed: " + response.OriginalException?.Message);
            }
        }

        public async Task IndexPermissionEventAsync(PermissionEventDto dto)
        {
            var response = await _client.IndexAsync(dto, idx => idx.Index("permission"));

            if (!response.IsValid)
            {
                throw new Exception("Elasticsearch indexing failed: " + response.OriginalException?.Message);
            }
        }
    }
}
