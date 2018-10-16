using UnityEngine;

public class CameraControl : MonoBehaviour {

    private readonly float speed = 10f;

    private void OnEnable() {
        EventManager.StartListening(CEvents.CAMERA_MOVE, OnCameraMove);
    }

    private void OnDisable() {
        EventManager.StopListening(CEvents.CAMERA_MOVE, OnCameraMove);
    }

    //TODOL those vectors should be normalizes and const somewhere
    public void OnCameraMove(BaseMessage msg) {
        Direction dir = (msg as CameraMessage).direction;
        switch (dir) {
            case Direction.UP:
                transform.Translate(new Vector3(1f, 0, 1f) * Time.deltaTime * speed);
                break;
            case Direction.DOWN:
                transform.Translate(new Vector3(-1f, 0, -1f) * Time.deltaTime * speed);
                break;
            case Direction.LEFT:
                transform.Translate(new Vector3(-1f, 0, 1f) * Time.deltaTime * speed);
                break;
            case Direction.RIGHT:
                transform.Translate(new Vector3(1f, 0, -1f) * Time.deltaTime * speed);
                break;
        }
    }
}
