using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ObservabilityPlatform.Rest.Entities;

namespace ObservabilityPlatform.Rest.Services
{
    internal class VMPushService : IVMPushService, IDisposable
    {
        private readonly HttpClient _client;
        private readonly string _vmUri;

        public VMPushService(IConfiguration configuration)
        {
            _client = new HttpClient();
            _vmUri = configuration["VMUri"];
        }
        
        public async Task Push(IEnumerable<Metric> metrics, Log source)
        {
            foreach (var metric in metrics)
            {
                var model = new VMModel()
                {
                    Metric = new Dictionary<string, string>()
                    {
                        {"__name__", metric.Name},
                        {"index", source.Index}
                    },
                    Values = new []{metric.Value},
                    Timestamps = new []{DateTimeOffset.Now.ToUnixTimeMilliseconds()}
                };

                var content = JsonConvert.SerializeObject(model);

                var response = await _client.PostAsync(_vmUri, new StringContent(content));
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}