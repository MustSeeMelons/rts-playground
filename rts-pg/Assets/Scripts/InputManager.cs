using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameObject townCenter;

    void Update() {
        // Camera movement
        if (Input.GetKey(KeyCode.W)) {
            EventManager.TriggerEvent(CEvents.CAMERA_MOVE, new CameraMessage(Direction.UP));
        }
        if (Input.GetKey(KeyCode.A)) {
            EventManager.TriggerEvent(CEvents.CAMERA_MOVE, new CameraMessage(Direction.LEFT));
        }
        if (Input.GetKey(KeyCode.S)) {
            EventManager.TriggerEvent(CEvents.CAMERA_MOVE, new CameraMessage(Direction.DOWN));
        }
        if (Input.GetKey(KeyCode.D)) {
            EventManager.TriggerEvent(CEvents.CAMERA_MOVE, new CameraMessage(Direction.RIGHT));
        }
    }
}
