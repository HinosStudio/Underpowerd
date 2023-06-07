using UnityEngine;

namespace hinos
{
    public class ShootingBehaviour : MonoBehaviour
    {
        public Transform weaponOrigin;
        public float timeBetweenShots;

        private float _timeSinceLastShot;
        private float _fireHoldTime;

        // Input
        private bool _fireHeld;

        private void Update() {
            _timeSinceLastShot += timeBetweenShots.deltaTime;

            if(_fireHeld) {
                _fireHoldTime += Time.deltaTime;

                if(_timeSinceLastShot >= timeBetweenShots) {
                    Fire();
                }
            }
        }

        private void Fire() {
            _timeSinceLastShot = 0;

            //TODO: Tracing logic
        }
    }
}