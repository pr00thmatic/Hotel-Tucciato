using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Building;
using System.Collections.Generic;

[CustomEditor(typeof(MatrixBuilding))]
public class MatrixBuildingEditor : GenericEditor<MatrixBuilding> {
    public static IBuildingModeOption current;
    public static TileCreation creation = new TileCreation();
    public static IBuildingModeOption Current {
        get {
            if (current == null) current = creation;
            return current;
        }
        set => current = value;
    }

    public void UndoHandler () {
        Target.PopulatePiecesInfo();
    }

    void OnEnable () {
        Target.PopulatePiecesInfo();
        CoolEditor.HideTool();
        Undo.undoRedoPerformed += UndoHandler;
    }

    void OnDisable () {
        Undo.undoRedoPerformed -= UndoHandler;
        CoolEditor.ShowTool();
    }

    public override void CustomInspectorGUI () {
        Current = GUILayout.Toggle(Current == creation, "Tile creation", "Button")?
            creation: Current;
        Current.DrawInspectorGUI(Target);
    }

    void OnSceneGUI () {
        Current.DrawGizmos(Target);
        UselessSceneGUI();
    }
}
