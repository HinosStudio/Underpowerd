using UnityEngine;
using UnityEngine.Events;

public interface IAreaCallback {
    void OnAreaEnter(GameObject obj);
    void OnAreaExit(GameObject obj);
}

public class Area : MonoBehaviour {
    [System.Serializable]
    public class AreaUnityEvent : UnityEvent<GameObject> {}

    public AreaUnityEvent areaEnter = new AreaUnityEvent();
    public AreaUnityEvent areaExit = new AreaUnityEvent();

    public void OnTriggerEnter(Collider other) {
        areaEnter.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        areaExit.Invoke(other.gameObject);
    }
}
