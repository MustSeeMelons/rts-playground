using UnityEngine;

public class TerrainModifyer : MonoBehaviour {

    [SerializeField] TerrainConfig config;
    Terrain terrain;

    int terrainWidth;
    int terrainDepth;

    TerrainData defTerrainData;
    Vector3 lastMousePosition = Vector3.down;
    bool active = false;

    Tuple[] pattern;
    GameObject instansiatedBuilding;
    Building currBuilding;

    private void Start() {
        terrain = GetComponent<Terrain>();
        defTerrainData = terrain.terrainData;

        terrainWidth = config.width;
        terrainDepth = config.depth;
    }

    private void OnEnable() {
        EventManager.StartListening(CEvents.BUILD, OnPointerMode);
        EventManager.StartListening(CEvents.MOUSE_POSITION, OnMousePosition);
    }

    private void OnDisable() {
        EventManager.StopListening(CEvents.BUILD, OnPointerMode);
        EventManager.StopListening(CEvents.MOUSE_POSITION, OnMousePosition);
    }

    private void Update() {
        if (active) {
            if (Input.GetMouseButtonDown(0)) {
                active = false;
                instansiatedBuilding = null;
                defTerrainData = terrain.terrainData;
            } else if (Input.GetMouseButtonDown(1)) {
                terrain.terrainData = defTerrainData;
                Destroy(instansiatedBuilding);
            }
        }
    }

    public void OnPointerMode(BaseMessage msg) {
        active = !active;
        if (active) {
            GameObject obj = (msg as BuildMessage).obj;

            Building b = obj.GetComponent<Building>();
            currBuilding = b;
            pattern = b.buildingPattern.pattern.data;

            instansiatedBuilding = Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
        } else {
            defTerrainData = terrain.terrainData;
        }
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

        foreach (Tuple pat in pattern) {
            int xVal = Mathf.Clamp(tx + pat.x, 0, terrainWidth);
            int zVal = Mathf.Clamp(tz + pat.z, 0, terrainDepth);

            terrainData.SetHeights(
           xVal,
           zVal,
           new float[,] { { value / config.height } }
           );
        }

        terrain.terrainData = terrainData;
        instansiatedBuilding.transform.position = new Vector3(
            position.x,
            terrainData.GetHeight(tx, tz) + currBuilding.heightModifier,
            position.z);
    }

    float CalculateAverageHeight(int x, int z) {
        TerrainData data = terrain.terrainData;

        float avg = 0;
        foreach (Tuple pat in pattern) {
            avg += data.GetHeight(x + pat.x, z + pat.z);
        }

        return (float)avg / pattern.Length;
    }
}
