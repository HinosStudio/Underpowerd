# Robotic Entity

```C#

public interface IDamageable {
    void OnHit(float damage);
}

public class RoboticEntity : MonoBehaviour, IDamageable, IAfflictable {
    public float capacitorCapacity;
    public float batteryCapacity;
    public float powerConsumption;

    private EnergyStore capacitor;
    private EnergyStore battery;

    private readonly List<IEffect> afflictions = new();

    public event Action OnCapacitorDepletedEvent;
    public event Action OnBatteryDepletedEvent;

    private void OnUpdate(){

    }

    public void OnHit(float damageValue){
        float delta = damageValue;
        if(!capacitor.IsDepleted)
            delta = capacitor.ApplyValue(-delta);
        if(!battery.IsDepleted && delta  < 0) 
            battery.ApplyValue(delta);
    }

    private void UpdateEffects(){
        foreach(IAffliction a in afflictions) {
            effect.Tick(this);
        }
    }

    public void ApplyEffect(IAffliction effect) {

    }

    public void RefrainEffect(IAffliction effect) {

    }
}



public class StatusAfflictions : MonoBehaviour {
    public readonly List<IEffects> effects = new ();

    private void OnUpdate(){
        foreach(IEffect e in effects){
            e.Tick();
        }
    }
}

public interface IEffect<TSource> : where TSource : IAfflictable{
    void OnApply(TSource source);
    void OnRefrain(TSource source);
    void OnTick(TSource source);
    bool HandleMessage(TSource source, Message message);
}

public abstract class DurableEffect<TSource> : IEffect<TSource> {
    public override void OnTick(TSource source){
        time += Time.deltaTime;
        if(time >= duration) {
            //TODO: Remove effect from source
        }
    }
}

public class BurnEffect : Effect<RoboticEntity> {

    public BurnEffect(float duration) : base(duration){

    }

    public void OnApply(RoboticEntity source){

    }

    public voif OnRefrain(RoboticEntity source){

    }

    public void OnTick(RoboticEntity source){

    }

    public bool HandleMessage(RoboticEntity source, Message message){
        return false;
    }
}

public class Burnable : MonoBehaviour {
    public float 
}

```