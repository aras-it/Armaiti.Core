using Armaiti.Core.Http;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;

namespace Armaiti.Core
{
    /// <summary>
    /// Extension methods to get client IP address.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Get remote ip, optionally allowing for x-forwarded-for header check.
        /// </summary>
        /// <param name="context">Http context</param>
        /// <param name="allowForwarded">Whether to allow x-forwarded-for header check.</param>
        /// <returns>The <see cref="IP"/>.</returns>
        public static IP GetRemoteIP(this HttpContext context, bool allowForwarded = true)
        {
            if (allowForwarded)
            {
                string header = (context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() 
                              ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault());
                if (IPAddress.TryParse(header, out IPAddress address))
                {
                    return new IP
                    {
                        Address = address,
                        HostEntry = Dns.GetHostEntry(Dns.GetHostName())
                    };
                }
            }

            return new IP
            {
                Address = context.Connection.RemoteIpAddress,
                HostEntry = Dns.GetHostEntry(Dns.GetHostName())
            };
        }
    }
}
