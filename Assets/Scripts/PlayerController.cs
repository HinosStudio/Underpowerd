using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Battery))]
public partial class PlayerController : MonoBehaviour {

    //Moving
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float friction = 15.0f;
    [SerializeField] private float maxSpeed = 5.0f;
    private Vector2 m_InputDirection = Vector2.zero;
    private Vector3 m_TargetDirection = new Vector3();
    private Rigidbody m_Rigidbody;

    private Battery m_Battery;

    private void Awake() {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Battery = GetComponent<Battery>();
    }

    void Update() {
        Handle_Input();

        if (Input.GetKeyDown(KeyCode.Z)) {
            StartInteraction();
        }
    }

    private void FixedUpdate() {
        m_TargetDirection.Set(m_InputDirection.x, 0.0f, m_InputDirection.y);

        var moving = m_InputDirection != Vector2.zero;
        var targetVelocity = moving ? m_TargetDirection * maxSpeed : Vector3.zero;
        var targetAcceleration = moving ? acceleration : friction;

        m_Rigidbody.velocity = Vector3.MoveTowards(m_Rigidbody.velocity, targetVelocity, targetAcceleration * Time.deltaTime);
    }

    private void OnGUI() {
        GUI.Box(new Rect(10, 10, 100, 100), "Player");
        GUI.Label(new Rect(10, 40, 100, 20), $"{m_Battery.Charge:n2}%");
    }

    private void Handle_Input() {
        //Get input
        m_InputDirection.x = Input.GetAxis("Horizontal");
        m_InputDirection.y = Input.GetAxis("Vertical");
        m_InputDirection = Vector2.ClampMagnitude(m_InputDirection, 1.0f);
    }

#region interaction

    private readonly List<IInteractable> m_Interactables = new List<IInteractable>();

    public void Register(IInteractable interactable) {
        if (!m_Interactables.Contains(interactable)) 
            m_Interactables.Add(interactable);
    }

    public void Unregister(IInteractable interactable) {
        if (m_Interactables.Contains(interactable))
            m_Interactables.Remove(interactable);
    }

    private void StartInteraction() {
        if (m_Interactables.Count == 0) return;

        IInteractable target = m_Interactables[0];

        if (m_Interactables.Count > 1) {
            // var shortestDistance = float.MaxValue;

            for (int i = 1; i < m_Interactables.Count; ++i) {
                //distance check
            }
        }

        target.Interact(this.gameObject);
    }

    #endregion

}
