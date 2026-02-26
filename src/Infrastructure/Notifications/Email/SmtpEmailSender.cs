using ApnaDhobi.Infrastructure.Models;
using ApnaDhobi.Infrastructure.Interfaces;
using ApnaDhobi.Infrastructure.Enums;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;


namespace ApnaDhobi.Infrastructure.Notifications;

/// <summary>
/// Implementation of the IEmailSender interface using SMTP protocol. This class is responsible for sending emails using the SMTP server specified in the email settings. It constructs the email message, connects to the SMTP server, and handles the sending process, including error handling and logging. The class uses the MailKit library for SMTP communication and supports features such as SSL/TLS encryption and authentication based on the provided email settings.
/// </summary>
public sealed class SmtpEmailSender(IOptionsMonitor<EmailSettings> options) : IEmailSender
{
    public void Send(EmailMessage message)
    {
        SendAsync(message).GetAwaiter().GetResult();
    }

    public async Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
    {
        if (message is null) throw new ArgumentNullException(nameof(message));
        if (message.To is null || message.To.Count == 0)
            throw new ArgumentException("At least one recipient is required.", nameof(message));

        var settings = options.CurrentValue;
        var logger = settings.Logger ?? new LoggerFactory().CreateLogger<SmtpEmailSender>();

        if (settings.Disabled)
        {
            logger.LogWarning("Email sending is disabled. Subject: {Subject}", message.Subject);
            return;
        }

        var mime = BuildMimeMessage(message, settings);

        using var client = new SmtpClient();
        client.Timeout = settings.TimeoutMs;

        try
        {
            var secureSocket = ResolveTlsMode(settings);

            logger.LogDebug("Connecting SMTP {Host}:{Port} TLS:{TlsMode}", settings.Host, settings.Port, secureSocket);

            await client.ConnectAsync(settings.Host, settings.Port, secureSocket, cancellationToken);

            if (!string.IsNullOrWhiteSpace(settings.UserName))
            {
                await client.AuthenticateAsync(settings.UserName, settings.Password, cancellationToken);
            }

            await client.SendAsync(mime, cancellationToken);

            logger.LogInformation(
                "Email sent. To:{To} Subject:{Subject}",
                string.Join(",", mime.To.Select(x => x.ToString())),
                message.Subject);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email. Subject: {Subject}", message.Subject);
            throw;
        }
        finally
        {
            if (client.IsConnected)
                await client.DisconnectAsync(true, cancellationToken);
        }
    }

    private static SecureSocketOptions ResolveTlsMode(EmailSettings settings)
    {
        if (settings.UseSsl) return SecureSocketOptions.SslOnConnect;
        if (settings.UseStartTls) return SecureSocketOptions.StartTls;
        return SecureSocketOptions.None;
    }

    private static MimeMessage BuildMimeMessage(EmailMessage message, EmailSettings settings)
    {
        var mime = new MimeMessage();

        // From
        mime.From.Add(new MailboxAddress(settings.FromName, settings.From));
        mime.Subject = message.Subject;

        // Redirect mode (useful for DEV/UAT)
        if (!string.IsNullOrWhiteSpace(settings.RedirectTo))
        {
            mime.To.Add(MailboxAddress.Parse(settings.RedirectTo));
        }
        else
        {
            AddRecipients(mime.To, message.To);
            AddRecipients(mime.Cc, message.Cc);
            AddRecipients(mime.Bcc, message.Bcc);
        }

        // Body + attachments
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = message.HtmlBody
        };

        if (message.Attachments is not null)
        {
            foreach (var a in message.Attachments)
            {
                bodyBuilder.Attachments.Add(a.FileName, a.Content, ContentType.Parse(a.ContentType));
            }
        }

        mime.Body = bodyBuilder.ToMessageBody();
        return mime;
    }

    private static void AddRecipients(InternetAddressList list, IReadOnlyCollection<string>? recipients)
    {
        if (recipients is null) return;

        foreach (var r in recipients.Where(x => !string.IsNullOrWhiteSpace(x)))
        {
            list.Add(MailboxAddress.Parse(r));
        }
    }
}