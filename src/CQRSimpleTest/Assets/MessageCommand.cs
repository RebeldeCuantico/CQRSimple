using CQRSimple.Contracts;

namespace CQRSimpleTest.Assets
{
    public class MessageCommand : IMessage<int>
    {
        public string Number { get; set; }
    }
}
