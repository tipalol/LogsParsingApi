using ObservabilityPlatform.Rest.Entities;

namespace ObservabilityPlatform.Rest.Services
{
    public interface IElasticPushService
    {
        public void Push(Log log);
    }
}