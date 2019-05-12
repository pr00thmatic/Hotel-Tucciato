using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Building;

// GUILayout.Button("label")
// Handles.Button(pos, rot, size1, size2, Handles.RectangleHandleCap)
// Handles.PositionHandle(pos, rot)
// EditorUtility.SetDirty(gameObject);
[CustomEditor(typeof(MatrixBuilding))]
public class MatrixBuildingEditor : Editor {
    MatrixBuilding _parsedTarget;
    MatrixBuilding Target {
        get {
            if (_parsedTarget == null) _parsedTarget = (MatrixBuilding) target;
            return _parsedTarget;
        }
    }

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        if (GUILayout.Button("generate!")) {
            Target.Generate();
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    void OnSceneGUI () {
        Handles.matrix = Target.transform.localToWorldMatrix;
        foreach (BuildingCell cell in Target.pieces) {
            BuildingTileEditor.DrawGizmos(cell.tile);
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }
}
