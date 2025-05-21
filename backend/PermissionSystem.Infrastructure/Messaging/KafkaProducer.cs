using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using PermissionSystem.Domain.Events;
using PermissionSystem.Domain.Services;
using System.Text.Json;

namespace PermissionSystem.Infrastructure.Messaging
{
    public class KafkaProducer : IMessagingService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;

        public KafkaProducer(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
            _topic = configuration["Kafka:Topic"] ?? "permissions";
        }

        public async Task SendEventAsync(PermissionEventDto eventDto)
        {
            var message = JsonSerializer.Serialize(eventDto);
            await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        }
    }
}
