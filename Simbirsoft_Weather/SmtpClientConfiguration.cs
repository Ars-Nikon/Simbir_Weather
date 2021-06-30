namespace Simbirsoft_Weather
{
    public class SmtpClientConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Name { get; set; }
        public string MailboxAddress { get; set; }
        public string Password { get; set; }
    }
}
