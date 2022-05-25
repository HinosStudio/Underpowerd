# Battery mechanic

- Battery has a charge.
- Charge can increase.
- Charge can decrease.
- Charge can transfer between batteries.

```C#

class Battery {
    private float charge;
    private float initialCharge;
    private float maxCharge;

    private float overCharge;
    private float maxOverCharge;

    event Depleted;
    event Augmented;

    public Battery(float initial, float maxCharge, float maxOverCharge, float decay = 0) { }

    // Methods
    public void AddCharge(float value) { }

    public void RemoveCharge(float value) { }

    // Procedures
    public static void TransferCharge(Battery src, Battery target, float value) { }

    public static void NormalizeCharge(Battery src, Battery target) { }
}

```