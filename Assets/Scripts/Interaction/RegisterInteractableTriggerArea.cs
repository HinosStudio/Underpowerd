using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interaction {
    public class RegisterInteractableTriggerArea : MonoBehaviour {
        public IInteractable interactable;

        public void OnTriggerEnter(Collider other) {
            var c_OtherInteract = other.GetComponent<InteractionController>();
            if(c_OtherInteract)
                c_OtherInteract.Register(interactable);
        }

        private void OnTriggerExit(Collider other) {
            var c_OtherInteract = other.GetComponent<InteractionController>();
            if(c_OtherInteract)
                c_OtherInteract.Unregister(interactable);
        }
    }
}