using ApnaDhobi.Infrastructure.Models;
namespace ApnaDhobi.Infrastructure.Interfaces;

public interface IEmailSender
{
    /// <summary>
    /// Sends an email message with the specified parameters, including attachments. 
    /// This method should handle the logic for sending an email, including constructing the email message with attachments, 
    /// connecting to the email server, and handling any errors that may occur during the sending process. 
    /// The implementation can use various email sending libraries or services as needed.
    /// </summary>
    /// <param name="message"></param>
    void Send(EmailMessage message);

    /// <summary>
    /// Sends an email message with the specified parameters, including attachments. This method should handle the logic for sending an email, including constructing the email message with attachments, connecting to the email server, and handling any errors that may occur during the sending process. The implementation can use various email sending libraries or services as needed.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    Task SendAsync(EmailMessage message, CancellationToken cancellationToken);
}