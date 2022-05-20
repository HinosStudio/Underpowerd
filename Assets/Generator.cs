using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour, IAreaCallback, IInteractable {

    private Battery m_Battery;
    private Battery m_ConnectedSrc;

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
        obj.GetComponent<PlayerController>()?.Register(this);
    }

    public void OnAreaExit(GameObject obj) {
        obj.GetComponent<PlayerController>()?.Unregister(this);
        m_ConnectedSrc = null;
    }
}
