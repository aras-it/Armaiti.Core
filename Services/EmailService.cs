using Armaiti.Core.Configurations;
using Armaiti.Core.Messaging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace Armaiti.Core.Services
{
    /// <summary>
    /// This class is used by the application to send smtp email.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly SmtpCarrier _carrier;
        private readonly SmtpConfig _smtpConfig;
        private readonly IWebHostEnvironment _hostEnvironment;

        /// <summary>
        /// Represents an instance of email service provider.
        /// </summary>
        /// <param name="smtpConfig"></param>
        /// <param name="logger"></param>
        public EmailService(IOptions<SmtpConfig> smtpConfig, ILogger<SmtpCarrier> logger, IWebHostEnvironment hostEnvironment)
        {
            _smtpConfig = smtpConfig.Value;
            _carrier = new SmtpCarrier(_smtpConfig, logger);
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// This method send an email to specific recipient.
        /// </summary>
        /// <param name="recipient">The recipient email address.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="body">Body of the email message in html/plain text format.</param>
        /// <param name="attachments">A <see cref="Dictionary{string, Stream}"/> that indicates the name and <see cref="Stream"/> of attachments.</param>
        public void Send(string recipient, string subject, string body, IDictionary<string, Stream> attachments = null)
        {
            Send(new EmailMessage
            {
                MailTo = recipient,
                Subject = subject,
                Body = body
            }, attachments);
        }

        /// <summary>
        /// This method sends an email that defined in the <see cref="EmailMessage"/>.
        /// </summary>
        /// <param name="emailMessage">Email message structure.</param>
        /// <param name="attachments">A <see cref="Dictionary{string, Stream}"/> that indicates the name and <see cref="Stream"/> of attachments.</param>
        public void Send(EmailMessage emailMessage, IDictionary<string, Stream> attachments = null)
        {
            if (emailMessage.MessageDocument != null)
                BodyFromTemlate(emailMessage);
            else if (emailMessage.IsBodyHtml)
                emailMessage.Body = $"<div dir=\"{(Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft ? "rtl" : "ltr")}\">{emailMessage.Body}</div>";

            _carrier.SendEmail(emailMessage, attachments);
        }

        /// <summary>
        /// This method send an email to specific recipient.
        /// </summary>
        /// <param name="recipient">The recipient email address.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="body">Body of the email message in html/plain text format.</param>
        /// <param name="attachments">A <see cref="Dictionary{string, Stream}"/> that indicates the name and <see cref="Stream"/> of attachments.</param>
        public async Task SendAsync(string recipient, string subject, string body, IDictionary<string, Stream> attachments = null)
        {
            await SendAsync(new EmailMessage
            {
                MailTo = recipient,
                Subject = subject,
                Body = body
            }, attachments);
        }

        /// <summary>
        /// This method sends an email that defined in the <see cref="EmailMessage"/>.
        /// </summary>
        /// <param name="emailMessage">Email message structure.</param>
        /// <param name="attachments">A <see cref="Dictionary{string, Stream}"/> that indicates the name and <see cref="Stream"/> of attachments.</param>
        public async Task SendAsync(EmailMessage emailMessage, IDictionary<string, Stream> attachments = null)
        {
            if (emailMessage.MessageDocument != null)
                BodyFromTemlate(emailMessage);
            else if (emailMessage.IsBodyHtml)
                emailMessage.Body = $"<div dir=\"{(Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft ? "rtl" : "ltr")}\">{emailMessage.Body}</div>";

            await _carrier.SendEmailAsync(emailMessage, attachments);
        }

        public void Dispose()
        {
            _carrier.Dispose();
        }

        private void BodyFromTemlate(EmailMessage emailMessage)
        {
            if (string.IsNullOrWhiteSpace(emailMessage.MessageDocument.Direction))
            {
                emailMessage.MessageDocument.Direction = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft ? "rtl" : "ltr";
            }

            if (!string.IsNullOrWhiteSpace(_smtpConfig.TemplatePath))
            {
                //get template path
                if (!_smtpConfig.TemplatePath.StartsWith('/'))
                    _smtpConfig.TemplatePath = '\\' + _smtpConfig.TemplatePath;
                var templatePath = $"{_hostEnvironment.ContentRootPath}{_smtpConfig.TemplatePath.Replace('/', '\\')}";

                //load xslt template
                var xsltTransform = new XslCompiledTransform();
                xsltTransform.Load(templatePath);

                using (StringWriter writer = new StringWriter())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(MessageDocument));
                    serializer.Serialize(writer, emailMessage.MessageDocument);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(writer.ToString());
                    writer.Flush();
                    writer.Close();

                    using StringWriter swOutput = new StringWriter();
                    xsltTransform.Transform(xmlDoc, null, swOutput);

                    emailMessage.Body = swOutput.ToString();
                    swOutput.Flush();
                    swOutput.Close();
                }

                emailMessage.Body = HttpUtility.HtmlDecode(emailMessage.Body/*.Replace("{0}", direction)*/);
            }
            else
            {
                emailMessage.Body = $"<div dir=\"{(CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft ? "rtl" : "ltr")}\">" + 
                    $"{emailMessage.MessageDocument.Body}</div>";
            }
        }
    }
}
