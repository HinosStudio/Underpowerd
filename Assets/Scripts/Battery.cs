using UnityEngine;

public class Battery : MonoBehaviour {
    [SerializeField] private float initialCharge = 50.0f;
    [SerializeField] private float maxCharge = 100.0f;

    // TODO: Add decay
    // [SerializeField] private float decay = 0;

    // TODO: Add overcharge
    // [SerializeField] private float overcharge = 0;

    private float m_Charge;

    public float Charge => m_Charge;

    public float ToFull => Mathf.Max(0, maxCharge - m_Charge);

    private void Awake() {
        m_Charge = initialCharge;
    }

    private void AddCharge(float value) {
        m_Charge += value;
        m_Charge = Mathf.Min(m_Charge, maxCharge);
    }

    private float RemoveCharge(float value) {
        if(value < 0) {
            Debug.LogError("cannot remove negative value", this);
            return 0;
        }

        var res = Mathf.Min(m_Charge, value);
        m_Charge -= res;
        return res;
    }

    public static void TransferCharge(Battery src, Battery targetSrc, float value) {
        var maxCharge = Mathf.Min(targetSrc.ToFull, value);
        var charge = src.RemoveCharge(maxCharge);
        targetSrc.AddCharge(charge);
    }
}
