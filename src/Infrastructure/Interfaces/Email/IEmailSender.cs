using ApnaDhobi.Infrastructure.Models;
namespace ApnaDhobi.Infrastructure.Interfaces;

public interface IEmailSender
{
    /// <summary>
    /// Sends an email with the specified parameters. This method should handle the logic for sending an email, including constructing the email message, connecting to the email server, and handling any errors that may occur during the sending process. The implementation can use various email sending libraries or services as needed.
    /// </summary>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body content of the email.</param>
    /// <returns>A task that represents the asynchronous operation of sending the email.</returns>
    Task SendEmailAsync(string to, string subject, string body);

    /// <summary>
    /// Sends an email with the specified parameters and attachments. This method should handle the logic for sending an email, including constructing the email message with attachments, connecting to the email server, and handling any errors that may occur during the sending process. The implementation can use various email sending libraries or services as needed.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken);

    /// <summary>
    /// Sends an email with the specified parameters, including attachments. This method should handle the logic for sending an email, including constructing the email message with attachments, connecting to the email server, and handling any errors that may occur during the sending process. The implementation can use various email sending libraries or services as needed.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <param name="attachments"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendEmailAsync(string to, string subject, string body, IEnumerable<EmailMessage> attachments, CancellationToken cancellationToken);

    Task SendEmailAsync(EmailMessage message, CancellationToken cancellationToken);
}