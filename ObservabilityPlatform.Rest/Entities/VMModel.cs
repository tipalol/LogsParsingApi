using System.Collections.Generic;
using Newtonsoft.Json;

namespace ObservabilityPlatform.Rest.Entities
{
    public class VMModel
    {
        [JsonProperty("metric")]
        public Dictionary<string, string> Metric { get; set; }
        
        [JsonProperty("values")]
        public long[] Values { get; set; }
        
        [JsonProperty("timestamps")]
        public long[] Timestamps { get; set; }
    }
}