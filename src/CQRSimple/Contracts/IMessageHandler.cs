namespace CQRSimple.Contracts
{
    public interface IMessageHandler<TMessage, TOut>
        where TMessage : class, IMessage<TOut>
    {
        Task<TOut> Handle(TMessage message);
    }
}
