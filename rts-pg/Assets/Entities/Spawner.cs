using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour {

    [SerializeField] GameObject entity;

    [SerializeField] List<Vector3> positions;

    void Start() {
        return;
        NavMeshHit hit;
        foreach(Vector3 pos in positions) {
            if (NavMesh.SamplePosition(pos, out hit, 200f, 9)) {
                Instantiate(entity, hit.position, Quaternion.identity);
            }
        }
    }

    void Update() {

    }
}
