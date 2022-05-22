using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(InteractionController))]
public partial class PlayerController : MonoBehaviour {
    [SerializeField] private CharacterController characterController;

    [Header("Lives")]
    [SerializeField] private int lives = 0;
    [SerializeField] private TMP_Text livesText;

    private Vector2 m_InputDirection = Vector2.zero;
    
    private InteractionController m_InteractionController;

    private void OnEnable() {
        characterController.OnDeathEvent += Handle_Player_Death;
    }

    private void OnDisable() {
        characterController.OnDeathEvent -= Handle_Player_Death;
    }

    private void Awake() {
        m_InteractionController = GetComponent<InteractionController>();
    }

    private void Start() {
        livesText.text = $"Lives: {lives}";
    }

    void Update() {
        Handle_Input();

        if (Input.GetKeyDown(KeyCode.Z)) 
            m_InteractionController.StartInteraction();
    }

    private void FixedUpdate() {
        characterController.Move(m_InputDirection);
    }

    private void Handle_Input() {
        //Get input
        m_InputDirection.x = Input.GetAxis("Horizontal");
        m_InputDirection.y = Input.GetAxis("Vertical");
        m_InputDirection = Vector2.ClampMagnitude(m_InputDirection, 1.0f);
    }

    private void Handle_Player_Death() {
        --lives;
        if (lives > 0)
            Debug.Log("Respawn character");

    }
}
