# Connection mechanic

- The player can use the interaction key to connect to connectable objects.
- Interacting with the object again will break the connection.
- When connected an interaction happens between the two objects, defined by the object the player connects to.
- If the player moves out of a range of the object the connection will break.
- If the connection with the object is broken the interaction between the player and the object will interrupt.

- When a player is connected to a negative station, the src will transfer a charge to the target.
- When a player is connected to a positive station, the target will transfer a charge to the src.
- When a player is connected to a neutral station, the charge between the src and the target will normalize.

A connection can be established between two objects.
A connection will fail when there are no available ports available on one of the objects.
A connection will break when an object decides to.
A connection will break when the distance between the 2 objects is too far.

```C#

public class Connector : MonoBehaviour {
    public int maxConnections;
    public Connection[] connections;

    public event Action OnConnectEvent;
    public event Action OnDisconnectEvent;

    private void Awake(){
        connections = new Connection[maxConnections];
    }

    private void Update(){
        foreach(Connection c in connections){
            c.Update();
        }
    }

    public bool MakeConnection(Connection connection){
        if(connections.Length < maxConnections){
            connections[connections.length] = connection;
            OnConnectEvent?.Invoke();
            return true;
        }

        return false;
    }

    public bool BreakConnection(Connection connection){

    }
    
}

public class Connection : MonoBehaviour {
    public GameObject source;
    public GameObject target;

    public event Action OnBreakEvent;

    protected virtual void Update(){

    }

    public bool Connect(GameObject source, GameObject target){
        if(!source.CanConnect || !target.CanConnect) return false;
        
        this.source = source;
        this.target = target;

        source.AddConnection(this);
        target.AddConnection(this);
        this.gameObject.enabled = true;
    }

    public void Break(){
        source.RemoveConnection(this);
        target.RemoveConnection(this);
        this.gameObject.enabled = false;
    }
}

public class EnergyTransferConnection : Connection {
    public EnergyStore sourceBattery;
    public EnergyStore targetBattery;

    public EnergyTransferConnection(GameObject source, GameObject target) : base(source, target) {

    }

    public override void Update() {

    }
}

public class Generator : MonoBehaviour, IInteractable {

    public void OnInterract(GameObject source){
        var connector = source.GetComponent<Connector>();
        if(connector != null) {
            connector.MakeConnection(new EnergyTransferConnection(source, this.gameObject));
        }
    }
}

```