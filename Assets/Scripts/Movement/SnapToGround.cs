using UnityEngine;

namespace Assets.Scripts.Movement {
    [RequireComponent(typeof(SurfaceDetector), typeof(Rigidbody))]
    public class SnapToGround : MonoBehaviour {
        [SerializeField] private float maxSnapSpeed = 100.0f;
        [SerializeField] private float probeDistance = 1.0f;
        [SerializeField] private LayerMask probeMask = -1;

        private Rigidbody _body;
        private SurfaceDetector _surfaceDetector;

        private void Awake() {
            _body = GetComponent<Rigidbody>();
            _surfaceDetector = GetComponent<SurfaceDetector>();
        }

        private void FixedUpdate() {
            var velocity = _body.velocity;

            if (_surfaceDetector.StepsSinceLastGrounded > 1) return;

            var speed = velocity.magnitude;
            if (speed > maxSnapSpeed) return;

            if (!Physics.Raycast(_body.position, Vector3.down, out RaycastHit hit, probeDistance, probeMask)) return;
            if (hit.normal.y < _surfaceDetector.GetMinDot(hit.collider.gameObject.layer)) return;

            // Update ground values
            _surfaceDetector.SetSurface(hit.normal);

            // Correct position to be on ground
            var dot = Vector3.Dot(velocity, hit.normal);
            if (dot > 0.0f) velocity = (velocity - hit.normal * dot).normalized * speed;

            _body.velocity = velocity;
        }
    }
}
