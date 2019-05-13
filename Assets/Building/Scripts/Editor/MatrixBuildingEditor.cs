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
        DrawGizmos(Target);

        if (GUI.changed) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    bool DrawButton (Coord coord, BuildingCell cell) {
        Vector3 pos = coord.ToWorld(FloorTile.tileSize) +
            FloorTile.tileSize/2f * new Vector3(-1, 0, 1);
        float size = FloorTile.tileSize * 0.2f;
        Color last = Handles.color;
        Handles.color = cell == null? Color.green: Color.red;
        bool clicked = Handles.Button(pos, Quaternion.Euler(90, 0, 0),
                                      size, size, Handles.CircleHandleCap);
        Handles.color = last;

        return clicked;
    }

    public void DrawGizmos (MatrixBuilding building) {
        Handles.matrix = building.transform.localToWorldMatrix;
        Dictionary<Coord, bool> flags = new Dictionary<Coord, bool>();
        List<Coord> queuedAddition = new List<Coord>();
        List<Coord> queuedDeletion = new List<Coord>();

        foreach (var piece in building.pieces) {
            if (DrawButton(piece.Key, piece.Value)) {
                queuedDeletion.Add(piece.Key);
            }

            for (int xStep=-1; xStep<2; xStep++) {
                for (int zStep=-1; zStep<2; zStep++) {
                    Coord neighbour = new Coord(piece.Key.x + xStep,
                                                piece.Key.z + zStep);

                    if (Mathf.Abs(xStep) == Mathf.Abs(zStep) ||
                        flags.ContainsKey(neighbour) ||
                        building.pieces.ContainsKey(neighbour)) {
                        continue;
                    }

                    flags[neighbour] = true;
                    if (DrawButton(neighbour, null)) {
                        queuedAddition.Add(neighbour);
                    }
                }
            }
        }

        foreach (Coord queued in queuedDeletion) {
            building.Remove(queued);
        }

        foreach (Coord queued in queuedAddition) {
            building.Add(queued);
        }
    }
}
