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

class Connection {
    private IConnectable src, target;

    public Connection(IConnectable src, IConnectable target){
        this.src = src;
        this.target = target;
    }
}

interface IConnectCallback {
    void OnConnect(GameObject other);
    void OnDisconnect(GameObject other);
}

class Connector : MonoBehaviour {
    private GameObject m_Other;

    public void Connect(GameObject other) { }
}


```