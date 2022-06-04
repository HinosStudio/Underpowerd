using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Battery))]
public abstract class ChargeStation : MonoBehaviour, IConnectCallback {

    private readonly Dictionary<Connector, Battery> m_ConnectedBatteries = new Dictionary<Connector, Battery>();

    protected Battery m_Battery;

    public IEnumerable<Battery> Batteries => m_ConnectedBatteries.Values;

    protected virtual void Awake() {
        m_Battery = GetComponent<Battery>();
    }

    protected virtual void Update() {
        foreach (Battery battery in m_ConnectedBatteries.Values) {
            TransferCharge(battery);
        }
    }

    protected abstract void TransferCharge(Battery target);

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
