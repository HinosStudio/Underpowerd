using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Connector : MonoBehaviour {
    [SerializeField] private Transform connectionPoint;
    [SerializeField] private float maxDistance = 2.0f;
    
    [SerializeField] private int maxConnections;
    [SerializeField] private ConnectionWire wire;

    private ConnectionWire[] wires;
    private readonly Dictionary<ConnectionPoint, ConnectionWire> m_Connections = new Dictionary<ConnectionPoint, ConnectionWire>();

    public ConnectionEvent OnConnectEvent = new ConnectionEvent();
    public ConnectionEvent OnDisconnectEvent = new ConnectionEvent();

    public Vector3 Point => connectionPoint.position;
    public bool CanConnect => m_Connections.Count < maxConnections;

    private void Awake() {

        wires = new ConnectionWire[maxConnections];
        for(int i = 0; i < maxConnections; ++i) {
            wires[i] = Instantiate(wire, connectionPoint);
            wires[i].gameObject.SetActive(false);
        }
    }

    public bool ConnectToObject(ConnectionPoint other) {
        if(!m_Connections.ContainsKey(other)) {
            if (!CanConnect) {
                DisconnectOldestConnection();
            }

            var wire = wires.First(w => !m_Connections.ContainsValue(w));
            wire.Initialize(other.transform);

            m_Connections.Add(other, wire);
            OnConnectEvent.Invoke(this);
            
            return true;
        }
        
        return false;
    }

    public void DisconnectFromObject(ConnectionPoint other) {
        if (m_Connections.ContainsKey(other)) {
            m_Connections[other].gameObject.SetActive(false);
            m_Connections.Remove(other);
            OnDisconnectEvent.Invoke(this);
        }
    }

    public void DisconnectOldestConnection() {
        DisconnectFromObject(m_Connections.First().Key);
    }
}
