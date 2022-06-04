using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour {
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float fireRate = 5.0f;

    private float nextShotTime = 0.0f;

    protected Transform m_Transform;

    public float Damage => damage;
    public float Range => range;

    private void Awake() {
        m_Transform = GetComponent<Transform>();
    }

    protected abstract void Shoot();

    public void Fire() {
        if(Time.time >= nextShotTime) {
            Shoot();
            nextShotTime = Time.time + 1 / fireRate;
        }
    }
}
