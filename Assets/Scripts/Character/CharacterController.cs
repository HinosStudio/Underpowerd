using System;
using UnityEngine;

[RequireComponent(typeof(Battery))]
[RequireComponent(typeof(InteractionController))]
[RequireComponent(typeof(Connector))]
public class CharacterController : MonoBehaviour {
    [SerializeField] private float consumption;

    [SerializeField] private ProgressBar chargeProgressBar;

    // flags
    private bool dead;
    // private bool invincible;

    private Transform m_Transform;
    private Battery m_Battery;
    private InteractionController m_InteractionController;
    private Connector m_Connector;

    public event Action OnDeathEvent;

    private void OnEnable() {
        m_Battery.OnBatteryDepleted.AddListener(Die);
    }

    private void OnDisable() {
        m_Battery.OnBatteryDepleted.RemoveListener(Die);
    }

    private void Awake() {
        m_Transform = GetComponent<Transform>();
        m_Battery = GetComponent<Battery>();
        m_InteractionController = GetComponent<InteractionController>();
        m_Connector = GetComponent<Connector>();
    }

    private void Update() {
        m_Battery.RemoveCharge(consumption * Time.deltaTime);
    }

    private void LateUpdate() {
        chargeProgressBar.Value = m_Battery.Charge;
    }

    public void Respawn() {
        
    }

    public void Die() {
        OnDeathEvent?.Invoke();
        dead = true;
    }

    public void Move(Vector2 input) {

    }

    public void StartInteraction() {
        m_InteractionController.StartInteraction();
    }
}
