using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Building;
using System.Collections.Generic;

// GUILayout.Button("label")
// Handles.Button(pos, rot, size1, size2, Handles.RectangleHandleCap)
// Handles.PositionHandle(pos, rot)
// EditorUtility.SetDirty(gameObject);
[CustomEditor(typeof(MatrixBuilding))]
public class MatrixBuildingEditor : Editor {
    MatrixBuilding Target { get => (MatrixBuilding) target; }
    public static IBuildingModeOption current;
    public TileCreation creation = new TileCreation();

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        current = current == null? creation: current;
        current = GUILayout.Toggle(current == creation, "Tile creation", "Button")?
            creation: current;
        current.DrawInspectorGUI(Target);

        if (GUILayout.Button("generate!")) {
            Target.Generate();
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    void OnSceneGUI () {
        current.DrawGizmos(Target);

        if (GUI.changed) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }


}
