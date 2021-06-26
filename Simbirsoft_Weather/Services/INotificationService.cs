using System.Threading.Tasks;

namespace Simbirsoft_Weather.Services
{
    public interface INotificationService
    {
        Task EchoAsync(int notificationId);
    }
}
