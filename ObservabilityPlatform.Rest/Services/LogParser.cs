using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using ObservabilityPlatform.Rest.Entities;

namespace ObservabilityPlatform.Rest.Services
{
    internal class LogParser : ILogParser
    {
        private const char ValueSeparator = ';';
        
        private readonly ILogger<LogParser> _logger;

        public LogParser(ILogger<LogParser> logger)
        {
            _logger = logger;
        }
        
        public IEnumerable<Metric> Parse(Log log, IEnumerable<string> keyWords)
        {
            var text = log.Text;
            var metrics = new List<Metric>();

            foreach (var key in keyWords)
            {
                if (text.Contains(key) == false)
                    continue;

                var start = text.IndexOf(key, StringComparison.Ordinal) + 1;
                var length = key.Length;
                var result = new StringBuilder();

                for (var i = start + length; text?[i] != ValueSeparator; i++)
                    result.Append(text[i]);

                var value = Convert.ToInt64(result.ToString());

                var metric = new Metric()
                {
                    Name = key,
                    Value = value
                };
                
                metrics.Add(metric);
            }

            return metrics;
        }
    }
}