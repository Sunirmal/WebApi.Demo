namespace WebApi.Demo.Services
{
    public class NotificationService
    {
        public Guid SendNotification(string message)
        {
            return Guid.NewGuid();
        }
    }
}
