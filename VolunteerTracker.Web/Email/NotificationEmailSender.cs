using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace VolunteerTracker.Web.Email
{
    public class NotificationEmailSender : IEmailSender
    {
        public NotificationEmailSender(IOptions<EmailSenderOptions> optionsAccessor)
        {
            if (optionsAccessor == null) { throw new ArgumentNullException(nameof(optionsAccessor)); }

            SendGridApiKey = optionsAccessor.Value?.SendGridApiKey
                ?? throw new ArgumentException($"Missing {nameof(optionsAccessor.Value.SendGridApiKey)}", nameof(optionsAccessor));

            SendGridUser = optionsAccessor.Value?.SendGridUser
                ?? throw new ArgumentException($"Missing {nameof(optionsAccessor.Value.SendGridUser)}", nameof(optionsAccessor));

            NotificationsEmailAddress = optionsAccessor.Value?.EmailAddresses?.Notifications
                ?? throw new ArgumentException($"Missing {nameof(optionsAccessor.Value.EmailAddresses)}.{nameof(optionsAccessor.Value.EmailAddresses.Notifications)}", nameof(optionsAccessor));
        }

        private string SendGridApiKey { get; }

        private string SendGridUser { get; }

        private string NotificationsEmailAddress { get; }

        public Task SendEmailAsync(string email,
                                   string subject,
                                   string htmlMessage)
        {
            var client = new SendGridClient(SendGridApiKey);

            var message = new SendGridMessage()
            {
                From = new EmailAddress(NotificationsEmailAddress, SendGridUser),
                Subject = subject,
                HtmlContent = htmlMessage
            };

            message.AddTo(new EmailAddress(email));

            message.SetClickTracking(enable: false, enableText: false);

            return client.SendEmailAsync(message);
        }
    }
}
