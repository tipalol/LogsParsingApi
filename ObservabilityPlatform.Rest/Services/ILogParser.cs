using System.Collections.Generic;
using ObservabilityPlatform.Rest.Entities;

namespace ObservabilityPlatform.Rest.Services
{
    public interface ILogParser
    {
        public IEnumerable<Metric> Parse(Log log, IEnumerable<string> keyWords);
    }
}