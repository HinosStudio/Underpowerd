using System.Collections.Generic;
using UnityEngine;

namespace hinos.targeting
{
    public class Target : MonoBehaviour 
    {
        public float size;

        private void OnEnable() {
            TargetingBehaviour.allTargets.Add(this);
        }

        private void OnDisable() {
            TargetingBehaviour.allTargets.Remove(this);
        }

    }

    public class TargetingBehaviour : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {
            var targetAngle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg;
            _rigidbody.rotation = Quaternion.AngleAxis(targetAngle, Vector3.up);
        }

        public void LookTowards(Vector3 direction) {
            _targetDirection = direction;
        }
    }
}