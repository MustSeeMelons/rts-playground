using UnityEditor;
using UnityEngine;

// [CustomPropertyDrawer(typeof(Pattern))]
public class PatternPropertyDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        float startY = position.y;
        float startX = position.x;

        var rows = property.FindPropertyRelative("rows");
        var columns = property.FindPropertyRelative("columns");

        EditorGUI.PropertyField(position, rows);
        position.y += position.height;
        EditorGUI.PropertyField(position, columns);

        var data = property.FindPropertyRelative("data");
        position.y += position.height;

        position.width /= columns.intValue;

        for (int i = 0; i < rows.intValue; i++) {
            for (int j = 0; j < columns.intValue; j++) {
                var elem = data.GetArrayElementAtIndex(i * rows.intValue + j);

                EditorGUI.PropertyField(position, elem, true);
                position.x += position.width;
            }
            position.y += position.height * 2;
            position.x = startX;
        }

        EditorGUI.EndProperty();
    }
}
