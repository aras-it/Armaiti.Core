using System.Net;

namespace Armaiti.Core.Http
{
    /// <summary>
    /// Provides a container class for both <see cref="IPAddress"/> and <see cref="IPHostEntry"/>.
    /// </summary>
    public class IP
    {
        /// <summary>
        /// Gets or seta an Internet Protocol (IP) address.
        /// </summary>
        public IPAddress Address { get; set; }

        /// <summary>
        /// Gets or sets a container class for Internet host address information.
        /// </summary>
        public IPHostEntry HostEntry { get; set; }
    }
}
