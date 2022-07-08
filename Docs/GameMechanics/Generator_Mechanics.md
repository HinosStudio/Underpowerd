# Generator Mechanic

- Generator has a battery
- Generator has a connection point

```C#

public class Generator : MonoBehaviour, IInteractionCallback, IConnectCallback {
    [SerializeField] private float connectionRange;

    [SerializeField] private Battery battery;
    [SerializeField] private ConnectionPoint connectionPoint;
    [SerializeField] private RegisterInteractableTriggerArea triggerArea;

    private readonly Dictionary<Connector, Battery> _connectedBatteries;

    private void Awake() {
        triggerArea.Interactable = this;
    }

    private void Update() {
        // Transfer charge to connected batteries
        // Check if any connections are out of range
    }

    public void OnInteract(GameObject source){
        // try to start a connection with source
    }

    public void OnConnect() {
        // Add battery to connected batteries
    }

    public void OnDisconnect() {
        // Remove battery from connected batteries
    }
}

```