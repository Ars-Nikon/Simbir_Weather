using Microsoft.AspNetCore.Mvc;
using Simbirsoft_Weather.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        public async Task<IActionResult> Post(int notificationId)
        {
            await _notificationService.EchoAsync(notificationId);
            return Ok();
        }
    }
}
