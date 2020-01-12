using System.Xml;
using System.Xml.Serialization;

namespace Armaiti.Core.Messaging
{
    /// <summary>
    /// This class describes the values ​​of the Html message template for the <see cref="XmlDocument"/>.
    /// </summary>
    [XmlRoot(ElementName = "MessageDocument", IsNullable = false)]
    public class MessageDocument
    {
        /// <summary>
        /// Html message direction. This property is nullable.
        /// </summary>
        [XmlElement(IsNullable = true)]
        public string Direction { get; set; }

        /// <summary>
        /// Html message header text. This property is nullable.
        /// </summary>

        [XmlElement(IsNullable = true)]
        public string Header { get; set; }

        /// <summary>
        /// Html message title. This property is nullable.
        /// </summary>

        [XmlElement(IsNullable = true)]
        public string Title { get; set; }

        /// <summary>
        /// Html message body. This property is required.
        /// </summary>

        [XmlElement(IsNullable = false)]
        public string Body { get; set; }

        /// <summary>
        /// Embedded link in Html message. This property is nullable.
        /// </summary>
        [XmlElement(IsNullable = true)]
        public string Links { get; set; }

        /// <summary>
        /// Html message footer text. This property is nullable.
        /// </summary>
        [XmlElement(IsNullable = true)]
        public string Footer { get; set; }
    }
}
