using UnityEngine;

namespace Assets.Scripts.Movement {
    public class SurfaceDetector : MonoBehaviour {
        [SerializeField] private float maxGroundAngle = 25.0f;
        [SerializeField] private float maxStairAngle = 50.0f;
        [SerializeField] private LayerMask stairMask = -1;

        private float m_MinGroundDotProduct = 0.0f, m_MinStairDotProduct = 0.0f;
        private int m_StepsSinceLastGrounded = 0;
        private int m_GroundContactCount = 0, m_SteepContactCount = 0;
        private Vector3 m_ContactNormal = Vector3.zero, m_SteepNormal = Vector3.zero;

        public Vector3 ContactNormal => m_ContactNormal;
        public int StepsSinceLastGrounded => m_StepsSinceLastGrounded;
        public bool OnGround => m_GroundContactCount > 0;
        public bool OnSteep => m_SteepContactCount > 0;

        private void Awake() {
            m_MinGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
            m_MinStairDotProduct = Mathf.Cos(maxStairAngle * Mathf.Deg2Rad);
        }

        private void FixedUpdate() {
            m_StepsSinceLastGrounded += 1;

            if (OnGround || CheckSteepContacts()) {
                m_StepsSinceLastGrounded = 0;
                if (m_GroundContactCount > 1) {
                    m_ContactNormal.Normalize();
                }
            }
            else {
                m_ContactNormal = Vector3.up;
            }

        }

        private void OnCollisionStay(Collision collision) {
            EvaluateCollision(collision);
        }

        private void ClearState() {
            m_GroundContactCount = m_SteepContactCount = 0;
            m_ContactNormal = m_SteepNormal = Vector3.zero;
        }

        private void EvaluateCollision(Collision collision) {
            var minDot = GetMinDot(collision.gameObject.layer);
            for (int i = 0; i < collision.contactCount; ++i) {
                var normal = collision.GetContact(i).normal;
                if (normal.y >= minDot) {
                    m_GroundContactCount += 1;
                    m_ContactNormal += normal;
                }
                else if (normal.y > -0.01f) {
                    m_SteepContactCount += 1;
                    m_SteepNormal += normal;
                }
            }
        }

        private bool CheckSteepContacts() {
            if (m_SteepContactCount > 1) {
                m_SteepNormal.Normalize();
                if (m_SteepNormal.y > m_MinGroundDotProduct) {
                    m_GroundContactCount = 1;
                    m_ContactNormal = m_SteepNormal;
                    return true;
                }
            }
            return false;
        }

        public void SetSurface(Vector3 normal) {
            m_GroundContactCount = 1;
            m_ContactNormal = normal;
        }

        public float GetMinDot(int layer) {
            return (stairMask & (1 << layer)) == 0 ? m_MinGroundDotProduct : m_MinStairDotProduct;
        }
    }
}
