using UnityEngine;

public class TerrainModifyer : MonoBehaviour {

    [SerializeField] TerrainConfig config;
    Terrain terrain;

    int terrainWidth;
    int terrainDepth;

    TerrainData defTerrainData;
    Vector3 lastMousePosition = Vector3.down;
    bool active = false;

    // TODO extract to terrain helper
    int[][] pattern = new int[][]{
        new int[] {-1, -1},
        new int[] {-1, 0},
        new int[] {-1, 1},
        new int[] {-1, 2},
        new int[] {0, -1 },
        new int[] {0, 0 },
        new int[] {0, 1 },
        new int[] {0, 2},
        new int[] {1, -1},
        new int[] {1, 0 },
        new int[] {1, 1},
        new int[] {1, 2},
        new int[] {2, -1},
        new int[] {2, 0},
        new int[] {2, 1},
        new int[] {2, 2},
    };

    private void Start() {
        terrain = GetComponent<Terrain>();
        defTerrainData = terrain.terrainData;

        terrainWidth = config.width;
        terrainDepth = config.depth;
    }

    private void OnEnable() {
        EventManager.StartListening(CEvents.POINTER_MODE, OnPointerMode);
        EventManager.StartListening(CEvents.MOUSE_POSITION, OnMousePosition);
    }

    private void OnDisable() {
        EventManager.StopListening(CEvents.POINTER_MODE, OnPointerMode);
        EventManager.StopListening(CEvents.MOUSE_POSITION, OnMousePosition);
    }

    public void OnPointerMode(BaseMessage msg) {
        active = !active;
    }

    public void OnMousePosition(BaseMessage msg) {
        if (!active) {
            return;
        }

        Vector3 position = (msg as PositionMessage).position;

        if (Utils.AreVectorsEqual(lastMousePosition, position)) {
            return;
        }

        lastMousePosition = position;

        // TODO should not copy, but revert if mouse has moved, then modify again
        TerrainData terrainData = Object.Instantiate(defTerrainData);

        int tx = terrainWidth / 2 + Mathf.FloorToInt(position.x);
        int tz = terrainDepth / 2 + Mathf.FloorToInt(position.z);

        float value = CalculateAverageHeight(tx, tz);

        foreach (int[] pat in pattern) {
            int xVal = Mathf.Clamp(tx + pat[0], 0, terrainWidth);
            int zVal = Mathf.Clamp(tz + pat[1], 0, terrainDepth);

            terrainData.SetHeights(
           xVal,
           zVal,
           new float[,] { { value } }
           );
        }

        terrain.terrainData = terrainData;
    }

    float CalculateAverageHeight(int x, int z) {
        TerrainData data = defTerrainData;

        float avg = 0;
        foreach (int[] pat in pattern) {
            // Terrain is lovered so the top is at y == 0
            avg += -config.height / 2 + data.GetHeight(x + pat[0], z + pat[1]);
        }

        return (float)avg / pattern.Length;
    }
}
