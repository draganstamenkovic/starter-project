using System;

public interface IEventBus
{
    void Raise(EventType eventType);
    void Subscribe(EventType eventType, Action callback);
    void Unsubscribe(EventType eventType, Action callback);
}