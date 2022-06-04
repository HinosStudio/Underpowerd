using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Battery))]
[RequireComponent(typeof(ConnectionPoint))]
public class NegativeChargeStation : ChargeStation, IInteractable, IAreaCallback {

    [Header("GUI")]
    [SerializeField] private ProgressBar progress;
    
    private ConnectionPoint m_ConnectionPoint;
    private ChargeStation m_ChargeStation;

    protected override void Awake() {
        base.Awake();
        m_ConnectionPoint = GetComponent<ConnectionPoint>();
        m_ChargeStation = GetComponent<ChargeStation>();
    }

    private void LateUpdate() {
        progress.Value = m_Battery.Charge;
    }

    protected override void TransferCharge(Battery target) {
        Battery.TransferCharge(target, m_Battery, Time.deltaTime * 2);
    }

    public void Interact(GameObject src) {
        Debug.Log($"{this.name}: started interaction with {src.name}", this);
        var connector = src.GetComponent<Connector>();
        if(connector) m_ConnectionPoint.AddConnector(connector);
    }

    public void OnAreaEnter(GameObject obj) {
        obj.GetComponent<InteractionController>()?.Register(this);
    }

    public void OnAreaExit(GameObject obj) {
        obj.GetComponent<InteractionController>()?.Unregister(this);
    }
}

