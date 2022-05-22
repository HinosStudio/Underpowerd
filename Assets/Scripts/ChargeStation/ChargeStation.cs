using UnityEngine;

[RequireComponent(typeof(Battery))]
public abstract class ChargeStation : MonoBehaviour, IConnectable {
    [SerializeField] private Vector3 connectionPoint = Vector3.zero;

    protected Battery targetBattery;
    
    protected Battery m_Battery;

    public Vector3 ConnectionPoint => connectionPoint;
    public GameObject ConnectionObject => this.gameObject;
    public float Charge => m_Battery.Charge;

    private void OnDisable() {
        targetBattery = null;
    }

    private void Awake() {
        m_Battery = GetComponent<Battery>();
    }

    private void Update() {
        if (targetBattery != null)
            TransferCharge();
    }

    public abstract void TransferCharge();

    public void OnConnect(IConnectable other) {
        targetBattery = other.ConnectionObject.GetComponent<Battery>();
    }

    public void OnDisconnect(IConnectable other) {
        targetBattery = null;
    }
}
