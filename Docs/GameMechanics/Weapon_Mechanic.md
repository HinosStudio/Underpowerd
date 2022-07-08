# Weapon mechanic

- a weapon can fire

## Rifle

- On fire, will shoot a hit scan into the looking direction of the player.

## Flamethrower

- On fire, will check a cone-shaped area for targets 

```C#

public enum ElementType {
    NONE, HEAT, COLD
}

[RequireComponent(typeof(Battery))]
public abstract class weapon : Monobehaviour {
    private float damage;
    private ElementType element;
    private float delay;
    private float nextShot;
    private float cost;

    private Battery _battery; 
    
    public void Fire(){
        if(!Time.Now() >= nextShot) return;

        QueryTargets();

        foreach(Target target in targets){
            target.hit(this.gameObject, damage);
        }

        _battery.RemoveCharge(cost);
        if(_battery.Depleted){
            // Destroy object
        }
    }

    public abstract void QueryTargets();
}

public class HitScanWeapon : Weapon {
    private float range;

    public override void QueryTargets() {

    }
}

public class FlameThrower : Weapon {
    private float range;
    private float radius;

    public override void QueryTargets() {

    }
}

```