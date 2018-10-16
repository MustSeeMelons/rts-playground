using UnityEditor;

[CustomEditor(typeof(CameraRaycaster))]
public class CameraRaycasterEditor : Editor {
    bool isLayerPrioritiesUnfolded = true;

    public override void OnInspectorGUI() {
        serializedObject.Update();

        isLayerPrioritiesUnfolded = EditorGUILayout.Foldout(isLayerPrioritiesUnfolded, "Layer Priorities");
        if (isLayerPrioritiesUnfolded) {
            EditorGUI.indentLevel++;
            {
                BindArraySize();
                BindArrayElements();
            }
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }

    void BindArraySize() {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
        int requiredArraySize = EditorGUILayout.IntField("Size", currentArraySize);
        if (requiredArraySize != currentArraySize) {
            serializedObject.FindProperty("layerPriorities.Array.size").intValue = requiredArraySize;
        }
    }

    void BindArrayElements() {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
        for (int i = 0; i < currentArraySize; i++) {
            var prop = serializedObject.FindProperty(string.Format("layerPriorities.Array.data[{0}]", i));
            prop.intValue = EditorGUILayout.LayerField(string.Format("Layer {0}:", i), prop.intValue);
        }
    }
}
