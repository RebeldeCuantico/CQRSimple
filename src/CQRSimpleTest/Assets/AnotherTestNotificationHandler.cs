using CQRSimple.Contracts;
using System.Diagnostics;

namespace CQRSimpleTest.Assets
{
    public class AnotherTestNotificationHandler : INotificationHandler<TestNotification>
    {
        public Task Handle(TestNotification notification)
        {
            Debug.WriteLine($"{nameof(AnotherTestNotificationHandler)} - {notification.Name} ");
            return Task.CompletedTask;
        }
    }
}
