using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObservabilityPlatform.Rest.Entities;
using ObservabilityPlatform.Rest.Services;
using OpenTelemetry.Trace;

namespace ObservabilityPlatform.Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParsingLogsController : ControllerBase
    {
        private readonly ILogger<ParsingLogsController> _logger;
        private readonly ILogParser _parser;

        private readonly IVMPushService _vmPusher;
        private readonly IElasticPushService _elasticPusher;

        public ParsingLogsController(
            ILogger<ParsingLogsController> logger, 
            ILogParser parser, 
            IVMPushService vmPusher, 
            IElasticPushService elasticPusher)
        {
            _logger = logger;
            _parser = parser;
            _vmPusher = vmPusher;
            _elasticPusher = elasticPusher;
        }

        [HttpPost("parse")]
        [Consumes("application/json")]
        public async Task<IEnumerable<Metric>> Parse([FromHeader] IEnumerable<string> keyWords, [FromBody] Log log)
        {
            IEnumerable<Metric> metrics;

            _logger.LogInformation("Parsing started");
                
            using (var span = new Activity("Log parsing"))
            {
                span.Start();
                metrics = _parser.Parse(log, keyWords);
                span.Stop();
            }

            _logger.LogInformation("Pushing metrics to VM started");

            var enumerable = metrics as Metric[] ?? metrics.ToArray();
            
            using (var span = new Activity("Push metrics to VM"))
            {
                span.Start();
                await _vmPusher.Push(enumerable, log);
                span.Stop();
            }
            
            _logger.LogInformation("Pushing log to Elastic started");
            
            using (var span = new Activity("Push logs to Elastic"))
            {
                span.Start();
                _elasticPusher.Push(log);
                span.Stop();
            }

            return enumerable;
        }
    }
}