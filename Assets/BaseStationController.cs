using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Battery))]
public class BaseStationController : MonoBehaviour, IInteractable, IAreaCallback {
    [SerializeField] private ProgressBar progress;

    private Battery m_Battery;
    private Battery m_ConnectedSrc;

    private void Awake() {
        m_Battery = GetComponent<Battery>();
    }

    private void Update() {
        if(m_ConnectedSrc != null) {
            Battery.TransferCharge(m_ConnectedSrc, m_Battery, Time.deltaTime);
        }
    }

    private void LateUpdate() {
        progress.Value = m_Battery.Charge;
    }

    private void OnGUI() {
        GUI.Box(new Rect(10, 100, 100, 100), "Base Station");
        GUI.Label(new Rect(10, 140, 100, 20), $"{m_Battery.Charge:n2}%");
    }

    public void Interact(GameObject src) {
        Debug.Log($"{this.name}: started interaction", this);
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


