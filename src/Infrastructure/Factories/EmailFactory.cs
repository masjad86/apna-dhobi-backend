using ApnaDhobi.Infrastructure.Enums;
using ApnaDhobi.Infrastructure.Models;
using ApnaDhobi.Infrastructure.Interfaces;
using ApnaDhobi.Infrastructure.Notifications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace ApnaDhobi.Infrastructure.Factories;

/// <summary>
/// Factory class for creating email-related objects such as EmailMessage, EmailSettings, and IEmailSender instances. This class provides static methods to construct these objects with the necessary parameters, allowing for a centralized and consistent way to create email messages and configure email sending settings. The factory also includes logic to create the appropriate IEmailSender implementation based on the specified email provider in the settings, enabling flexibility in choosing different email sending mechanisms (e.g., SMTP, SendGrid, Custom) without changing the client code that uses the factory.
/// </summary>
public sealed class EmailFactory
{
    /// <summary>
    /// Creates an EmailMessage instance with the specified parameters, including subject, HTML body, recipients (To, CC, BCC), attachments, and additional fields. This method provides a convenient way to construct an email message with all necessary information in a single call, ensuring that the message is properly initialized and ready to be sent using an IEmailSender implementation.
    /// </summary>
    /// <param name="subject">Subject of the email message</param>
    /// <param name="htmlBody">HTML body content of the email message</param>
    /// <param name="to">List of recipient email addresses in the To field</param>
    /// <param name="cc">List of recipient email addresses in the CC field (optional)</param>
    /// <param name="bcc">List of recipient email addresses in the BCC field (optional)</param>
    /// <param name="attachments">List of attachments to include in the email message (optional)</param>
    /// <param name="isBodyHtml">Allow html as a email body.</param>
    /// <param name="additionalFields">Addtional data to be used in template body</param>
    /// <returns></returns>
    public static EmailMessage CreateEmailMessage(string subject, 
        string htmlBody, IReadOnlyCollection<string> to, 
        IReadOnlyCollection<string>? cc = null, 
        IReadOnlyCollection<string>? bcc = null, 
        IReadOnlyCollection<EmailAttachment>? attachments = null,
        bool isBodyHtml = true,
        IReadOnlyDictionary<string, string>? additionalFields = null) 
            => new EmailMessage()
            {
                Subject = subject,
                HtmlBody = htmlBody,
                To = to,
                Cc = cc,
                Bcc = bcc,
                Attachments = attachments,
                IsBodyHtml = isBodyHtml,
                AdditionalFields = additionalFields
            };

    public static EmailSettings CreateEmailSetting(string host, 
        int port, 
        string fromEmail, 
        string fromName, 
        string userName,
        string password, 
        bool useSsl = false, 
        bool useStartTls = false,
        bool disabled = false,
        int timeout = 10000,
        string redirectTo = "",
        EmailProviderType provider = EmailProviderType.Smtp)
    {
        return new EmailSettings
        {
            Host = host,
            Port = port,
            From = fromEmail,
            FromName = fromName,
            Password = password,
            UseSsl = useSsl,
            UseStartTls = useStartTls,
            UserName = userName,
            Disabled = disabled,
            TimeoutMs = timeout,
            RedirectTo = redirectTo,
            Provider = provider
        };
    }

    public static EmailAttachment CreateEmailAttachment(string fileName, byte[] content, string contentType)
    {
        return new EmailAttachment
        {
            FileName = fileName,
            Content = content,
            ContentType = contentType
        };
    }

    /// <inheritdoc/>
    public static IEmailSender CreateSender(IOptionsMonitor<EmailSettings> options)
    {
        var settings = options.CurrentValue;
        return settings.Provider switch
        {
            EmailProviderType.Smtp => new SmtpEmailSender(options),
            EmailProviderType.SendGrid => throw new NotImplementedException("SendGrid email sender is not implemented yet."),// Implement SendGridEmailSender and return an instance here
            _ => throw new ArgumentException("Invalid email provider specified in email settings."),
        };
    }
}