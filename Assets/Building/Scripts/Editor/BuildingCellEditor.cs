using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;

// TODO: can't work standalone
namespace Building {
    [CustomEditor(typeof(BuildingCell))]
    public class BuildingCellEditor : GenericEditor<BuildingCell> {
        void OnEnable () {
            Target.MatrixBuilding.PopulatePiecesInfo();
        }

        public static void DrawButton (Coord coord, MatrixBuilding matrix) {
            CoolEditor.SetHandlesMatrix(matrix.transform.localToWorldMatrix);

            bool exists = matrix.pieces.ContainsKey(coord);
            CoolEditor.SetHandlesColor(exists? Color.red: Color.green);

            float size = FloorTile.tileSize * 0.2f;
            Vector3 pos = coord.ToWorld() + new Vector3(-1,0,1) * FloorTile.tileSize * 0.5f;
            if (Handles.Button(pos, Quaternion.Euler(90,0,0), size, size,
                               Handles.CircleHandleCap)) {
                if (exists) matrix.Remove(coord);
                else matrix.Add(coord);
            }

            CoolEditor.RestoreHandlesColor();
            CoolEditor.RestoreHandlesMatrix();
        }

        public static void DrawGizmos (BuildingCell cell) {
            BuildingTileEditor.DrawGizmos(cell.tile);
            DrawButton(cell.Coord, cell.MatrixBuilding);

            foreach (CardinalPoint point in Util.ListCardinalPoints()) {
                DrawButton(cell.Coord + Coord.Cast(Util.UnitVector(point)),
                           cell.MatrixBuilding);
            }
        }


        void OnSceneGUI () {
            DrawGizmos(Target);
            UselessSceneGUI();
        }
    }
}
