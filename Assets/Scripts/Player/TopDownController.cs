using UnityEngine;
using hinos.util;

namespace hinos.topdown
{
    public class TopDownController : MonoBehaviour
    {
        public float maxSpeed;
        public float maxAcceleration;
        public Vector3 surfaceNormal;

        // Physics State
        private Vector3 _velocity;
        private Vector3 _heading;
        private Vector3 _crossHeading;

        // Input
        private Vector3 _moveDirection;

        // Components
        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake() {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {

            // Update physics state
            _velocity = _rigidbody.velocity;
            _crossHeading = Vector3.Cross(_heading, Vector3.up);

            // Process Input
            float speed, acceleration;
            var moveAmount = _moveDirection.magnitude;
            if(moveAmount > 0.01f) {
                _heading = _moveDirection.normalized;
                speed = maxSpeed * moveAmount;
                acceleration = maxAcceleration;
            }
            else {
                _heading = _velocity.normalized;
                speed = 0;
                acceleration = maxAcceleration;
            }

            // Apply movement forces
            var moveVelocity = CalculateVelocityChange(_velocity, _heading, speed, acceleration);
            var latVelocityCorrection = CalculateVelocityChange(_velocity, _crossHeading, 0, acceleration);
            _rigidbody.velocity += moveVelocity + latVelocityCorrection;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, _heading);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _crossHeading);
        }

        private Vector3 CalculateVelocityChange(Vector3 velocity, Vector3 direction, float targetSpeed, float speedChange) {
            var alignedSpeed = Vector3.Dot(velocity, direction);
            var finalSpeed = Mathf.MoveTowards(alignedSpeed, targetSpeed, speedChange * Time.deltaTime);
            return direction * (finalSpeed - alignedSpeed);
        }

        public void MoveTowards(Vector3 direction) {
            _moveDirection = direction;

        }
    }
}
