# Energy store mechanics

- The player has 2 pools of energy representing his health and money.
- When both pools of energy are depleted the player will die.
- When the player takes damage, he will lose a relative amount of energy in his first pool.
- When the player takes damage and the first pool is empty the player will lose a relative amount in his second pool.
- When the player spends money, he will lose a relative amount of energy in his second pool.

```C#

public class EnergyPool {
    private float capacity;
    private float value;

    public bool isDepleted = false;

    public float ApplyValue(float value) { ... }
}

public class PlayerHealth {
    private EnergyPool firstPool, secondPool;

    private bool isDead = false;

    public void ApplyDamage(float amount) { ... }

    public void ApplyCost(float amount) { ... }
}

public class EnergyStore {
    public float capacity;
    public float value;
    public float transferRate;

    public event Action OnStoreAugmentedEvent;
    public event Action OnStoreDepletedEvent;

    public bool IsAugmented => value >= capacity;
    public bool IsDepleted => value <= 0;

    public EnergyStore(float capacity, float initValue = 0){
        this.capacity = capacity;
        this.value = initValue;
    }

    public float ApplyValue(float inputValue){
        float delta = this.value + inputValue;

        // delta value exceeds upper-bound, clamp on capacity
        if(delta > capacity){
            this.value = capacity;
            OnStoreAugmentedEvent?.Invoke();
            return delta - capacity;
        }

        // delta value exceeds lower-bound, clamp on ZERO
        if(delta < 0){
            this.value = 0;
            OnStoreDepletedEvent?.Invoke();
            return delta;
        }

        this.value = delta;
        return 0;
    }

    // The source will supply the target with energy at a transfer rate defined by the source.
    public static void TransferValue(EnergyStore source, EnergyStore target){

    }

    public static void NormalizeValue(params EnergyStore[] source){

    }
}

```