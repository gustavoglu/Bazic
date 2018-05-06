namespace Bazic.Domain.Core.Events
{
    public interface IHandler<T> where T : Message
    {
        void Handler(T message);
    }
}
