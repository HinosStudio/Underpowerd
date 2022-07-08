using UnityEngine;

[RequireComponent(typeof(Battery))]
public class BatteryDecay : MonoBehaviour {
    public float decay;
    private Battery m_Battery;

    private void Awake() {
        m_Battery = GetComponent<Battery>();
    }

    private void Update() {
        m_Battery.RemoveCharge(decay * Time.deltaTime);
    }
}