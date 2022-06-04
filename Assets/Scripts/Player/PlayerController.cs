using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.AI;

[RequireComponent(typeof(Character), typeof(Weapon))]
[RequireComponent(typeof(InteractionController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(NavMeshAgent))]
public partial class PlayerController : MonoBehaviour {
    [SerializeField] private ProgressBar charge;

    [Header("Lives")]
    [SerializeField] private int lives = 0;

    private Vector2 m_LookValue;

    private PlayerInput m_PlayerInput;
    private Character m_Character;
    private Weapon m_Weapon;
    private InteractionController m_InteractionController;
    private NavMeshAgent m_NavMeshAgent;

    private void Awake() {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_Character = GetComponent<Character>();
        m_Weapon = GetComponent<Weapon>();
        m_InteractionController = GetComponent<InteractionController>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();

        m_NavMeshAgent.updateRotation = false;
        m_NavMeshAgent.updatePosition = false;
    }

    private void OnGUI() {
        GUI.color = Color.black;
        GUI.Label(new Rect(Screen.width * 0.5f + m_LookValue.x - 25, Screen.height * 0.5f - m_LookValue.y - 25, 50, 50), $"{m_LookValue}");
    }

    public void OnMove(InputAction.CallbackContext value) {
        m_Character.MoveDirection = value.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext value) {
        m_LookValue = value.ReadValue<Vector2>();
        m_LookValue.x -= Screen.width * 0.5f;
        m_LookValue.y -= Screen.height * 0.5f;
        m_Character.LookDirection = m_LookValue;
    }

    public void OnInteract(InputAction.CallbackContext value) {
        if(value.started)
            m_InteractionController.StartInteraction();
    }

    public void OnFire(InputAction.CallbackContext value) {
        if (value.started)
            m_Weapon.Fire();
    }
}
