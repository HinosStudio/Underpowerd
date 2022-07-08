using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Conditions.Burning {
    public class FireWell : MonoBehaviour {
        [SerializeField] private float damage = 10;
        private Collider _collider;

        private void Awake() {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerStay(Collider other) {
            other.GetComponent<Burnable>()?.OnHit(new HitData(this.gameObject, damage * Time.deltaTime, ElementType.HEAT));
        }

        private void OnDrawGizmos() {
            if (_collider) {
                Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
                Gizmos.DrawCube(_collider.bounds.center, _collider.bounds.size);
            }
        }
    }
}