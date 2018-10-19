using UnityEngine;

[System.Serializable]
public class Tuple {
    public int x;
    public int z;

    public Tuple(int x, int z) {
        this.x = x;
        this.z = z;
    }
}

[System.Serializable]
public class Pattern {
    [Range(1, 5)]
    public int rows;
    [Range(1, 5)]
    public int columns;
    public Tuple[] data;

    public Pattern(Tuple[] data) {
        this.data = data;
    }
}

[CreateAssetMenu(fileName = "Data", menuName = "Terrain/Base Pattern", order = 2)]
public class BasePattern : ScriptableObject {
    public Pattern pattern;
}
