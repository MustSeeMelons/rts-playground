using UnityEngine;

public class GameManager : MonoBehaviour {

    void Start() {
        EventManager.IgnoreEvent(CEvents.MOUSE_POSITION);
        EventManager.IgnoreEvent(CEvents.CAMERA_MOVE);
    }

    void Update() {

    }
}
