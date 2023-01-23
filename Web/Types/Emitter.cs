using EscortBookUser.Web.Handlers;

namespace EscortBookUser.Web.Types;

public static class Emitter<T> where T : class
{
    public static void EmitMessage(IOperationHandler<T> operationHandler, T message)
        => operationHandler.Publish(message);
}
