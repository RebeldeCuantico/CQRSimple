namespace CQRSimple.Contracts
{
    public interface IDispatcher
    {
        Task<TOut> Send<TMessage, TOut>(TMessage message)
             where TMessage : class, IMessage<TOut>;
    }
}
