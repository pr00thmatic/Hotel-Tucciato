using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;

// GUILayout.Button("label")
// Handles.Button(pos, rot, size1, size2, Handles.RectangleHandleCap)
// Handles.PositionHandle(pos, rot)
// EditorUtility.SetDirty(gameObject);
namespace Building {
    [CustomEditor(typeof(BuildingCell))]
    public class BuildingCellEditor : Editor {
        BuildingCell _parsedTarget;
        BuildingCell Target {
            get {
                if (_parsedTarget == null) _parsedTarget = (BuildingCell) target;
                return _parsedTarget;
            }
        }

        public bool DrawNeighbourButton (BuildingCell cell, CardinalPoint location) {
            BuildingCell neighbour = cell.connected[(int) location];
            Vector3 pos = Util.UnitVector(location) * FloorTile.tileSize +
                FloorTile.tileSize/2f * new Vector3(-1, 0, 1);
            float size = FloorTile.tileSize * 0.2f;
            Color last = Handles.color;
            Handles.color = neighbour == null? Color.green: Color.red;
            bool clicked = Handles.Button(pos, Quaternion.Euler(90, 0, 0),
                                          size, size, Handles.CircleHandleCap);
            Handles.color = last;

            return clicked;
        }

        public void DrawGizmos (BuildingCell cell) {
            Handles.matrix = cell.transform.localToWorldMatrix;
            BuildingTileEditor.DrawGizmos(cell.tile);

            foreach (CardinalPoint location in Util.ListCardinalPoints()) {
                if (DrawNeighbourButton(cell, location)) {
                    cell.AddNeighbour(location);
                }
            }
        }

        public override void OnInspectorGUI () {
            DrawDefaultInspector();

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
    }
}
