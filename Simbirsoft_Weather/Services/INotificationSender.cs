using System.Threading.Tasks;

namespace Simbirsoft_Weather.Services
{
    public interface INotificationSender
    {
        Task SendNotificationAsync(string email, string subject, string message);
    }
}
