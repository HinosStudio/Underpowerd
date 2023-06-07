# Energy Resource

The player has a battery which represents his health and currency.

## Battery saving

When the players battery reaches a low amount he will enter battery saving mode. battery saving mode locks all abilities and decreases battery drain.

```C#

public class EnergyResource : MonoBehaviour
{
    public float capacity;

    private float _value;

    public event Action OnValueChangedEvent;

    public void ApplyValue(float value) {
        _value += value;
        OnValueChangedEvent?.Invoke();
    }
}

public class IdleConsumption : MonoBehaviour
{
    public float standardConsumption;
    public bool isConsuming = true;

    // Components
    private EnergyResource _recource;

    private void Awake() {
        _resource = GetComponent<EnergyResource>();
    }

    private void Update() {
        if(isConsuming) {
            var amount = standardConsumption * Time.deltaTime;
            _resource.ApplyValue(amount);
        }
    }
}

```