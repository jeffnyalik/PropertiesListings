using PropertiesListings.Helpers;

namespace PropertiesListings.Services
{
    public interface IMailService
    {
        Task SendEmailAysnc(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(MailRequest sendWelcomeRequest);

    }
}
