using System;
using System.Collections.Generic;

public class EventBus : IEventBus
{
    private static Dictionary<EventType, Action> assignedAction = new();

    public void Raise(EventType eventType)
    {
        if(assignedAction.TryGetValue(eventType, out var existingAction))
            existingAction?.Invoke();
    }

    public void Subscribe(EventType eventType, Action callback)
    {
        if(!assignedAction.TryAdd(eventType, callback))
            assignedAction[eventType] += callback;
    }

    public void Unsubscribe(EventType eventType, Action callback)
    {
        if (assignedAction.ContainsKey(eventType))
        {
            assignedAction[eventType] -= callback;
        }
    }
} 
