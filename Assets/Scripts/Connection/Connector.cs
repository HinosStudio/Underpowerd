using UnityEngine;

public class Connector : MonoBehaviour, IConnectable {
    [SerializeField] private Vector3 connectionPoint = Vector3.up;

    private Connection m_Connection;
    private Transform m_Transform;

    public Vector3 ConnectionPoint => m_Transform.position + connectionPoint;

    public GameObject ConnectionObject => this.gameObject;

    private void OnDisable() {
        m_Connection = null;
    }

    private void Awake() {
        m_Transform = GetComponent<Transform>();
    }

    public void Connect(IConnectable other) {
        m_Connection = new Connection(this, other);
    }

    public void OnConnect(IConnectable other) {
        
    }

    public void OnDisconnect(IConnectable other) {
        m_Connection = null;
    }
}
