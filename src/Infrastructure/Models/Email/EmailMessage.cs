namespace ApnaDhobi.Infrastructure.Models;

/// <summary>
/// Represents an email message with properties for subject, body, recipients, and attachments. This class is used to encapsulate the details of an email that needs to be sent, including the content and metadata. It can be utilized by email sending services to construct and send emails based on the provided information.
/// </summary>
public sealed class EmailMessage
{
    /// <summary>
    /// The subject of the email. This property is required and should contain a brief summary of the email's content. It is used by email clients to display the email in the inbox and should be concise yet descriptive enough to give recipients an idea of what the email is about.
    /// </summary>
    public required string Subject { get; init; }

    /// <summary>
    /// The HTML body of the email. This property is required and should contain the main content of the email formatted in HTML. It allows for rich text formatting, including images, links, and other HTML elements, providing a visually appealing and structured email message to the recipients.
    /// </summary>
    public required string HtmlBody { get; init; }

    /// <summary>
    /// The collection of recipient email addresses. This property is required and should contain one or more email addresses to which the email will be sent. It can include multiple recipients, and the email sending service should handle sending the email to all specified addresses.
    /// </summary>
    public required IReadOnlyCollection<string> To { get; init; }

    /// <summary>
    /// The collection of email addresses to be included in the CC (carbon copy) field. This property is optional and can contain one or more email addresses that will receive a copy of the email. Recipients in the CC field are visible to all other recipients of the email.
    /// </summary>
    public IReadOnlyCollection<string>? Cc { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public IReadOnlyCollection<string>? Bcc { get; init; }

    /// <summary>
    /// The collection of attachments to be included with the email. This property is optional and can contain one or more EmailAttachment objects, each representing a file to be attached to the email. Attachments can include documents, images, or any other type of file that needs to be sent along with the email message.
    /// </summary>
    public IReadOnlyCollection<EmailAttachment>? Attachments { get; init; }

    /// <summary>
    /// Indicates whether the body of the email is in HTML format. This property is optional and defaults to true, meaning that the email body will be treated as HTML content. If set to false, the email body will be treated as plain text, and any HTML tags will be rendered as plain text rather than being interpreted as HTML elements.
     /// </summary>
    /// </summary>
    public bool IsBodyHtml { get; init; } = true;

    /// <summary>
    /// Additional fields that can be included in the email message. This property is optional and can contain a dictionary of key-value pairs representing any extra information that may be needed for processing or sending the email. These additional fields can be used to store custom metadata or parameters that are relevant to the email sending process.
     ///
    /// </summary>
    public IReadOnlyDictionary<string, string>? AdditionalFields { get; init; }

    public EmailMessage() { }
}

public sealed class EmailAttachment
{
    public required string FileName { get; init; }
    public required byte[] Content { get; init; }
    public string ContentType { get; init; } = "application/octet-stream";
}
