namespace VolunteerTracker.Web.Email
{
    public class EmailSenderOptions
    {
        public string? SendGridApiKey { get; set; }

        public string? SendGridUser { get; set; }

        public EmailAddresses? EmailAddresses { get; set; }
    }
}
