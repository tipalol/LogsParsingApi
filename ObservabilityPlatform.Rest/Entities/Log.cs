using System;
using Newtonsoft.Json;

namespace ObservabilityPlatform.Rest.Entities
{
    [Serializable]
    public class Log
    {
        [JsonProperty("index")]
        public string Index { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}