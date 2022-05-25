using UnityEngine;

public class Connector : MonoBehaviour {
    [SerializeField] private Transform connectionPoint;
    [SerializeField] private float maxDistance = 2.0f;

    private ConnectionPoint m_Connection;

    public Transform ConnectionPoint => connectionPoint;
    public ConnectionPoint Connection => m_Connection;

    private void OnDisable() {
        Disconnect();
    }

    private void Update() {
        if (m_Connection != null) {
            var distance = Vector3.Distance(connectionPoint.position, m_Connection.Point);
            if (distance > maxDistance) {
                Disconnect();
            }
        }
    }

    public void ConnectToPoint(ConnectionPoint other) {
        if (m_Connection != null) {
            Disconnect();
        }

        if (other.AddConnector(this)) {
            m_Connection = other;
        }
    }

    public void Disconnect() {
        m_Connection?.RemoveConnector(this);
        m_Connection = null;
    }
}
