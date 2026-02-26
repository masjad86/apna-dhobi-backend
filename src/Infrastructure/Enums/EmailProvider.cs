namespace ApnaDhobi.Infrastructure.Enums;

/// <summary>
/// Enumeration representing different email providers that can be used for sending emails. 
/// </summary>
public enum EmailProviderType
{
    Smtp,
    SendGrid,
    Custom,
    Unknown
}