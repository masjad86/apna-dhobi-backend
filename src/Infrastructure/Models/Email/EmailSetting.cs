using ApnaDhobi.Infrastructure.Enums;
using Microsoft.Extensions.Logging;
namespace ApnaDhobi.Infrastructure.Models;

/// <summary>
/// Represents the settings required for configuring an email sender. This class includes properties such as SMTP server, port, sender email, sender name, password, SSL enablement, and default credentials usage. It also includes an enumeration for the email provider to specify which email service is being used (e.g., SMTP, SendGrid, Custom). These settings are essential for establishing a connection to the email server and sending emails successfully.
/// </summary>
public class EmailSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string From { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool UseSsl { get; set; }
    public bool UseStartTls { get; set; }
    public bool Disabled { get; set; } = false;
    public int TimeoutMs { get; set; } = 10000; // Default timeout of 10 seconds
    public string RedirectTo { get; set; } = string.Empty; // For testing purposes, redirect all emails to this address
    public EmailProviderType Provider { get; set; } = EmailProviderType.Smtp;
    public ILogger? Logger { get; set; } // Optional logger for email sending operations
}