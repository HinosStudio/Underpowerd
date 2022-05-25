using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ConnectionEvent : UnityEvent<Connector> { }

public class ConnectionPoint : MonoBehaviour {
    [SerializeField] private int maxConnections = 1;

    private Transform m_Transform;
    private readonly List<Connector> m_Connections = new List<Connector>();

    public ConnectionEvent connectEvent = new ConnectionEvent();
    public ConnectionEvent disconnectEvent = new ConnectionEvent();

    public Vector3 Point => m_Transform.position;
    public ReadOnlyCollection<Connector> Connections => m_Connections.AsReadOnly();

    private void Awake() {
        m_Transform = GetComponent<Transform>();
    }

    public bool AddConnector(Connector other) {
        if (m_Connections.Count < maxConnections && !m_Connections.Contains(other)) {
            m_Connections.Add(other);
            connectEvent.Invoke(other);
            return true;
        }

        return false;
    }

    public bool RemoveConnector(Connector other) {
        if (m_Connections.Contains(other)) {
            m_Connections.Remove(other);
            disconnectEvent.Invoke(other);
            return true;
        }

        return false;
    }
}
