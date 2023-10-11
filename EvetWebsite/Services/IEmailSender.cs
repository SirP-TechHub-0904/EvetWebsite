namespace EvetWebsite.Services
{
    public interface IEmailSender
    {
        Task<bool> SendAsync(string message, string recipient, string subject);

    }
}
