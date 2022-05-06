using System;

namespace EscortBookUser.Handlers
{
    public interface IOperationHandler<T> where T : class
    {
        void Publish(T eventType);

        void Subscribe(string subscriberName, Action<T> action);
    }
}
