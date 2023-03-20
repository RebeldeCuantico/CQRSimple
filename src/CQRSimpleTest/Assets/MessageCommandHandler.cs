using CQRSimple.Contracts;

namespace CQRSimpleTest.Assets
{
    public class MessageCommandHandler : IMessageHandler<MessageCommand, int>
    {
        public Task<int> Handle(MessageCommand message)
        {
            return Task.FromResult(int.Parse(message.Number));
        }
    }
}
