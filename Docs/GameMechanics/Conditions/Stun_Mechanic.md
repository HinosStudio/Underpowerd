# Stun Mechanic

- condition needs a threshold, a resistance, a cooldown, and an immune state
- condition has a value
- condition has a satisfied state

- The value of a condition will increase when the object is hit by a certain element.
- The value of a condition will decrease over time.
- When the value of a condition reaches the threshold, and the object is not immune, the condition will turn active.
- When the value of a condition reaches 0, and it is in the active state, the condition will deactivate.

```C#

public interface IStunnable {
    float StunThreshold { get; }
    float StunResistance { get; }
    float StunRecovery { get; }

    bool CanStun();
    void OnStunStart();
    void OnStunEnd();
}

public class Stunnable : MonoBehaviour, IHitCallback {
    private IStunnable _stunnable;
    private float _value;
    private bool _stunned;

    public void Awake(){
        _stunnable = GetComponent<IStunnable>();
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

public class Character : MonoBehaviour, IStunnable {
    float StunThreshold { get; }
    float StunResistance { get; }
    float StunRecovery { get; }

    public bool CanStun(){

    }

    public void OnStunStart(){
        // lock actions
    }

    public void OnStunEnd(){
        // unlock actions
    }
}

```