using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour, IAreaCallback, IInteractable, IGameEventListener {
    [SerializeField] private GameEvent interactionEvent;

    private Battery m_Battery;
    private Battery m_ConnectedSrc;

    private void OnDisable() {
        interactionEvent?.Unregister(this);
    }

    private void Awake() {
        m_Battery = GetComponent<Battery>();
    }

    private void Update() {
        if (m_ConnectedSrc != null) {
            Battery.TransferCharge(m_Battery, m_ConnectedSrc, Time.deltaTime);
        }
    }

    public void Interact(GameObject src) {
        m_ConnectedSrc = src.GetComponent<Battery>();
    }

    public void OnAreaEnter(GameObject obj) {
        interactionEvent?.Register(this);
    }

    public void OnAreaExit(GameObject obj) {
        interactionEvent?.Unregister(this);
        m_ConnectedSrc = null;
    }

    public void OnEventRaised(GameEvent e, GameObject src) {
        Debug.Log($"{this.name}: started interaction", this);
    }
}
