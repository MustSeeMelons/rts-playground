using UnityEngine;

public class InputManager : MonoBehaviour {

    void Update() {
        if (Input.GetKeyUp(KeyCode.Q)) {
            EventManager.TriggerEvent(CEvents.POINTER_MODE, new BaseMessage());
        }
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
