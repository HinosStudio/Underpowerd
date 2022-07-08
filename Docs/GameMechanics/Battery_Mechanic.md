# Battery mechanic

- Battery has a charge.
- Charge can increase.
- Charge can decrease.
- Charge can transfer between batteries.

```C#

public class Battery : MonoBehaviour {
    [SerializeField] private float initialCharge;
    [SerializeField] private float maxCharge;

    private float charge;

    private void Start() {
        // initialize values
    }

    public float AddCharge(float value) { }

    public float RemoveCharge(float value) { }

    public static void TransferCharge(Battery src, Battery target, float value) { }

    public static void NormalizeCharge(Battery src, Battery target) { }
}

```