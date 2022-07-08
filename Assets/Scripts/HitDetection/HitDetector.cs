using System.Collections;
using UnityEngine;

namespace Assets.Scripts {

    public class HitDetector : MonoBehaviour {
        public HitUnityEvent hitEvent = new HitUnityEvent();

        public void Hit(GameObject source, float value, ElementType type) {
            hitEvent.Invoke(new HitData(source, value, type));
        }
    }
}