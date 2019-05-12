using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Building {
    [CustomEditor(typeof(BuildingTile))]
    public class BuildingTileEditor : Editor {
        BuildingTile    _parsedTarget;
        BuildingTile    Target {
            get {
                if (_parsedTarget == null) _parsedTarget = (BuildingTile) target;
                return _parsedTarget;
            }
        }

        static bool DrawWallButton (BuildingTile tile, CardinalPoint orientation) {
            float size = FloorTile.tileSize * 0.1f;
            Vector3 pos = (Vector3.forward - Vector3.right) * FloorTile.tileSize / 2f;
                // (tile.transform.forward - tile.transform.right) *
                // (FloorTile.tileSize/2f);
            pos += Util.UnitVector(orientation) *
                // pos += tile.transform.TransformDirection(Util.UnitVector(orientation)) *
                (FloorTile.tileSize/2f - size * 2);

            return Handles.Button(pos, Quaternion.Euler(90, 0, 0),
                                  size, size, Handles.RectangleHandleCap);
        }

        static bool DrawLightControl (BuildingTile tile) {
            float size = FloorTile.tileSize * 0.25f;
            Vector3 pos = Vector3.up * 6 - new Vector3(1, 0, -1) * FloorTile.tileSize/2f;
            bool toggledActive =
                Handles.Button(pos, Quaternion.Euler(90, 0, 0),
                               size, size, Handles.CircleHandleCap);
            bool toggledOnOff = false;
            if (tile.ceilingLight.isActive) {
                toggledOnOff = Handles.Button(pos + Vector3.right * size,
                                              Quaternion.identity,
                                              size * 0.5f, size * 0.5f,
                                              Handles.RectangleHandleCap);
            }

            if (toggledActive) {
                tile.ToggleCeilingLightExistence();
            } else if (toggledOnOff) {
                tile.ToggleCeilingLight();
            }
            return toggledActive || toggledOnOff;
        }


        public static bool DrawGizmos (BuildingTile tile) {
            Handles.matrix = tile.transform.localToWorldMatrix;
            bool modified = false;

            foreach (CardinalPoint orientation in
                     System.Enum.GetValues(typeof(CardinalPoint))) {
                if (DrawWallButton(tile, orientation)) {
                    tile.ShuffleWall(orientation);
                    modified = true;
                }
            }

            DrawLightControl(tile);

            return modified;
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
