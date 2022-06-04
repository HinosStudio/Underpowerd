using System;
using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField] private float batteryCapacity;
    [SerializeField] private float powerConsumption;
    [SerializeField] private float mobility;
    [SerializeField] private float resistance;

    public Vector2 MoveDirection;
    public Vector2 LookDirection;

    // flags
    private bool dead;
    // private bool invincible;

    public event Action OnDeathEvent;

    public float PowerConsumption => powerConsumption;

    public void Initialize() {

    }

    public void Die() {
        OnDeathEvent?.Invoke();
        dead = true;
    }
}
