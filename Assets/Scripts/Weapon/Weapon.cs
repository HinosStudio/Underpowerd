using Assets.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour {
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private float fireRate = 5.0f;
    [SerializeField] private float cost = 1.0f;

    private float nextShotTime = 0.0f;

    private Battery _battery;
    private Transform _transform;

    protected Vector3 Position => _transform.position;
    protected Vector3 Forward => _transform.forward;

    private void Awake() {
        _battery = GetComponent<Battery>();
        _transform = GetComponent<Transform>();
    }

    protected abstract HitDetector[] QueryTargets();

    public void Fire() {
        if(Time.time >= nextShotTime) {
            var targets = QueryTargets();
            foreach(HitDetector target in targets) {
                target.Hit(this.gameObject, damage, ElementType.NONE);
            }

            _battery.RemoveCharge(cost);
            if (_battery.Depleted) {
                Debug.Log($"[Weapon] {this.name}, has been detroyed");
            }

            nextShotTime = Time.time + 1 / fireRate;
        }
    }
}
