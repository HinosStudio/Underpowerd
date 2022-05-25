using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingCharacter : MonoBehaviour {
    [SerializeField] private float maxSpeed = 5.0f;
    [SerializeField] private float maxAcceleration = 10.0f;
    [SerializeField] private float maxGroundAngle = 25.0f;

    [Header("snapping")]
    [SerializeField] private float maxSnapSpeed = 100.0f;
    [SerializeField] private float probeDistance = 1.0f;
    [SerializeField] private LayerMask probeMask = -1;

    [Header("stairs")]
    [SerializeField] private float maxStairAngle = 50.0f;
    [SerializeField] private LayerMask stairMask = -1;

    private Vector2 m_InputDirection = Vector2.zero;
    private Vector3 m_Velocity = Vector3.zero;
    private Vector3 m_DesiredVelocity = Vector3.zero;

    private float m_MinGroundDotProduct = 0.0f, m_MinStairDotProduct = 0.0f;
    private int m_StepsSinceLastGrounded = 0;
    private int m_GroundContactCount = 0, m_SteepContactCount = 0;
    private Vector3 m_ContactNormal = Vector3.zero, m_SteepNormal = Vector3.zero;

    private Rigidbody m_Rigidbody;

    public bool OnGround => m_GroundContactCount > 0;
    public bool OnSteep => m_SteepContactCount > 0;

    private void Awake() {
        m_Rigidbody = GetComponent<Rigidbody>();

        m_MinGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        m_MinStairDotProduct = Mathf.Cos(maxStairAngle * Mathf.Deg2Rad);
    }

    private void Update() {
        // Get input
        m_InputDirection.x = Input.GetAxis("Horizontal");
        m_InputDirection.y = Input.GetAxis("Vertical");
        m_InputDirection = Vector2.ClampMagnitude(m_InputDirection, 1.0f);

        //Set desired velocity
        m_DesiredVelocity.x = m_InputDirection.x * maxSpeed;
        m_DesiredVelocity.z = m_InputDirection.y * maxSpeed;
    }

    private void FixedUpdate() {
        UpdateState();
        AdjustVelocity();
        
        m_Rigidbody.velocity = m_Velocity;

        ClearState();
    }

    private void OnCollisionStay(Collision collision) {
        EvaluateCollision(collision);
    }

    private void UpdateState() {
        m_StepsSinceLastGrounded += 1;
        m_Velocity = m_Rigidbody.velocity;

        if (OnGround || SnapToGround() || CheckSteepContacts()) {
            m_StepsSinceLastGrounded = 0;
            if (m_GroundContactCount > 1) {
                m_ContactNormal.Normalize();
            }
        }
        else {
            m_ContactNormal = Vector3.up;
        }
    }

    private void ClearState() {
        m_GroundContactCount = m_SteepContactCount = 0;
        m_ContactNormal = m_SteepNormal = Vector3.zero;
    }

    private void AdjustVelocity() {
        var xAxis = ProjectOnContactPlane(Vector3.right).normalized;
        var zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

        float currentX = Vector3.Dot(m_Velocity, xAxis);
        float currentZ = Vector3.Dot(m_Velocity, zAxis);

        var maxSpeedChange = maxAcceleration * Time.deltaTime;

        var newX = Mathf.MoveTowards(currentX, m_DesiredVelocity.x, maxSpeedChange);
        var newZ = Mathf.MoveTowards(currentZ, m_DesiredVelocity.z, maxSpeedChange);

        m_Velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
    }

    private bool SnapToGround() {
        if(m_StepsSinceLastGrounded > 1) {
            return false;
        }

        var speed = m_Velocity.magnitude;
        if(speed > maxSnapSpeed) {
            return false;
        }

        if (!Physics.Raycast(m_Rigidbody.position, Vector3.down, out RaycastHit hit, probeDistance, probeMask)) {
            return false;
        }

        if(hit.normal.y < GetMinDot(hit.collider.gameObject.layer)) {
            return false;
        }

        // Update ground values
        m_GroundContactCount = 1;
        m_ContactNormal = hit.normal;

        // Correct position to be on ground
        var dot = Vector3.Dot(m_Velocity, hit.normal);
        if (dot > 0.0f) {
            m_Velocity = (m_Velocity - hit.normal * dot).normalized * speed;
        }

        return true;
    }

    private void EvaluateCollision(Collision collision) {
        var minDot = GetMinDot(collision.gameObject.layer);
        for (int i = 0; i < collision.contactCount; ++i) {
            var normal = collision.GetContact(i).normal;
            if (normal.y >= minDot) {
                m_GroundContactCount += 1;
                m_ContactNormal += normal;
            }
            else if(normal.y > -0.01f) {
                m_SteepContactCount += 1;
                m_SteepNormal += normal;
            }
        }
    }

    private bool CheckSteepContacts() {
        if(m_SteepContactCount > 1) {
            m_SteepNormal.Normalize();
            if(m_SteepNormal.y > m_MinGroundDotProduct) {
                m_GroundContactCount = 1;
                m_ContactNormal = m_SteepNormal;
                return true;
            }
        }
        return false;
    }

    private Vector3 ProjectOnContactPlane(Vector3 vector) {
        return vector - m_ContactNormal * Vector3.Dot(vector, m_ContactNormal);
    }

    private float GetMinDot(int layer) {
        return (stairMask & (1 << layer)) == 0 ? m_MinGroundDotProduct : m_MinStairDotProduct;
    }
}
