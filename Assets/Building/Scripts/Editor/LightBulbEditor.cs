using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// GUILayout.Button("label")
// Handles.Button(pos, rot, size1, size2, Handles.RectangleHandleCap)
// Handles.PositionHandle(pos, rot)
// EditorUtility.SetDirty(gameObject);
[CustomEditor(typeof(LightBulb))]
public class LightBulbEditor : GenericEditor<LightBulb> {
    public override void CustomInspectorGUI () {
        if (GUILayout.Button("On/Off")) {
            Target.Toggle();
        }

        if (GUILayout.Button("Toggle Active")) {
            Target.SetActive(!Target.isActive);
        }
    }
}
