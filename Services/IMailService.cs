using PropertiesListings.Helpers;

namespace PropertiesListings.Services
{
    public interface IMailService
    {
        Task SendEmailAysnc(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(string toEmail, string subject, string message);
        Task SendOnboardingMessage(string toEmail, string subject, string message);
        Task SendEmailWithAttachmentAsync(MailRequest mailRequest);

    }
}
