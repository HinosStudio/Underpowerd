# Burn Mechanics

- condition needs a threshold, a resistance, a cooldown, and an immune state
- condition has a value
- condition has a satisfied state

- The value of a condition will increase when the object is hit by a certain element.
- The value of a condition will decrease over time.
- When the value of a condition reaches the threshold, and the object is not immune, the condition will turn active.
- When the value of a condition reaches 0, and it is in the active state, the condition will deactivate.

```C#

public interface IBurnable {
    float BurnThreshold { get; }
    float BurnResistance { get; }
    float BurnRecovery { get; }

    bool CanBurn();
    void BurnTick();
}

public class Burnable : MonoBehaviour, IHitCallback {
    private IBurnable _burnable;
    private float _value;
    private bool _burning;

    public void Awake(){
        _burnable = GetComponent<Burnable>();
    }

    public void Update(){
        // Reduce value using objects recovery
    }

    public override void OnHit(HitData data){
        // Early exit when already burning
        // Add damage, reduced by object burn resistance, to value
        // if staisfied start burning
    }
}

public class Player : MonoBehaviour, IBurnable {
    float BurnThreshold { get; }
    float BurnResistance { get; }
    float BurnRecovery { get; }

    public override bool CanBurn(){

    }

    public override void BurnTick(){
        // Reduce battery charge
        // If battery depleted, kill with burn message
    }
}

```