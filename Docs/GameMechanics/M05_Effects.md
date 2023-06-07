# Effect Mechanics

```C#

public interface IAfflictable {
    void ApplyEffect(IEffect effect);
    void RefrainEffect(IEffect effect);
    bool IsAfflicted(IEffect effect);
}

public class AfflictionCollection {
    public readonly List<IAffliction> afflictions = new ();

    public AfflictionCollection() {

    }

    public void AddAffliction(IAffliction effect){
        afflictions.Add(effect);
    }

    public void RemoveAffliction(IAffliction effect){
        afflictions.Remove(effect);
    }

    public void UpdateAfflictions(float deltaTime){
        foreach(IAffliction effect in afflictions){
            effect.OnTick(deltaTime);
        }
    }
}

public interface IAffliction {
    void OnApply();
    void OnRefrain();
    void OnTick(float deltaTime);
}

public abstract class DurableEffect : IAffliction {
    protected readonly float duration;

    protected float lifetime;

    public DurableEffect(float duration){
        this.duration = duration;
    }

    public void OnApply() {

    }

    public void OnRefrain() {

    }

    public void OnTick() {

    }
}

public abstract class InstantEffect : IAffliction {

    public void OnApply() {

    }

    public void OnRefrain() {

    }

    public void OnTick() {

    }
}

```