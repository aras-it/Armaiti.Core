# MessageDocument
MessageDocument is an xml serializable class for serializing an email message template. This class has a required attribute called Body and 5 optional attributes called Direction, Header, Title, Links and Footer. The XSLT template for MessageDocument can be as follows:

```
<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:template match="/">
    <html>
      <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=yes" />
        <style>
          body { direction: <xsl:value-of select="/MessageDocument/Direction"/>; }
          .messageBody{ direction: <xsl:value-of select="/MessageDocument/Direction"/>; width: 100%; }
          .header{ font-size: xx-small; }
          .title{ font-weight: bolder; }
          .description{ font-family: Tahoma; }
          .links{ text-decoration: none; }
          .footer{ font-size: xx-small; }
        </style>
      </head>
      <body>
        <table class="messageBody">
          <tr>
            <td>
              <div class="header">
                <xsl:value-of select="/MessageDocument/Header"/>
              </div>
              <br/>
            </td>
          </tr>
          <tr>
            <td>
              <div class="title">
                <xsl:value-of select="/MessageDocument/Title"/>
              </div>
              <br/>
            </td>
          </tr>
          <tr>
            <td>
              <br/>
              <div class="description">
                <xsl:value-of select="/MessageDocument/Body"/>
              </div>
              <br/>
            </td>
          </tr>
          <tr>
            <td>
              <div class="links">
                <xsl:value-of select="/MessageDocument/Links"/>
              </div>
            </td>
          </tr>
          <tr>
            <td>
              <br/>
              <div class="footer">
                <xsl:value-of select="/MessageDocument/Footer"/>
              </div>
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
```
### Properties
Name | Description | Required
---- | ----------- | --------
**Direction** | Html message direction. | The default value depends on the current CultureInfo
**Header** | Html message header text. | 
**Title** | Html message title. | 
**Body** | Html message body. | :heavy_check_mark:
**Links** | Embedded link in Html message. | 
**Footer** | Html message footer text. | 
  
### How to use?
Create an EmailMessage and set MessageDocument proerties
```
public class EmailSample
{
    private readonly IEmailService _emailService;

    public EmailSample(IEmailService emailService)
    {
        _emailService = emailService;
    }

    ...

    public async Task SendEmailAsync()
    {
        var message = new EmailMessage
        {
            MailTo = "user@companyname.com",
            MessageDocument = new MessageDocument
            {
                Body = "This is an e-mail message sent by Armaiti.Core EmailService while testing the MessageDocument template.",
                Footer = "This is footer of the test message",
                Header = "This is header of the test message",
                Title = "This is title of the test message"
            },
            Subject = "Test message"
        };
        _emailService.Send(message);
    }
}
```

See [here](https://github.com/aras-it/Armaiti.Core/Services#EmailService) for more information on configuring SMTP.