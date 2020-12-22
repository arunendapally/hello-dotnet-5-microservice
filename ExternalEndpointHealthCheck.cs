using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace HelloDotnet5
{
    public class ExternalEndpointHealthCheck : IHealthCheck
    {
        private readonly ServiceSettings serviceSettings;

        public ExternalEndpointHealthCheck(IOptions<ServiceSettings> options)
        {
            serviceSettings = options.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();
            var reply = await ping.SendPingAsync(serviceSettings.OpenWeatherHost);
            if (reply.Status != IPStatus.Success)
            {
                return HealthCheckResult.Unhealthy();
            }
            return HealthCheckResult.Healthy();
        }
    }
}