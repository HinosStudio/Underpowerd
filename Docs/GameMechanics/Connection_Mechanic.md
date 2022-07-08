# Connection mechanic

- The player can use the interaction key to connect to connectable objects.
- Interacting with the object again will break the connection.
- When connected an interaction happens between the two objects, defined by the object the player connects to.
- If the player moves out of a range of the object the connection will break.
- If the connection with the object is broken the interaction between the player and the object will interrupt.

- When a player is connected to a negative station, the src will transfer a charge to the target.
- When a player is connected to a positive station, the target will transfer a charge to the src.
- When a player is connected to a neutral station, the charge between the src and the target will normalize.

```C#

public class Connector : MonoBehaviour {
    [SerializeField] private int maxConnections;
    [SerializeField] private ConnectionWire wirePrefab;

    private ConnectionWire[] _wireInstances;
    private readonly Dictionary<ConnectionPoint, ConnectionWire> _connections = new Dictionary<ConnectionPoint, ConnectionWire>();

    private void Awake(){
        // Initialize wire instances
    }

    public bool ConnectToPoint(ConnectionPoint target) { }

    public bool DisconnectFromPoint(ConnectionPoint target) { }
}

public class ConnectionPoint : Monobehaviour {
    [SerializeField] private int maxConnections;

    private readonly List<Connector> _connections = new List<Connector>();

    public ReadOnlyCollection<Connector> Connections => _connections.AsReadOnly;

    public event OnConnectEvent;
    public event OnDisconnectEvent;

    public bool AddConnection(Connector source) { }

    public bool RemoveConnection(Connector source) { }
}

[RequireComponent(typeof(ConnectionPoint))]
public class ConnectionPointDistanceLimiter : MonoBehaviour {
    [SerializeField, Min(0)] private float maxDistance = 1f;

    private ConnectionPoint _connectionPoint;

    private void Awake(){
        // GetComponents
    }

    private void Update(){
        // Break connection when out of range
    }
}

```