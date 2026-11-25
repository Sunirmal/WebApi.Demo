using Microsoft.AspNetCore.Mvc;
using WebApi.Demo.Services;

namespace WebApi.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController:ControllerBase
    {
        private readonly NotificationService _notificationService1;
        private readonly NotificationService _notificationService2;

        public NotificationController(NotificationService notificationService1,
            NotificationService notificationService2)
        {
            _notificationService1 = notificationService1;
            _notificationService2 = notificationService2;
            Console.WriteLine(_notificationService1.SendNotification("Test message 1"));
            Console.WriteLine(_notificationService2.SendNotification("Test message 2"));
        }
    }
}
