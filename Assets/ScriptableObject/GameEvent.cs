using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent", order = 10)]
public class GameEvent : ScriptableObject {
    private readonly List<GameEventListener> eventListeners =
        new List<GameEventListener>();

    public void Raise() {
        for (var i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener) {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener) {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}