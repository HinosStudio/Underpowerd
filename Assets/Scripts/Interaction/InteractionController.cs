using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour {
    private readonly List<IInteractable> m_Interactables = new List<IInteractable>();

    public void Register(IInteractable interactable) {
        if (!m_Interactables.Contains(interactable))
            m_Interactables.Add(interactable);
    }

    public void Unregister(IInteractable interactable) {
        if (m_Interactables.Contains(interactable))
            m_Interactables.Remove(interactable);
    }

    public void StartInteraction() {
        if (m_Interactables.Count == 0) return;

        IInteractable target = m_Interactables[0];

        if (m_Interactables.Count > 1) {
            // var shortestDistance = float.MaxValue;

            for (int i = 1; i < m_Interactables.Count; ++i) {
                //distance check
            }
        }

        target.Interact(this.gameObject);
    }
}
