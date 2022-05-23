using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NegativeChargeStation))]
public class BaseStationController : MonoBehaviour, IInteractable, IAreaCallback {

    [Header("GUI")]
    [SerializeField] private ProgressBar progress;

    private NegativeChargeStation m_ChargeStation;

    private void Awake() {
        m_ChargeStation = GetComponent<NegativeChargeStation>();
    }

    private void LateUpdate() {
        progress.Value = m_ChargeStation.Charge;
    }

    public void Interact(GameObject src) {
        Debug.Log($"{this.name}: started interaction with {src.name}", this);
        src.GetComponent<Connector>().Connect(m_ChargeStation);
    }

    public void OnAreaEnter(GameObject obj) {
        obj.GetComponent<InteractionController>()?.Register(this);
    }

    public void OnAreaExit(GameObject obj) {
        obj.GetComponent<InteractionController>()?.Unregister(this);
    }
}


