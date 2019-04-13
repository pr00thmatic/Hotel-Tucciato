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

        bool DrawWallButton (BuildingTile tile, CardinalPoint orientation) {
            float size = Facade.tileSize * 0.1f;
            Vector3 pos = tile.transform.localPosition +
                (tile.transform.forward - tile.transform.right) * (Facade.tileSize/2f);
            pos += Util.UnitVector(orientation) * (Facade.tileSize/2f - size * 2);

            return Handles.Button(pos, Quaternion.identity * Quaternion.Euler(90, 0, 0),
                                  size, size, Handles.RectangleHandleCap);
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
                foreach (CardinalPoint orientation in
                         System.Enum.GetValues(typeof(CardinalPoint))) {
                    DrawWallButton(tile, orientation);
                }

                // // foreach ()

                //     tile.CurrentType.typeOfWall["north"] =
                //         Util.Next(tile.CurrentType.typeOfWall["north"]);
                //     tile.UpdateBuildingType();

                //     Target.tilesInfo[i] = tile.CurrentType;
                // }

                // i++;
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
