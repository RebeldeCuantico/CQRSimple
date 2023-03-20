namespace CQRSimple.Contracts
{
    public interface INotificationHandler<TNotification>
        where TNotification : class, INotification
    {
        Task Handle(TNotification notification);
    }
}
