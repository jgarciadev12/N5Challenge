using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using PermissionSystem.Domain.Services;
using PermissionSystem.Infrastructure.Elasticsearch;

namespace PermissionSystem.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Kafka
            services.AddSingleton<IMessagingService, KafkaProducer>();

            // Elasticsearch
            var settings = new ConnectionSettings(new Uri(configuration["Elasticsearch:Uri"]))
                .DefaultIndex("permissions")
                .EnableDebugMode();

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
            services.AddSingleton<IElasticsearchService, ElasticsearchService>();

            return services;
        }
    }
}
