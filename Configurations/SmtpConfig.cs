using System.Net.Mail;
using System.Text.Json.Serialization;

namespace Armaiti.Core.Configurations
{
    public sealed class SmtpConfig
    {
        /// <summary>
        /// Gets or sets the default value that indicates who the email message is from.
        /// </summary>
        /// <remarks>
        /// A string that represents the default value indicating who a mail message is from.
        /// </remarks>
        [JsonPropertyName("form")]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the default value that indicates who the email answer is reply-to.
        /// </summary>
        /// <remarks>
        /// A string that represents the default value indicating who a mail answer is reply-to.
        /// </remarks>
        [JsonPropertyName("replyTo")]
        public string ReplyTo { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the SMTP server.
        /// </summary>
        /// <remarks>
        /// Returns: A string that represents the name of the SMTP server to connect to.
        /// </remarks>
        [JsonPropertyName("host")]
        public string Host { get; set; }
        
        /// <summary>
        /// Gets or sets the port that SMTP clients use to connect to an SMTP mail server. The default value is 25.
        /// </summary>
        /// <remarks>
        /// A string that represents the port to connect to an SMTP mail server.
        /// </remarks>
        [JsonPropertyName("port")]
        public int Port { get; set; }
        
        /// <summary>
        /// Gets or sets whether SSL is used to access an SMTP mail server. The default value is false.
        /// </summary>
        /// <remarks>
        /// true indicates that SSL will be used to access the SMTP mail server; otherwise, false.
        /// </remarks>
        [JsonPropertyName("enableSsl")]
        public bool EnableSsl { get; set; }
        
        /// <summary>
        /// Gets or sets the Simple Mail Transport Protocol (SMTP) delivery method. The default
        /// delivery method is System.Net.Mail.SmtpDeliveryMethod.Network.
        /// </summary>
        /// <remarks>
        /// A string that represents the SMTP delivery method.
        /// </remarks>
        [JsonPropertyName("deliveryMethod")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
        
        /// <summary>
        /// Gets or sets the delivery format to use for sending outgoing e-mail using the Simple Mail Transport Protocol (SMTP).
        /// </summary>
        /// <remarks>
        /// Returns System.Net.Mail.SmtpDeliveryFormat.The delivery format to use for sending outgoing e-mail using SMTP.
        /// </remarks>
        [JsonPropertyName("deliveryFormat")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SmtpDeliveryFormat DeliveryFormat { get; set; }
        
        /// <summary>
        /// Gets or sets the folder where applications save mail messages to be processed by the SMTP server.
        /// </summary>
        /// <remarks>
        /// A string that specifies the pickup directory for e-mail messages.
        /// </remarks>
        [JsonPropertyName("pickupDirectoryLocation")]
        public string PickupDirectoryLocation { get; set; }
        
        /// <summary>
        /// Gets or sets the path of MessagesDocument template.
        /// </summary>
        /// <remarks>
        /// A string that specifies the path of xslt template.
        /// </remarks>
        [JsonPropertyName("templatePath")]
        public string TemplatePath { get; set; }
        
        /// <summary>
        /// Determines whether or not default user credentials are used to access an SMTP server. The default value is false.
        /// </summary>
        /// <remarks>
        /// Returns: true indicates that default user credentials will be used to access the SMTP server; otherwise, false.
        /// </remarks>
        [JsonPropertyName("defaultCredentials")]
        public bool DefaultCredentials { get; set; }
        
        /// <summary>
        /// Gets or sets the user name to connect to an SMTP mail server.
        /// </summary>
        /// <remarks>
        /// A string that represents the user name to connect to an SMTP mail server.
        /// </remarks>
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        
        /// <summary>
        /// Gets or sets the user password to use to connect to an SMTP mail server.
        /// </summary>
        /// <remarks>
        /// A string that represents the password to use to connect to an SMTP mail server.
        /// </remarks>
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
