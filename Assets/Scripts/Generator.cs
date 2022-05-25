using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Battery))]
[RequireComponent(typeof(ConnectionPoint))]
[RequireComponent(typeof(ChargeStation))]
public class Generator : MonoBehaviour, IInteractable, IAreaCallback {
    
    private Battery m_Battery;
    private ConnectionPoint m_ConnectionPoint;
    private ChargeStation m_ChargeStation;

    private void Awake() {
        m_Battery = GetComponent<Battery>();
        m_ConnectionPoint = GetComponent<ConnectionPoint>();
        m_ChargeStation = GetComponent<ChargeStation>();
    }

    private void Update() {
        foreach (Battery battery in m_ChargeStation.Batteries) {
            Battery.TransferCharge(m_Battery, battery, Time.deltaTime);
        }
    }

    public void Interact(GameObject src) {
        Debug.Log($"{this.name}: started interaction with {src.name}", this);
        src.GetComponent<Connector>()?.ConnectToPoint(m_ConnectionPoint);
    }

    public void OnAreaEnter(GameObject obj) {
        obj.GetComponent<InteractionController>()?.Register(this);
    }

    public void OnAreaExit(GameObject obj) {
        obj.GetComponent<InteractionController>()?.Unregister(this);
    }
}
