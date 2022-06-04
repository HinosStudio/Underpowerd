# Weapon mechanic

- a weapon can fire

## Rifle

- On fire, will shoot a hit scan into the looking direction of the player.

```C#

class weapon {
    public float damage;
    public float range;

    public Transform weaponOrigin;

    public void OnFire() {}

    public void Shoot() {}
}

class rifle {
    private float delay;
    private float damage;
    private float cost;


    public void Shoot();
}

```