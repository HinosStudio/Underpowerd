using UnityEngine;

namespace Assets.Scripts {
    [System.Serializable]
    public struct HitData {
        public GameObject source;
        public float damage;
        public ElementType element;

        public HitData(GameObject source, float damage, ElementType element) {
            this.source = source;
            this.damage = damage;
            this.element = element;
        }
    }
}