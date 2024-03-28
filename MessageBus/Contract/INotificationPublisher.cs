namespace MessageBus.Contract;

public interface INotificationPublisher<in T> where T : class
{
    Task SendNotification(T obj);
}