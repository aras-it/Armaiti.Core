using Armaiti.Core.Configurations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Armaiti.Core.Messaging
{
    /// <summary>
    /// Smtp email sender.
    /// </summary>
    public class SmtpCarrier
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpConfig _smtpConfig;
        private readonly ILogger<SmtpCarrier> _logger;

        /// <summary>
        /// Represents an instance of the SmtpCarrier.
        /// </summary>
        /// <param name="smtpConfig">The smtp configuration settings from dependency injection.</param>
        /// <param name="logger">A generic interface for logging from dependency injection.</param>
        public SmtpCarrier(SmtpConfig smtpConfig, ILogger<SmtpCarrier> logger)
        {
            _smtpConfig = smtpConfig;
            _logger = logger;

            try
            {
                _smtpClient = new SmtpClient
                {
                    DeliveryFormat = _smtpConfig.DeliveryFormat,
                    DeliveryMethod = _smtpConfig.DeliveryMethod,
                    EnableSsl = _smtpConfig.EnableSsl,
                    Host = _smtpConfig.Host,
                    Port = _smtpConfig.Port,
                    PickupDirectoryLocation = _smtpConfig.PickupDirectoryLocation,
                    UseDefaultCredentials = _smtpConfig.DefaultCredentials
                };

                if (!_smtpClient.UseDefaultCredentials)
                {
                    _smtpClient.Credentials = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled SmtpConfig Exception!");
#if DEBUG
                throw new Exception("Unhandled SmtpConfig Exception!", ex);
#endif
            }
        }

        /// <summary>
        /// The sync method for sending an email message.
        /// </summary>
        /// <param name="emailMessage">Email message structure.</param>
        /// <param name="attachments">A <see cref="Dictionary{string, Stream}"/> that indicates the name and <see cref="Stream"/> of attachments.</param>
        public void SendEmail(EmailMessage emailMessage, IDictionary<string, Stream> attachments = null)
        {
            if (_smtpClient == null)
                return;

            try
            {
                using (var message = CreateMessage(emailMessage, attachments))
                {
                    _smtpClient.Send(message);
                }

                CleanupAttachments(attachments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled SendEmail Exception!");
#if DEBUG
                throw ex;
#endif
            }
        }

        /// <summary>
        /// The async method for sending an email message.
        /// </summary>
        /// <param name="emailMessage">Email message structure.</param>
        /// <param name="attachments">A <see cref="Dictionary{string, Stream}"/> that indicates the name and <see cref="Stream"/> of attachments.</param>
        /// <returns></returns>
        public async Task SendEmailAsync(EmailMessage emailMessage, IDictionary<string, Stream> attachments = null/*, object userToken = null*/)
        {
            if (_smtpClient == null)
                return;

            try
            {
                using (var message = CreateMessage(emailMessage, attachments))
                {
                    await _smtpClient.SendMailAsync(message);
                }

                CleanupAttachments(attachments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled SendEmailAsync Exception!");
#if DEBUG
                throw ex;
#endif
            }
        }

        private MailMessage CreateMessage(EmailMessage emailMessage, IDictionary<string, Stream> attachments = null)
        {
            //construct new mail message
            var message = new MailMessage();

            //set sender on mail message
            if (!string.IsNullOrEmpty(_smtpConfig.From))
            {
                var sender = _smtpConfig.From.Split('|', StringSplitOptions.RemoveEmptyEntries);
                if (sender.Length > 1)
                    message.From = new MailAddress(sender[1], sender[0], Encoding.UTF8);
                else
                    message.From = new MailAddress(sender[0]);
            }
            else
            {
                throw new Exception("Invalid Email Notification Settings!");
            }

            //add to addresses
            string[] recipients = emailMessage.MailTo.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (string toAddress in recipients)
            {
                var recipient = toAddress.Split('|', StringSplitOptions.RemoveEmptyEntries);
                if (recipient.Length > 1)
                    message.To.Add(new MailAddress(recipient[1], recipient[0], Encoding.UTF8));
                else
                    message.To.Add(new MailAddress(recipient[0]));
            }

            //add cc addresses
            if (!string.IsNullOrEmpty(emailMessage.MailCC))
            {
                recipients = emailMessage.MailCC.Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (string ccAddress in recipients)
                {
                    var recipient = ccAddress.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    if (recipient.Length > 1)
                        message.CC.Add(new MailAddress(recipient[1], recipient[0], Encoding.UTF8));
                    else
                        message.CC.Add(new MailAddress(recipient[0]));
                }
            }

            //add bcc addresses
            if (!string.IsNullOrEmpty(emailMessage.MailBCC))
            {
                recipients = emailMessage.MailBCC.Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (string bccAddress in recipients)
                {
                    var recipient = bccAddress.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    if (recipient.Length > 1)
                        message.Bcc.Add(new MailAddress(recipient[1], recipient[0], Encoding.UTF8));
                    else
                        message.Bcc.Add(new MailAddress(recipient[0]));
                }
            }

            //attachments
            if (attachments != null && attachments.Count > 0)
            {
                foreach (string key in attachments.Keys)
                {
                    var attachment = new Attachment(attachments[key], key);
                    message.Attachments.Add(attachment);
                }
            }

            //subject
            message.Subject = emailMessage.Subject;
            message.SubjectEncoding = Encoding.UTF8;

            //body 
            message.Body = emailMessage.Body;
            message.BodyEncoding = Encoding.UTF8;

            //set message properties
            message.DeliveryNotificationOptions = emailMessage.DeliveryNotification;
            message.IsBodyHtml = emailMessage.IsBodyHtml;

            //other parameters
            string replyTo = _smtpConfig.ReplyTo;
            if (!string.IsNullOrEmpty(replyTo))
                message.Headers.Add("Reply-To", replyTo);
            else
                message.Headers.Add("Reply-To", message.From.Address);

            return message;
        }

        private void CleanupAttachments(IDictionary<string, Stream> attachments = null)
        {
            if (attachments != null && attachments.Count > 0)
            {
                foreach (string key in attachments.Keys)
                {
                    attachments[key].Flush();
                    attachments[key].Close();
                }
            }
        }

        public void Dispose()
        {
            if (_smtpClient != null)
                _smtpClient.Dispose();
        }
    }
}
