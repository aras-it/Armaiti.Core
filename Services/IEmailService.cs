using Armaiti.Core.Messaging;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Armaiti.Core.Services
{
    /// <summary>
    /// The email service API.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// This method send an email to specific recipient.
        /// </summary>
        /// <param name="recipient">The recipient email address.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="body">Body of the email message in html/plain text format.</param>
        /// <param name="attachments">A <see cref="Dictionary{string, Stream}"/> that indicates the name and <see cref="Stream"/> of attachments.</param>
        void Send(string recipient, string subject, string body, IDictionary<string, Stream> attachments = null);

        /// <summary>
        /// This method sends an email that defined in the <see cref="EmailMessage"/>.
        /// </summary>
        /// <param name="emailMessage">Email message structure.</param>
        /// <param name="attachments">A <see cref="Dictionary{string, Stream}"/> that indicates the name and <see cref="Stream"/> of attachments.</param>
        void Send(EmailMessage emailMessage, IDictionary<string, Stream> attachments = null);

        /// <summary>
        /// This method send an email to specific recipient.
        /// </summary>
        /// <param name="recipient">The recipient email address.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="body">Body of the email message in html/plain text format.</param>
        /// <param name="attachments">A <see cref="Dictionary{string, Stream}"/> that indicates the name and <see cref="Stream"/> of attachments.</param>
        Task SendAsync(string recipient, string subject, string body, IDictionary<string, Stream> attachments = null);

        /// <summary>
        /// This method sends an email that defined in the <see cref="EmailMessage"/>.
        /// </summary>
        /// <param name="emailMessage">Email message structure.</param>
        /// <param name="attachments">A <see cref="Dictionary{string, Stream}"/> that indicates the name and <see cref="Stream"/> of attachments.</param>
        Task SendAsync(EmailMessage emailMessage, IDictionary<string, Stream> attachments = null);
    }
}