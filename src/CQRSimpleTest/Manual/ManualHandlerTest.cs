using CQRSimple.Contracts;
using CQRSimple.Handlers;
using CQRSimpleTest.Assets;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSimpleTest.Manual
{
    public class ManualHandlerTest
    {
        [Fact]
        public async Task SendEventHandler() //For manual testing
        {
            var serviceProvider = new ServiceCollection()
           .AddSingleton<IMessageHandler<MessageCommand, int>, MessageCommandHandler>()
           .BuildServiceProvider();

            var dispatch = new Dispatcher(serviceProvider);
            var @event = new MessageCommand { Number = "5" };
            var result = await dispatch.Send<MessageCommand, int>(@event);
            result.Should().Be(5);
        }

        [Fact]
        public async Task PublishANotification() //For manual testing
        {
            var serviceProvider = new ServiceCollection()
           .AddSingleton<INotificationHandler<TestNotification>, TestNotificationHandler>()
           .AddSingleton<INotificationHandler<TestNotification>, AnotherTestNotificationHandler>()
           .BuildServiceProvider();

            var dispatch = new Dispatcher(serviceProvider);
            var @event = new TestNotification { Name = "Ismael" };
            await dispatch.Publish(@event);
        }
    }
}
