using UnityEngine;

public interface IAreaCallback {
    void OnAreaEnter(GameObject obj);
    void OnAreaExit(GameObject obj);
}

public class Area : MonoBehaviour {
    [SerializeField][RequireInterface(typeof(IAreaCallback))]
    private Object callBackObj;

    public IAreaCallback CallBackObject {
        get => callBackObj as IAreaCallback;
    }

    public void OnTriggerEnter(Collider other) {
        CallBackObject?.OnAreaEnter(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        CallBackObject?.OnAreaExit(other.gameObject);
    }

    public void SetCallbackObject(Object obj) {
        callBackObj = obj;
    }
}
