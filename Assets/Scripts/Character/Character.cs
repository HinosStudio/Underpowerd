using System;
using UnityEngine;

public class Character : MonoBehaviour {
    public Vector2 MoveDirection;
    public Vector2 LookDirection;

    // flags
    private bool dead;
    // private bool invincible;

    public event Action OnDeathEvent;

    public void Initialize() {

    }

    public void Die() {
        OnDeathEvent?.Invoke();
        dead = true;
    }
}
