using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Terrain/New Config", order = 1)]
public class TerrainConfig : ScriptableObject {
    [Tooltip("Y axis max.")]
    public int height = 2;
    [Tooltip("X Axis.")]
    [Range(32, 2048)]
    public int width = 32;
    [Tooltip("Z Axis.")]
    [Range(32, 2048)]
    public int depth = 32;
    [Tooltip("Max amplitude of the height.")]
    public float scale = 2f;
}
