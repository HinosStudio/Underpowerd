using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour {
    [SerializeField] private Transform connectionPoint;
    [SerializeField] private int maxConnections = 1;

    private readonly List<Connector> m_Connections = new List<Connector>();

    public ConnectionEvent onConnectEvent = new ConnectionEvent();
    public ConnectionEvent onDisconnectEvent = new ConnectionEvent();

    public Vector3 Point => connectionPoint.position;
    public ReadOnlyCollection<Connector> Connections => m_Connections.AsReadOnly();

    public bool AddConnector(Connector other) {
        if (m_Connections.Count < maxConnections && !m_Connections.Contains(other)) {
            if (other.ConnectToObject(this)) {
                m_Connections.Add(other);
                onConnectEvent.Invoke(other);
                return true;
            }
        }

        return false;
    }

    public bool RemoveConnector(Connector other) {
        if (m_Connections.Contains(other)) {
            other.DisconnectFromObject(this);
            m_Connections.Remove(other);
            onDisconnectEvent.Invoke(other);
            return true;
        }

        return false;
    }
}
