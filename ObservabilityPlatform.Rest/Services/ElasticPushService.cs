using System;
using Microsoft.Extensions.Configuration;
using Nest;
using ObservabilityPlatform.Rest.Entities;

namespace ObservabilityPlatform.Rest.Services
{
    internal class ElasticPushService : IElasticPushService
    {
        private readonly ElasticClient _client;

        public ElasticPushService(IConfiguration configuration)
        {
            var urlString = configuration["elasticUri"];
            var uri = new Uri(urlString);
            var connection = new ConnectionSettings(uri);

            _client = new ElasticClient(connection);
        }
        
        public void Push(Log log)
        {
            _client.Index(log, index => index.Index(log.Index));
        }
    }
}