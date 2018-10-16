using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerrainGenerator : MonoBehaviour {
    Terrain terrain;

    [SerializeField] TerrainConfig config;

    NavMeshDataInstance navMeshDataInstance;

    private void OnEnable() {
        terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        terrain.transform.position = new Vector3(-config.width / 2, -config.height, -config.depth / 2);

        List<NavMeshBuildSource> buildSources = new List<NavMeshBuildSource>();

        NavMeshBuilder.CollectSources(
            null,
            ~9, // TODO Custom editor so we can select layers
            NavMeshCollectGeometry.RenderMeshes,
            0,
            new List<NavMeshBuildMarkup>(),
            buildSources
        );

        NavMeshData navData = NavMeshBuilder.BuildNavMeshData(
            NavMesh.GetSettingsByID(0),
            buildSources,
            new Bounds(new Vector3(0, -config.height, 0), new Vector3(config.width, config.height, config.depth)),
            Vector3.zero,
            Quaternion.identity
        );

        if (navData == null) {
            Debug.LogError("Failed to create nav mesh data!");
        } else {
            navMeshDataInstance = NavMesh.AddNavMeshData(navData);
        }
    }

    TerrainData GenerateTerrain(TerrainData terrainData) {
        terrainData.heightmapResolution = config.width + 1;
        terrainData.size = new Vector3(config.width, config.height, config.depth);
        terrainData.SetHeights(0, 0, getHeights());
        return terrainData;
    }

    float[,] getHeights() {
        float[,] heights = new float[config.width, config.depth];
        for (int x = 0; x < config.width; x++) {
            for (int z = 0; z < config.depth; z++) {
                heights[x, z] = getHeightAt(x, z);
            }
        }
        return heights;
    }

    float getHeightAt(int x, int z) {
        float xCoord = (float)x / config.width * config.scale;
        float zCoord = (float)z / config.depth * config.scale;

        // TODO: will need to feed in seed
        return Mathf.PerlinNoise(xCoord, zCoord);
    }
}
