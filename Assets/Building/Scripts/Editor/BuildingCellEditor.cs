using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;

// TODO: can't work standalone
namespace Building {
    [CustomEditor(typeof(BuildingCell))]
    public class BuildingCellEditor : GenericEditor<BuildingCell> {
        public static bool hasModifiedCollection = false;

        void OnEnable () {
            Target.MatrixBuilding.PopulatePiecesInfo();
        }

        public static bool DrawButton (Coord coord, MatrixBuilding matrix) {
            CoolEditor.SetHandlesMatrix(matrix.transform.localToWorldMatrix);

            bool clicked = false;
            bool exists = matrix.pieces.ContainsKey(coord) && matrix.pieces[coord];
            CoolEditor.SetHandlesColor(exists? Color.red: Color.green);

            float size = FloorTile.tileSize * 0.2f;
            Vector3 pos = coord.ToWorld() + new Vector3(-1,0,1) * FloorTile.tileSize * 0.5f;
            if (Handles.Button(pos, Quaternion.Euler(90,0,0), size, size,
                               Handles.CircleHandleCap)) {
                if (exists) matrix.Remove(coord);
                else matrix.Add(coord);
                matrix.Blend(coord);
                clicked = true;
            }

            CoolEditor.RestoreHandlesColor();
            CoolEditor.RestoreHandlesMatrix();
            return clicked;
        }

        public static void DrawGizmos (BuildingCell cell) {
            hasModifiedCollection = false;
            BuildingTileEditor.DrawGizmos(cell.tile);
            if (DrawButton(cell.Coord, cell.MatrixBuilding)) {
                hasModifiedCollection = true;
                return;
            }

            foreach (CardinalPoint point in Util.ListCardinalPoints()) {
                if (DrawButton(cell.Coord + Coord.Cast(Util.UnitVector(point)),
                               cell.MatrixBuilding)) {
                    hasModifiedCollection = true;
                    return;
                }
            }
        }


        void OnSceneGUI () {
            DrawGizmos(Target);
            UselessSceneGUI();
        }
    }
}
