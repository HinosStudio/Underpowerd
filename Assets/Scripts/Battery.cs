using System;
using UnityEngine;
using UnityEngine.Events;

public class Battery : MonoBehaviour {
    [SerializeField] private float initialCharge = 50.0f;
    [SerializeField] private float maxCharge = 100.0f;

    // TODO: Add overcharge
    // [SerializeField] private float overcharge = 0;

    private bool depleted = false;
    public float Charge { get; private set; }

    public UnityEvent OnBatteryDepleted = new UnityEvent();
    public UnityEvent OnBatteryAugmented = new UnityEvent();

    public float ToFull => Mathf.Max(0, maxCharge - Charge);

    private void Awake() {
        Charge = initialCharge;
    }

    public void AddCharge(float value) {
        Charge += value;
        Charge = Mathf.Min(Charge, maxCharge);
    }

    public float RemoveCharge(float value) {
        if(value < 0) {
            Debug.LogError("cannot remove negative value", this);
            return 0;
        }

        var res = Mathf.Min(Charge, value);
        Charge -= res;

        if (!depleted && Charge <= 0) {
            depleted = true;
            OnBatteryDepleted.Invoke();
        }

        return res;
    }

    public static void TransferCharge(Battery src, Battery targetSrc, float value) {
        var maxCharge = Mathf.Min(targetSrc.ToFull, value);
        var charge = src.RemoveCharge(maxCharge);
        targetSrc.AddCharge(charge);
    }

    public static void NormalizeCharge(Battery src, Battery target) {

    }
}
