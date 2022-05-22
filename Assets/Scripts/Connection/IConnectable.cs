using UnityEngine;

public interface IConnectable {
    Vector3 ConnectionPoint { get; }
    GameObject ConnectionObject { get; }

    void OnConnect(IConnectable other);
    void OnDisconnect(IConnectable other);
}


