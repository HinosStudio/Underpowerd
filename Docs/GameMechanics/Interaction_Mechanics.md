# Interaction Mechanics

```C#

public interface IInteractionCallback {
    void OnInteract(GameObject source);
}

public class InteractionManager : MonoBehaviour {
    private readonly List<IInteractionCallback> _interactables;

    public void AddInteractionCallback(IInteractionCallback other) {

    }

    public void RemoveInteractionCallback(IInteractionCallback other) {

    }
}

public class RegisterInteractableTriggerArea : MonoBehaviour {
    public IInteractionCallback Interactable { get; set; };

    private void OnTriggerEnter(Collider other) {
        // Register interactable
    }

    private void OnTriggerExit(Collider other) {
        // Unregister interactable
    }
}

```