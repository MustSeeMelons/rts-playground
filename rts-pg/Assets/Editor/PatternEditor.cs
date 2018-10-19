using UnityEditor;

// [CustomEditor(typeof(BasePattern))]
public class PatternEditor : Editor {

    int rowCount = 0;
    int colCount = 0;
    bool collapsed = false;

    public override void OnInspectorGUI() {
        serializedObject.Update();

        CreateDimensionSelector();
        CreatePatternCreator();

        serializedObject.ApplyModifiedProperties();
    }

    private void CreateDimensionSelector() {
        var rows = serializedObject.FindProperty("rows");
        rows.intValue = EditorGUILayout.IntField("Rows: ", rows.intValue);
        rowCount = rows.intValue;

        var columns = serializedObject.FindProperty("columns");
        columns.intValue = EditorGUILayout.IntField("Columns: ", columns.intValue);
        colCount = columns.intValue;
    }

    private void CreatePatternCreator() {
        collapsed = EditorGUILayout.Foldout(collapsed, "Pattern");
        if (collapsed) {
            var pattern = serializedObject.FindProperty("pattern");
            serializedObject.FindProperty("pattern.data.Array.size").intValue = rowCount * colCount;

            EditorGUILayout.PropertyField(pattern);
        }
    }
}
