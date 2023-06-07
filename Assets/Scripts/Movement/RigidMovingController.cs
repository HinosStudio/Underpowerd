using UnityEngine;

namespace Assets.Scripts.Movement {
    [RequireComponent(typeof(Rigidbody), typeof(SurfaceDetector))]
    public class RigidMovingController : MonoBehaviour {
        private float _acceleration = 0;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _desiredVelocity = Vector3.zero;
        private Vector3 _desiredRotation = Vector3.forward;

        private Rigidbody _rigidbody;
        private SurfaceDetector _surfaceDetector;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _surfaceDetector = GetComponent<SurfaceDetector>();
        }

        private void FixedUpdate() {
            _velocity = _rigidbody.velocity;

            AdjustVelocity();

            _rigidbody.velocity = _velocity;
            _rigidbody.MoveRotation(Quaternion.LookRotation(_desiredRotation));
        }

        private void AdjustVelocity() {
            var normal = _surfaceDetector.ContactNormal;
            var xAxis = ProjectOnContactPlane(normal, Vector3.right).normalized;
            var zAxis = ProjectOnContactPlane(normal, Vector3.forward).normalized;

            float currentX = Vector3.Dot(_velocity, xAxis);
            float currentZ = Vector3.Dot(_velocity, zAxis);

            var maxSpeedChange = _acceleration * Time.deltaTime;

            var newX = Mathf.MoveTowards(currentX, _desiredVelocity.x, maxSpeedChange);
            var newZ = Mathf.MoveTowards(currentZ, _desiredVelocity.z, maxSpeedChange);

            _velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
        }

        private Vector3 ProjectOnContactPlane(Vector3 normal, Vector3 vector) {
            return vector - normal * Vector3.Dot(vector, normal);
        }

        public void Move(Vector3 direction, float speed, float acceleration) {
            _desiredVelocity = direction * speed;
            _acceleration = acceleration;
        }
    }
}