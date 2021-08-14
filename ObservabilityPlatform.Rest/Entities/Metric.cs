using System;

namespace ObservabilityPlatform.Rest.Entities
{
    [Serializable]
    public class Metric
    {
        public string Name { get; set; }
        public long Value { get; set; }
    }
}