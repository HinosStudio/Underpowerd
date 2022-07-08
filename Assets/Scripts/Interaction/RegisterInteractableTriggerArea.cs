using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interaction {
    public class RegisterInteractableTriggerArea : MonoBehaviour {
        public IInteractable interactable;

        public void OnTriggerEnter(Collider other) {
            other.GetComponent<InteractionController>()?.Register(interactable);
        }

        private void OnTriggerExit(Collider other) {
            other.GetComponent<InteractionController>()?.Unregister(interactable);
        }
    }
}