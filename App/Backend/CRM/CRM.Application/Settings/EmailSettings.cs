namespace CRM.Application.Settings;

public class EmailSettings
{
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string FromEmail { get; set; }
    public string Password { get; set; }
}