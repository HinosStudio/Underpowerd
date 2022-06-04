using UnityEngine;

[RequireComponent(typeof(ConnectionPoint))]
public class ConnectionPointDistanceLimiter : MonoBehaviour {
    [SerializeField, Min(0.01f)] private float maxDistance = 1.0f;

    private ConnectionPoint m_ConnectionPoint;

    private void Awake() {
        m_ConnectionPoint = GetComponent<ConnectionPoint>();
    }

    private void Update() {
        var connections = m_ConnectionPoint.Connections;
        for(int i = 0; i < connections.Count; ++i) {
            var distance = Vector3.Distance(m_ConnectionPoint.Point, connections[i].Point);
            if(distance > maxDistance) {
                m_ConnectionPoint.RemoveConnector(connections[i]);
            }
        }
    }
}