using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// GUILayout.Button("label")
// Handles.Button(pos, rot, size1, size2, Handles.RectangleHandleCap)
// Handles.PositionHandle(pos, rot)
// EditorUtility.SetDirty(gameObject);
[CustomEditor(typeof(LightBulb))]
public class LightBulbEditor : Editor {
    LightBulb _parsedTarget;
    LightBulb Target {
        get {
            if (_parsedTarget == null) _parsedTarget = (LightBulb) target;
            return _parsedTarget;
        }
    }

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        if (GUILayout.Button("On/Off")) {
            Target.Toggle();
        }

        if (GUILayout.Button("Toggle Active")) {
            Target.SetActive(!Target.isActive);
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }
}
