# EmailService
Email Service send SMTP email with/without message template.

### How to configure and use?

#### Step 1
Add SMTP configuration section to 'appsettings.json' file and set its values.
The following Configuration snippet shows example and default values:
```
  "SMTP": {
    "from": "Sender Name|noreply@yourcompany.com",
    "replyTo": "",
    "host": "mail.yourcompany.com",
    "port": 25,
    "enableSsl": false,
    "deliveryFormat": "International",
    "deliveryMethod": "Network",
    "pickupDirectoryLocation": null,
    "templatePath": "/XSLT/mail.xslt",
    "defaultCredentials": false,
    "userName": "noreply@yourcompany.com",
    "password": "noreply password"
  }
```
**Note:** You can use custom xslt template to create MessageDocument. [more](https://github.com/aras-it/Armaiti.Core/tree/master/Messaging#MessageDocument)

#### Step 2
Register SMTP configuration instance and add EmailService
```
public class Startup
{
    ...

    public void ConfigureServices(IServiceCollection services)
    {
        ...
        services.Configure<SmtpConfig>(Configuration.GetSection("SMTP"))
            .AddSingleton<IEmailService, EmailService>();
        ...
	}
}
```

#### Step 3
Add IEmailService to the class constructor as a parameter (use DI pattern)
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
        // simple method
        await _emailService.SendAsync(
            "user@companyname.com",
            "Armaiti Test Message",
            "<p>This is an e-mail message sent by Armaiti.Core EmailService while testing the configuration settings for your app.</p>");

        // multi recipients method
        await _emailService.SendAsync(new EmailMessage
        {
            MailTo = "user-1@companyname.com;user-2@companyname.com; ... ;user-n@companyname.com",
            Subject = "Armaiti Test Message",
            Body = "<p>This is an e-mail message sent by Armaiti.Core EmailService while testing the configuration settings for your app.</p>"
        });
    }
}
```
