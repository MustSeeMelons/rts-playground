using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleMaggot : MonoBehaviour {

    CameraRaycaster raycaster;
    NavMeshAgent agent;

    void Start() {
        raycaster = FindObjectOfType<CameraRaycaster>();
        agent = GetComponent<NavMeshAgent>();

        // raycaster.onPriorityLayerClick += OnTerrainClick;
    }

    void OnTerrainClick(RaycastHit raycastHit, int layerHit) {
        agent.SetDestination(raycastHit.point);
    }

    private void OnDrawGizmos() {
        NavMeshPath path = agent.path;
        if (path.corners.Length == 0) {
            return;
        }
        for (int i = 0; i < path.corners.Length - 1; i++) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
        }
    }
}
