using System.Collections.Generic;
using UnityEngine;

public interface IGameEventListener {
    void OnEventRaised(GameEvent e, GameObject src);
}

[CreateAssetMenu(fileName = "new Game Event", menuName = "Game Event")]
public class GameEvent : ScriptableObject {
    private readonly List<IGameEventListener> m_Listeners = new List<IGameEventListener>();
    private bool handled;

    public bool IsHandled => handled;

    public void Register(IGameEventListener listener) {
        if (!m_Listeners.Contains(listener))
            m_Listeners.Add(listener);
    }

    public void Unregister(IGameEventListener listener) {
        if (m_Listeners.Contains(listener))
            m_Listeners.Remove(listener);
    }

    public virtual void Raise(GameObject src) {
        for(int i = 0; i < m_Listeners.Count; ++i) {
            m_Listeners[i].OnEventRaised(this, src);
        }
    }

    public static void Dispatch<T>(GameEvent e, System.Func<bool> func, bool setHandled = false) where T : GameEvent {
        var res = false;
        if (e is T) res = func.Invoke();
        e.handled = setHandled && res;
    }
}
