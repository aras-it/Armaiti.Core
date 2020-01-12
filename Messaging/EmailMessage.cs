using System;
using System.Net.Mail;

namespace Armaiti.Core.Messaging
{
    /// <summary>
    /// Describes the structure of the email message.
    /// </summary>
    [Serializable]
    public class EmailMessage
    {
        /// <summary>
        /// List of recipients in comma separated string.
        /// </summary>
        public string MailTo { get; set; }

        /// <summary>
        /// List of CC recipients in comma separated string.
        /// </summary>
        public string MailCC { get; set; }

        /// <summary>
        /// List of BCC recipients in comma separated string.
        /// </summary>
        public string MailBCC { get; set; }

        /// <summary>
        /// The email subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The message body when the MessageDocument is null.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Represents the format of the message body (html or plain text). Default value is true.
        /// </summary>
        public bool IsBodyHtml { get; set; } = true;

        /// <summary>
        /// HTML message template values. If this property is set, the body property will be ignored.
        /// </summary>
        public MessageDocument MessageDocument { get; set; }

        /// <summary>
        /// Description of email delivery notification options. Default value is OnFailure.
        /// </summary>
        public DeliveryNotificationOptions DeliveryNotification { get; set; } = DeliveryNotificationOptions.OnFailure;
    }
}
