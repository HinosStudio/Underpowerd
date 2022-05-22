using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NegativeChargeStation))]
public class BaseStationController : MonoBehaviour, IAreaCallback, IGameEventListener {
    [SerializeField] private GameEvent interactionEvent;

    [Header("GUI")]
    [SerializeField] private ProgressBar progress;

    private NegativeChargeStation m_ChargeStation;

    private void OnDisable() {
        interactionEvent?.Unregister(this);
    }

    private void Awake() {
        m_ChargeStation = GetComponent<NegativeChargeStation>();
    }

    private void LateUpdate() {
        progress.Value = m_ChargeStation.Charge;
    }

    public bool Interact(GameObject src) {
        Debug.Log($"{this.name}: started interaction with {src.name}", this);
        src.GetComponentInChildren<Connector>().Connect(m_ChargeStation);
        return true;
    }

    public void OnAreaEnter(GameObject obj) {
        interactionEvent?.Register(this);
    }

    public void OnAreaExit(GameObject obj) {
        interactionEvent?.Unregister(this);
    }

    public void OnEventRaised(GameEvent e, GameObject src) {
        GameEvent.Dispatch<InteractionGameEvent>(e, () => { return Interact(src); });
    }
}


