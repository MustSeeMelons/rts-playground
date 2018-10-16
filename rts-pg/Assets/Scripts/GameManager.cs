using UnityEngine;

public class GameManager : MonoBehaviour {

    void Start() {
        EventManager.IgnoreEvent(CEvents.MOUSE_POSITION);
    }

    void Update() {

    }
}
