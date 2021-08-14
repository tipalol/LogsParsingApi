using System.Collections.Generic;
using System.Threading.Tasks;
using ObservabilityPlatform.Rest.Entities;

namespace ObservabilityPlatform.Rest.Services
{
    public interface IVMPushService
    {
        public Task Push(IEnumerable<Metric> metrics, Log source);
    }
}