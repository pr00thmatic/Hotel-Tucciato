using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Building;
using System.Collections.Generic;

namespace Building {
public class TileCreation: IBuildingModeOption {
    public void DrawInspectorGUI (MatrixBuilding building) {}

    public void DrawGizmos (MatrixBuilding building) {
        foreach (KeyValuePair<Coord,BuildingCell> piece in building.pieces) {
            if (!piece.Value) {
                continue;
            }
            BuildingCellEditor.DrawGizmos(piece.Value);
            if (BuildingCellEditor.hasModifiedCollection) return;
        }
    }
}
}
