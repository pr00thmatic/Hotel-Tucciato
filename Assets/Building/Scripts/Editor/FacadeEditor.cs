using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Building {
    // GUILayout.Button("label")
    // Handles.Button(pos, rot, size1, size2, Handles.RectangleHandleCap)
    // Handles.PositionHandle(pos, rot)
    // EditorUtility.SetDirty(gameObject);
    [CustomEditor(typeof(Facade))]
    public class FacadeEditor : Editor {
        Facade _parsedTarget;
        Facade Target {
            get {
                if (_parsedTarget == null) _parsedTarget = (Facade) target;
                return _parsedTarget;
            }
        }

        public override void OnInspectorGUI () {
            DrawDefaultInspector();
            if (GUILayout.Button("Generate")) Target.Generate();

            if (GUI.changed) {
                EditorUtility.SetDirty(Target);
                EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
            }
        }

        bool DrawWallButton (BuildingTile tile, string orientation) {
            Vector3 pos = tile.transform.localPosition;
            Vector2 size = new Vector2(1, 1) * 0.8f * Facade.tileSize;

            if (orientation == "south" || orientation == "north") {
                size = new Vector3(size.x, size.y * 0.3f);
            } else {
                size = new Vector3(size.x * 0.3f, size.y);
            }

            if (orientation == "south") {
                pos += tile.transform.forward * (Facade.tileSize - size.y);
            } else if (orientation == "east") {
                pos += -tile.transform.right * (Facade.tileSize - size.x);
            }

            return Handles.Button(pos, size.x, size.y, Handles.RectangleHandleCap);
        }

        void EndAndStart () {
            Handles.matrix = Target.transform.localToWorldMatrix;

            Quaternion rot = Quaternion.LookRotation(Target.start - Target.end);
            Vector3 start = Handles.PositionHandle(Target.start, rot);
            Vector3 end = Handles.
                PositionHandle(Target.end,  rot * Quaternion.Euler(0, 180, 0));
            bool shouldGenerate = start != Target.start || end != Target.end;
            Target.start = start; Target.end = end;

            if (shouldGenerate) {
                Target.Generate();
            }
        }

        void BuildingTileEdition () {
            int i=0;
            foreach (BuildingTile tile in Target.tileInstances) {
                // foreach ()
                
                    tile.CurrentType.typeOfWall["north"] =
                        Util.Next(tile.CurrentType.typeOfWall["north"]);
                    tile.UpdateBuildingType();

                    Target.tilesInfo[i] = tile.CurrentType;
                }

                i++;
            }
        }

        void OnSceneGUI () {
            Tools.current = Tool.None;

            EndAndStart();
            BuildingTileEdition();

            if (GUI.changed) {
                EditorUtility.SetDirty(Target);
                EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
            }
        }
    }
}
