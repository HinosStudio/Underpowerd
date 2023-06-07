using System;
using UnityEngine;
using UnityEngine.Events;

public class Battery : MonoBehaviour {
    public float initialCharge = 50.0f;
    public float maxCharge = 100.0f;
    public float charge;

    // TODO: Add overcharge
    // [SerializeField] private float overcharge = 0;

    private bool depleted = false;

    // Events
    public UnityEvent OnBatteryDepleted = new();
    public UnityEvent OnBatteryAugmented = new();

    public float ToFull => Mathf.Max(0, maxCharge - charge);
    public bool Depleted => charge <= 0;

    private void Awake() {
        charge = initialCharge;
    }

    public void AddCharge(float value) {
        charge += value;
        charge = Mathf.Min(charge, maxCharge);
    }

    public float RemoveCharge(float value) {
        if(value < 0) {
            Debug.LogError("cannot remove negative value", this);
            return 0;
        }

        var res = Mathf.Min(charge, value);
        charge -= res;

        if (!depleted && charge <= 0) {
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
