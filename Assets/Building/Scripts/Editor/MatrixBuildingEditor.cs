using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Building;
using System.Collections.Generic;

[CustomEditor(typeof(MatrixBuilding))]
public class MatrixBuildingEditor : GenericEditor<MatrixBuilding> {
    public static IBuildingModeOption current;
    public TileCreation creation = new TileCreation();

    public override void CustomInspectorGUI () {
        current = current == null? creation: current;
        current = GUILayout.Toggle(current == creation, "Tile creation", "Button")?
            creation: current;
        current.DrawInspectorGUI(Target);

        if (GUILayout.Button("generate!")) {
            Target.Generate();
        }
    }

    void OnSceneGUI () {
        current.DrawGizmos(Target);
        UselessSceneGUI();
    }


}
