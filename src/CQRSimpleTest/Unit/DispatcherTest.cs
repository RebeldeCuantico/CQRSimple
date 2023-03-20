using CQRSimple.Contracts;
using CQRSimple.Handlers;
using CQRSimpleTest.Assets;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CQRSimpleTest.Unit
{
    public class DispatcherTest
    {
        [Fact]
        public async Task Dispatch_Send_Event_Without_Handlers_Should_Throw_a_Exception()
        {
            var mock = new Mock<IServiceProvider>();
            var dispatcher = new Dispatcher(mock.Object);
            var @event = new MessageCommand { Number = "5" };

            Func<Task> func = async () => await dispatcher.Send<MessageCommand, int>(@event);

            await func.Should()
                      .ThrowExactlyAsync<NotImplementedException>()
                      .WithMessage("No MessageHandler has been implemented.");
        }

        [Fact]
        public async Task Dispatch_Send_Event_With_Handler_Should_Works()
        {
            var messageHandlerMock = new Mock<IMessageHandler<MessageCommand, int>>();
            var @event = new MessageCommand { Number = "5" };
            messageHandlerMock.Setup(s => s.Handle(@event));

            var serviceCollection = new ServiceCollection()
            .AddSingleton<IMessageHandler<MessageCommand, int>>(messageHandlerMock.Object)
            .BuildServiceProvider();

            var dispatcher = new Dispatcher(serviceCollection);

            await dispatcher.Send<MessageCommand, int>(@event);

            messageHandlerMock.Verify(s => s.Handle(@event), Times.Once);
        }

        [Fact]
        public async Task Dispatch_Publish_Event_Without_Handlers_Should_Throw_a_Exception()
        {
            var sp = new ServiceCollection().BuildServiceProvider();
            var dispatcher = new Dispatcher(sp);
            var @event = new TestNotification { Name = "Ismael" };

            Func<Task> func = async () => await dispatcher.Publish<TestNotification>(@event);

            await func.Should()
                      .ThrowExactlyAsync<NotImplementedException>()
                      .WithMessage("No NotificationHandler has been implemented.");
        }

        [Fact]
        public async Task Dispatch_Publish_Event_With_Handlers_Should_Works()
        {
            var notificationHandlerMock = new Mock<INotificationHandler<TestNotification>>();
            var secondNotificationHandlerMock = new Mock<INotificationHandler<TestNotification>>();

            var @event = new TestNotification { Name = "Ismael" };
            notificationHandlerMock.Setup(s => s.Handle(@event));
            secondNotificationHandlerMock.Setup(s => s.Handle(@event));

            var serviceCollection = new ServiceCollection()
            .AddSingleton<INotificationHandler<TestNotification>>(notificationHandlerMock.Object)
            .AddSingleton<INotificationHandler<TestNotification>>(secondNotificationHandlerMock.Object)
            .BuildServiceProvider();

            var dispatcher = new Dispatcher(serviceCollection);

            await dispatcher.Publish<TestNotification>(@event);

            notificationHandlerMock.Verify(s => s.Handle(@event), Times.Once);
            secondNotificationHandlerMock.Verify(s => s.Handle(@event), Times.Once);
        }
    }
}
