# Target Mechanics

- when a target is hit, it depletes the charge on a battery
- when the charge on the battery is depleted, it will destroy the object

```C#

public interface IDestroyable {
    void Destroy(string message);
}

[RequireComponent(typeof(Battery))]
public class Target : Monobehaviour {
    private Battery _battery;
    private IDestroyable _destroyable;

    private void Awake(){
        _battery = GetComponent<Battery>();
        _destroyable = GetComponent<IDestroyable>();
    }

    public void Hit(GameObject src, float damage) {
        _battery.RemoveCharge(damage);
        if(_battery.IsDepleted) {
            _destroyable.Destroy($"killed by {src.name});
        }
    }
}

```