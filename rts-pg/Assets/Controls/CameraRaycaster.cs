using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRaycaster : MonoBehaviour {

    [SerializeField] int[] layerPriorities;
    int lastFrameLayer = -1;
    readonly float maxRaycastDepth = 100f;

    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, maxRaycastDepth);

        RaycastHit? priorityHit = GetLayerHitByPriority(raycastHits);

        if (priorityHit != null) {
            EventManager.TriggerEvent(
           CEvents.MOUSE_POSITION,
           new PositionMessage(priorityHit.Value.point)
           );
        }

    }

    RaycastHit? GetLayerHitByPriority(RaycastHit[] raycastHits) {
        foreach (int layer in layerPriorities) {
            foreach (RaycastHit hit in raycastHits) {
                if (hit.collider.gameObject.layer == layer) {
                    return hit;
                }
            }
        }
        return null;
    }
}
