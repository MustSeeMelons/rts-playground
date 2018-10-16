using UnityEngine;

public class CameraRaycaster : MonoBehaviour {

    [SerializeField] int[] layerPriorities;
    int lastFrameLayer = -1;
    readonly float maxRaycastDepth = 100f;

    // TODO: move to event manager?
    // LayerChange delegate
    public delegate void OnCursorLayerChange(int newLayer);
    public event OnCursorLayerChange onCursorLayerChange;

    // Mouse click delegate
    public delegate void OnClickPriorityLayer(RaycastHit raycastHit, int layerHit);
    public event OnClickPriorityLayer onPriorityLayerClick;

    void Update() {
        // Don't have a UI for now
        // if (EventSystem.current.IsPointerOverGameObject()) {
        //     return;
        // }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, maxRaycastDepth);

        RaycastHit? priorityHit = GetLayerHitByPriority(raycastHits);
        if (!priorityHit.HasValue) {
            // Mouse is on something we dont care about
            NotifyObserersIfLayerChanged(0);
            return;
        }

        // Fire event if layer has changed
        var layerHit = priorityHit.Value.collider.gameObject.layer;
        NotifyObserersIfLayerChanged(layerHit);

        // If mouse is clicked send click event, otherwise just the position
        if (Input.GetMouseButton(0)) {
            // onPriorityLayerClick(priorityHit.Value, layerHit);
        } else {
            EventManager.TriggerEvent(
                CEvents.MOUSE_POSITION,
                new PositionMessage(priorityHit.Value.point)
                );
        }
    }

    void NotifyObserersIfLayerChanged(int newLayer) {
        if (newLayer != lastFrameLayer) {
            lastFrameLayer = newLayer;
            // onCursorLayerChange(newLayer);
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
