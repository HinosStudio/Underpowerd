# Base Station Mechanics

- Base station has a battery
- Base station has a connection point
- Base station has a trigger area

- Base station can be interacted with

```C#

public class BaseStation : MonoBehaviour, IInteractCallback IConnectCallback {
    [SerializeField] private float connectionRange;
    
    [SerializeField] private Battery battery;
    [SerializeField] private ConnectionPoint connectionPoint;
    [SerializeField] private RegisterInteractableTriggerArea triggerArea;

    private readonly Dictionary<Connector, Battery> _connectedBatteries;

    private void Awake() {
        triggerArea.Interactable = this;
    }

    private void Update() {
        // Transfer charge from connected batteries
        // Check if any connections are out of range
    }

    public void OnInteract(GameObject src) {
        // Try to start a connection with src
    }

    public void OnConnect(Connector other) {
        // Add battery to connected batteries
    }

    public void OnDisconnect(Connector other) {
        // Remove battery from connected batteries
    }
}

```