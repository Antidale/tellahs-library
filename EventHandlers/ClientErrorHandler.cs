using DSharpPlus;

namespace tellahs_library.EventHandlers
{
    public class ClientErrorHandler : IClientErrorHandler
    {
        public ValueTask HandleEventHandlerError(string name, Exception exception, Delegate invokedDelegate, object sender, object args)
        {
            throw new NotImplementedException();
        }

        public ValueTask HandleGatewayError(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
