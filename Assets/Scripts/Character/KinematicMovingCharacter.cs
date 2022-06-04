using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class KinematicMovingCharacter : MonoBehaviour {

    private Vector3 m_Velocity = Vector3.zero;

    private CharacterController m_CharacterController;

    private void Awake() {
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        GroundCheck();

        ApplyGravity();
    }

    private void GroundCheck() {

    }

    private void ApplyGravity() {
        m_Velocity += Physics.gravity * Time.deltaTime;
    }
}
