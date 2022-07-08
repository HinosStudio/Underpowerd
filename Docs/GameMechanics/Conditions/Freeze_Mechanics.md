# Freeze Mechanic

- condition needs a threshold, a resistance, a cooldown, and an immune state
- condition has a value
- condition has a satisfied state

- The value of a condition will increase when the object is hit by a certain element.
- The value of a condition will decrease over time.
- When the value of a condition reaches the threshold, and the object is not immune, the condition will turn active.
- When the value of a condition reaches 0, and it is in the active state, the condition will deactivate.

```C#

public interface IFreezable {
    float FreezeThreshold { get; }
    float FreezeResistance { get; }
    float FreezeRecovery { get; }

    bool CanFreeze();
    void OnFreezeStart();
    void OnFreezeEnd();
}

public class Freezable : MonoBehaviour, IHitCallback {
    private IFreezable _freezable;
    private float _value;
    private bool _freezing;

    public void Awake(){
        _freezable = GetComponent<IFreezable>();
    }

    public void Update(){
        // Reduce value using objects recovery
    }

    public void OnHit(HitData data){
        // Early exit when already burning
        // Add damage, reduced by object burn resistance, to value
        // if staisfied start burning
    }
}

public class Character : MonoBehaviour, IFreezable {
    float FreezeThreshold { get; }
    float FreezeResistance { get; }
    float FreezeRecovery { get; }

    public bool CanFreeze(){

    }

    public void OnFreezeStart(){
        // Slow movement
    }

    public void OnFreezeEnd(){
        // Unslow movement
    }
}

```