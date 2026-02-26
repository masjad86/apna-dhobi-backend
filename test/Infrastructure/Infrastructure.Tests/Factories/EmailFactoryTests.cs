using ApnaDhobi.Infrastructure.Factories;
using ApnaDhobi.Infrastructure.Models;
using ApnaDhobi.Infrastructure.Enums;
using System.Runtime.InteropServices;
namespace ApnaDhobi.Infrastructure.Tests.Factories;

public class EmailFactoryTests
{
    [Fact]
    public void CreateEmailMessage_ShouldReturnMessage()
    {
        var message = EmailFactory.CreateEmailMessage("Test Subject", "Test Body", new List<string> { "test@example.com" });
        Assert.Multiple(() =>
        {
            Assert.NotNull(message);
            Assert.Equal("Test Subject", message.Subject);
            Assert.Equal("Test Body", message.HtmlBody);
            Assert.Equal("test@example.com", message.To.First());
        });
    }

    [Fact]
    public void CreateEmailSettings_ShouldReturnSettings()
    {
        var settings = EmailFactory.CreateEmailSetting(
            host: "smtp.example.com",
            port: 587,
            fromEmail: "test@example.com",
            fromName: "Test Sender",
            userName: "testuser",
            password: "testpassword",
            useSsl: true,
            useStartTls: true,
            disabled: false,
            timeout: 10000,
            redirectTo: "",
            provider: EmailProviderType.Smtp);
        Assert.Multiple(() =>
        {
            Assert.NotNull(settings);
            Assert.Equal("smtp.example.com", settings.Host);
            Assert.Equal(587, settings.Port);
            Assert.Equal("test@example.com", settings.From);
            Assert.Equal("Test Sender", settings.FromName);
            Assert.Equal("testuser", settings.UserName);
            Assert.Equal("testpassword", settings.Password);
            Assert.True(settings.UseSsl);
            Assert.True(settings.UseStartTls);
            Assert.False(settings.Disabled);
            Assert.Equal(10000, settings.TimeoutMs);
            Assert.Equal("", settings.RedirectTo);
            Assert.Equal(EmailProviderType.Smtp, settings.Provider);
            Assert.Null(settings.Logger);
        });
    }

    [Theory]
    [InlineData(EmailProviderType.Smtp)]
    public void CreateSender_ShouldReturnSender(EmailProviderType provider)
    {
        var settings = IOptionsMonitor<EmailSettings>.Create(new EmailSettings { Provider = provider });
        var sender = EmailFactory.CreateSender(settings);
        Assert.NotNull(sender);
        Assert.IsAssignableFrom<IEmailSender>(sender);
        Assert.Equal(provider, sender.Provider);
    } 

    [Theory]
    [InlineData(EmailProviderType.SendGrid, "SendGrid email sender is not implemented yet.")]
    public void CreateSender_WhenNotImplemented_ShouldThrowException(EmailProviderType provider, string expectedMessage)
    {
        var settings = IOptionsMonitor<EmailSettings>.Create(new EmailSettings { Provider = provider });
        var exception = Assert.Throws<NotImplementedException>(() => EmailFactory.CreateSender(settings));
        Assert.NotNull(exception);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Fact]
    public void CreateSender_UnknownProvider_ShouldThrowException()
    {
        var settings = IOptionsMonitor<EmailSettings>.Create(new EmailSettings { Provider = (EmailProviderType)999 });
        var exception = Assert.Throws<ArgumentException>(() => EmailFactory.CreateSender(settings));
        Assert.NotNull(exception);
        Assert.Equal("Invalid email provider specified in email settings.", exception.Message);
    }   
}