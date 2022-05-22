using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour {
    [SerializeField] private GameEvent interactionEvent;

    public void StartInteraction() {
        /*
        if (m_Interactables.Count == 0) return;

        IInteractable target = m_Interactables[0];

        if (m_Interactables.Count > 1) {
            // var shortestDistance = float.MaxValue;

            for (int i = 1; i < m_Interactables.Count; ++i) {
                //distance check
            }
        }

        target.Interact(this.gameObject);
        */

        interactionEvent.Raise(this.gameObject);
    }
}
