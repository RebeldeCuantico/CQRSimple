using CQRSimple.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSimple.Handlers
{
    public class Dispatcher : IDispatcher
    {

        private readonly IServiceProvider _serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TOut> Send<TMessage, TOut>(TMessage message)
             where TMessage : class, IMessage<TOut>
        {
            var handler = _serviceProvider.GetService<IMessageHandler<TMessage, TOut>>();

            if (handler != null)
            {
                return await handler.Handle(message);
            }

            throw new NotImplementedException("No MessageHandler has been implemented.");            
        }

        public Task Publish<TNotification>(TNotification message)
            where TNotification : class, INotification
        {
            var handlers = _serviceProvider.GetServices<INotificationHandler<TNotification>>();

            if (handlers != null)
            {
                handlers.ToList()?.ForEach(async concreteHandler => await concreteHandler.Handle(message)); 
                return Task.CompletedTask;
            }

            throw new NotImplementedException("No NotificationHandler has been implemented.");            
        }
    }
}
