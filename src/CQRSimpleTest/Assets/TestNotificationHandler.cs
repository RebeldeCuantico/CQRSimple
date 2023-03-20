using CQRSimple.Contracts;
using System.Diagnostics;

namespace CQRSimpleTest.Assets
{
    public class TestNotificationHandler : INotificationHandler<TestNotification>
    {
        public Task Handle(TestNotification notification)
        {
            Debug.WriteLine($"{nameof(TestNotificationHandler)} - {notification.Name} ");
            return Task.CompletedTask;
        }
    }
}
