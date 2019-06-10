using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;

// TODO: can't work standalone
namespace Building {
    [CustomEditor(typeof(BuildingCell))]
    public class BuildingCellEditor : GenericEditor<BuildingCell> {
        void OnEnable () {
            Target.ReadNeighbours();
        }

        public static bool DrawNeighbourButton (BuildingCell cell, CardinalPoint location) {
            BuildingCell neighbour = null;
            if (cell.connected != null && (int) location < cell.connected.Length) {
                neighbour = cell.connected[(int) location];
            }

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

        public static void DrawGizmos (BuildingCell cell) {
            Handles.matrix = cell.transform.localToWorldMatrix;
            BuildingTileEditor.DrawGizmos(cell.tile);

            foreach (CardinalPoint location in Util.ListCardinalPoints()) {
                if (DrawNeighbourButton(cell, location)) {
                    cell.AddNeighbour(location);
                }
            }
        }


        void OnSceneGUI () {
            DrawGizmos(Target);
            UselessSceneGUI();
        }
    }
}
