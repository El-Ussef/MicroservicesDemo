using MessageBus.Contract;

namespace MessageBus.Implementation;

public class NotificationPublisher<T> : INotificationPublisher<T> where T : class
{
    public Task SendNotification(T obj)
    {
        throw new NotImplementedException();
    }
}