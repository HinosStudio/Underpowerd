using UnityEngine;

public interface IConnectCallback {
    void OnConnect(Connector other);
    void OnDisconnect(Connector other);
}


