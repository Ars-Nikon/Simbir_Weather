using Microsoft.AspNetCore.Mvc;
using Simbirsoft_Weather.Services;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Controllers
{
    [Route("[controller]")]
    public class SendNotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public SendNotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Calls the method sending the notification.
        /// </summary>
        /// <param name="notificationId">Unique event identifier.</param>
        /// <returns><see cref="OkResult"/> when the action is done.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(int notificationId)
        {
            await _notificationService.EchoAsync(notificationId);
            return Ok();
        }
    }
}
