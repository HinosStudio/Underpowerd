using System.Collections.Generic;
using UnityEngine;

public class ChargeStation : MonoBehaviour, IConnectCallback {

    private readonly Dictionary<Connector, Battery> m_ConnectedBatteries = new Dictionary<Connector, Battery>();

    public IEnumerable<Battery> Batteries => m_ConnectedBatteries.Values;

    public void OnConnect(Connector other) {
        var battery = other.GetComponent<Battery>();
        if (battery != null) {
            m_ConnectedBatteries.Add(other, battery);
        }
    }

    public void OnDisconnect(Connector other) {
        if (m_ConnectedBatteries.ContainsKey(other)) {
            m_ConnectedBatteries.Remove(other);
        }
    }
}
