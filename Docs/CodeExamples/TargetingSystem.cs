using UnityEngine;

namespace hinos.topdown
{
    public class TargetingManager : MonoBehaviour
    {
        public Target[] targets;

        private Transform _transform;

        private void Awake() {
            _transform = GetComponent<_transform>();
        }

        private void Update() {
            // map the input angle of x to the output angle of y
            // f(id): x -> y
            var inputAngle = Mathf.Atan2(inputAngle.y, inputAngle.x);
        }

        public bool IsTargetValid(Target target) {
            return !Physics.Linecast(_transform.position, target.position);
        }

        public float AngleToTarget(Target target) {
            var localPosition = _transform.inverseTransform(target.position);
            return Mathf.Atan2(localPosition.z, localPosition.x) * Mathf.Rad2Deg;
        }

        public float AngularDiameterOfTarget(Target target) {
            return 2 * Mathf.Atan2(2 * target.size, 2 * Vector3.Distance(_transform.position, target.position)) * Mathf.Rad2Deg;
        }
    }

    public class Target : MonoBehaviour
    {
        public Vector3 position;
        public float size;
    }
}